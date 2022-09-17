using Marketplace.Domain.Sales.MakeOfferSagaNS;
using Marketplace.Domain.SharedKernel;
using MediatR;
using Moq;

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
	}
}
