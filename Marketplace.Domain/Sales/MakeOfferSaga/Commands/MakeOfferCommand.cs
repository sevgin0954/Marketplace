using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Constants;
using Marketplace.Domain.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.MakeOfferSagaNS.Commands
{
    public class MakeOfferCommand : IRequest<Result>
	{
		public MakeOfferCommand(
			string buyerId,
			string productId,
			string sellerId,
			string message,
			int quantity)
		{
			// TODO: Validate ids existence
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

		internal class MakeOfferCommandHandler : IRequestHandler<MakeOfferCommand, Result>
		{
			private readonly IRepository<MakeOfferSagaData, MakeOfferSagaId> sagaDataRepository;
			private readonly IMediator mediator;

			public MakeOfferCommandHandler(
				IRepository<MakeOfferSagaData, MakeOfferSagaId> sagaDataRepository,
				IMediator mediator)
			{
				this.sagaDataRepository = sagaDataRepository;
				this.mediator = mediator;
			}

			public async Task<Result> Handle(MakeOfferCommand request, CancellationToken cancellationToken)
			{
				var buyerId = new Id(request.BuyerId);
				var productId = new Id(request.ProductId);
				var sagaId = new MakeOfferSagaId(buyerId, productId);

				var isSagaDataAvailible = await this.sagaDataRepository.CheckIfExistAsync(sagaId);
				if (isSagaDataAvailible)
					return Result.Fail("Pending offer exists for this product from this buyer!");

				var sellerId = new Id(request.SellerId);
				var sagaData = new MakeOfferSagaData(sagaId, sellerId, request.Message, request.Quantity);
				var saga = new MakeOfferSaga(sagaData, this.mediator);

				await saga.StartSagaAsync();

				var isSagaUpdateSuccessfully = await this.sagaDataRepository.SaveChangesAsync();
				if (isSagaUpdateSuccessfully == false)
					return Result.Fail(ErrorConstants.NO_RECORD_ALTERED);

				return Result.Ok();
			}
		}
	}
}