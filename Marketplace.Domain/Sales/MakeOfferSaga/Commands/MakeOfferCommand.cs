using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Constants;
using Marketplace.Domain.SharedKernel;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.MakeOfferSagaNS.Commands
{
	public class MakeOfferCommand : IRequest<Result>
	{
		public MakeOfferCommand(string buyerId, string productId, string sellerId, string message, int quantity)
		{
			this.BuyerId = buyerId;
			this.ProductId = productId;
			this.SellerId = sellerId;
			this.Message = message;
			this.Quantity = quantity;
		}

		public string BuyerId { get; }

		public string ProductId { get; }

		public string SellerId { get; }

		public string Message { get; }

		public int Quantity { get; }

		internal class StartMakingOfferCommandHandler : IRequestHandler<MakeOfferCommand, Result>
		{
			private readonly ISagaRepository<MakeOfferSaga, MakeOfferSagaData> makeOfferSagaRepository;
			private readonly IMediator mediator;

			internal StartMakingOfferCommandHandler(
				ISagaRepository<MakeOfferSaga, MakeOfferSagaData> makeOfferSagaRepository,
				IMediator mediator)
			{
				this.makeOfferSagaRepository = makeOfferSagaRepository;
				this.mediator = mediator;
			}

			public async Task<Result> Handle(MakeOfferCommand notification, CancellationToken cancellationToken)
			{
				var buyerId = new Id(notification.BuyerId);
				var productId = new Id(notification.ProductId);
				var makeOfferSagaId = new MakeOfferSagaId(buyerId, productId);

				var makeOfferSaga = await this.makeOfferSagaRepository.GetByIdAsync(makeOfferSagaId);

				if (makeOfferSaga != null && makeOfferSaga.IsCompleted == false)
					return Result.Fail("Offer was already made to his product!");

				var sellerId = new Id(notification.SellerId);
				var saga = this.CreateSaga(
					buyerId,
					productId,
					sellerId,
					notification.Message,
					notification.Quantity
				);

				await saga.StartSagaAsync();

				this.makeOfferSagaRepository.Add(saga);
				var rowsChanged = await this.makeOfferSagaRepository.SaveChangesAsync(cancellationToken);
				if (rowsChanged == 0)
				{
					return Result.Fail(ErrorConstants.NO_RECORD_ALTERED);
				}

				return Result.Ok();
			}

			private MakeOfferSaga CreateSaga(Id buyerId, Id productId, Id sellerId, string message, int quantity)
			{
				var sagaData = new MakeOfferSagaData(buyerId, productId, sellerId, message, quantity);
				var sagaId = new MakeOfferSagaId(buyerId, productId);
				var saga = new MakeOfferSaga(sagaData, sagaId, this.mediator);

				return saga;
			}
		}
	}
}