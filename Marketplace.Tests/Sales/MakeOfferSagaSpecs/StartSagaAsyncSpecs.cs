using Marketplace.Domain.Sales.ProductAggregate.Commands;
using Marketplace.Domain.Sales.SellerAggregate.Commands;
using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Marketplace.Tests.Sales.MakeOfferSagaSpecs
{
	public class StartSagaAsyncSpecs
	{
		[Fact]
		public async Task Start_already_started_saga_should_throw_an_exception()
		{
			// Arrange
			var mock = new Mock<IMediator>();
			var saga = MakeOfferSagaFactory.Create(mock.Object);

			// Act
			await saga.StartSagaAsync();

			// Assert
			var exception = 
				await Assert.ThrowsAsync<InvalidOperationException>(async () => await saga.StartSagaAsync());
		}

		[Fact]
		public async Task Start_saga_should_send_check_is_buyer_banned_command()
		{
			var mediatorMock = new Mock<IMediator>();
			var saga = MakeOfferSagaFactory.Create(mediatorMock.Object);

			// Act
			await saga.StartSagaAsync();

			// Assert
			mediatorMock.Verify(
				m => m.Send(It.IsAny<CheckIsBuyerBannedCommand>(), 
				It.IsAny<CancellationToken>()), Times.Once);
		}

		[Fact]
		public async Task Start_saga_should_send_check_can_buy_product_command()
		{
			var mediatorMock = new Mock<IMediator>();
			var saga = MakeOfferSagaFactory.Create(mediatorMock.Object);

			// Act
			await saga.StartSagaAsync();

			// Assert
			mediatorMock.Verify(
				m => m.Send(It.IsAny<CheckCanBuyProductCommand>(),
				It.IsAny<CancellationToken>()), Times.Once);
		}
	}
}