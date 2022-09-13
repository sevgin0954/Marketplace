using Marketplace.Domain.Common;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.MakeOfferSaga.Commands
{
	public class StartMakingOfferCommand : IRequest<Result>
	{
		const string OFFER_ALREADY_EXISTS = "Offer was already made to his product!";

		public StartMakingOfferCommand(string buyerId, string productId, string sellerId, string message, int quantity)
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

		internal class StartMakingOfferCommandHandler : IRequestHandler<StartMakingOfferCommand, Result>
		{
			private readonly ISagaRepository<MakeOffer, MakeOfferSagaData> makeOfferSagaRepository;
			private readonly Mediator mediator;

			internal StartMakingOfferCommandHandler(
				ISagaRepository<MakeOffer, MakeOfferSagaData> makeOfferSagaRepository,
				Mediator mediator)
			{
				this.makeOfferSagaRepository = makeOfferSagaRepository;
				this.mediator = mediator;
			}

			public async Task<Result> Handle(StartMakingOfferCommand notification, CancellationToken cancellationToken)
			{
				Result result;

				var makeOfferSagaId = new MakeOfferSagaId(notification.BuyerId, notification.ProductId);
				var makeOfferSaga = await this.makeOfferSagaRepository.GetByIdAsync(makeOfferSagaId);
				if (makeOfferSaga == null || makeOfferSaga.IsCompleted == false)
				{
					await this.CreateSaga(
						notification.BuyerId, 
						notification.ProductId, 
						notification.Message, 
						notification.Quantity, 
						notification.SellerId
					);
					result = Result.Ok();
				}
				else
				{
					result = Result.Fail(OFFER_ALREADY_EXISTS);
				}

				return result;
			}

			private async Task CreateSaga(string buyerId, string productId, string message, int quantity, string sellerId)
			{
				var sagaData = new MakeOfferSagaData(buyerId, productId, message, quantity, sellerId);
				var sagaId = new MakeOfferSagaId(buyerId, productId);
				var saga = new MakeOffer(sagaData, sagaId, this.mediator);

				var rowsChanged = await this.makeOfferSagaRepository.AddAsync(saga);
				if (rowsChanged == 0)
					throw new InvalidOperationException();
			}
		}
	}
}