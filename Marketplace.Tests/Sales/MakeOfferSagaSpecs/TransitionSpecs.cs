using Marketplace.Domain.Sales.OfferAggregate.Commands;
using Marketplace.Domain.Sales.ProductAggregate.Events;
using Marketplace.Domain.Sales.SellerAggregate.Events;
using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Marketplace.Tests.Sales.MakeOfferSagaSpecs
{
	public class TransitionSpecs
	{
		[Fact]
		public void On_receiving_buyer_was_banned_event_should_complete_saga()
		{
			// Arrange
			var saga = MakeOfferSagaFactory.Create();
			var buyerWasBannedEvent = new BuyerWasBannedEvent(null, null);

			// Act
			saga.Transition(buyerWasBannedEvent);

			// Assert
			Assert.True(saga.IsCompleted);
		}

		[Fact]
		public void On_receiving_product_could_not_be_bought_should_complete_saga()
		{
			// Arrange
			var saga = MakeOfferSagaFactory.Create();
			var productCouldNotBeBoughtEvent = new ProductCouldNotBeBoughtEvent(null, null, null);

			// Act
			saga.Transition(productCouldNotBeBoughtEvent);

			// Assert
			Assert.True(saga.IsCompleted);
		}

		[Fact]
		public async Task On_receiving_buyer_was_not_banned_event_only_should_not_complete_saga()
		{
			// Arrange
			var saga = MakeOfferSagaFactory.Create();
			var buyerWasBannedEvent = new BuyerWasNotBannedEvent(null, null);

			// Act
			await saga.TransitionAsync(buyerWasBannedEvent);

			// Assert
			Assert.False(saga.IsCompleted);
		}

		[Fact]
		public async Task On_receiving_product_could_be_banned_event_only_should_not_complete_saga()
		{
			// Arrange
			var saga = MakeOfferSagaFactory.Create();
			var productCouldBeBoughtEvent = new ProductCouldBeBoughtEvent(null, null);

			// Act
			await saga.TransitionAsync(productCouldBeBoughtEvent);

			// Assert
			Assert.False(saga.IsCompleted);
		}

		[Fact]
		public async Task On_receiving_buyer_was_not_banned_event_and_product_could_be_bought_event_should_send_make_offer_command()
		{
			// Arrange
			var mediatorMock = new Mock<IMediator>();
			var saga = MakeOfferSagaFactory.Create(mediatorMock.Object);

			var buyerWasNotBannedEvent = new BuyerWasNotBannedEvent(null, null);
			var productCouldBeBoughtEvent = new ProductCouldBeBoughtEvent(null, null);

			// Act
			await saga.TransitionAsync(buyerWasNotBannedEvent);
			await saga.TransitionAsync(productCouldBeBoughtEvent);

			// Assert
			mediatorMock.Verify(mm => mm.Send(It.IsAny<MakeOfferCommand>(), It.IsAny<CancellationToken>()));
		}
	}
}