using Marketplace.Domain.Sales.BuyerAggregate.Events;
using Marketplace.Domain.Sales.MakeOfferSagaNS;
using Marketplace.Domain.Sales.ProductAggregate.Events;
using Marketplace.Domain.Sales.SellerAggregate.Events;
using Marketplace.Domain.SharedKernel;
using MediatR;
using Moq;
using System.Threading.Tasks;

namespace Marketplace.Tests.Sales.MakeOfferSagaSpecs
{
	internal static class MakeOfferSagaFactory
	{
		public static MakeOfferSaga Create()
		{
			var mediatorMock = new Mock<IMediator>();

			return Create(mediatorMock.Object);
		}

		public static MakeOfferSaga Create(IMediator mediator)
		{
			var buyerId = new Id();
			var productId = new Id();
			var sellerId = new Id();
			var message = "Default message";
			var quantity = 1;
			var data = new MakeOfferSagaData(buyerId, productId, sellerId, message, quantity);

			var id = new MakeOfferSagaId(buyerId, productId);

			var saga = new MakeOfferSaga(data, id, mediator);

			return saga;
		}

		public static async Task<MakeOfferSaga> CreateCompletedAsync()
		{
			var saga = Create();

			var productCouldBeBoughtEvent = new ProductCouldBeBoughtEvent(null, null);
			await saga.TransitionAsync(productCouldBeBoughtEvent);

			var buyerWasNotBannedEvent = new BuyerWasNotBannedEvent(null, null);
			await saga.TransitionAsync(buyerWasNotBannedEvent);

			var offerWasAddedToBuyerEvent = new OfferWasAddedToBuyerEvent(null, null);
			await saga.FinishSagaAsync(offerWasAddedToBuyerEvent);

			return saga;
		}
	}
}
