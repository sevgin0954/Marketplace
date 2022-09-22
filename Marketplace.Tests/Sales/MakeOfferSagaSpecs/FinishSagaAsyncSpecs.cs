using Marketplace.Domain.Sales.BuyerAggregate.Events;
using Marketplace.Domain.Sales.OfferAggregate.Commands;
using Marketplace.Domain.Sales.ProductAggregate.Events;
using Marketplace.Domain.Sales.SellerAggregate.Events;
using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Marketplace.Tests.Sales.MakeOfferSagaSpecs
{
	public class FinishSagaAsyncSpecs
	{
		[Fact]
		public async Task With_null_argument_for_message_should_throw_an_exception()
		{
			// Arrange
			var saga = MakeOfferSagaFactory.Create();
			OfferWasAddedToBuyerEvent offerWasAddedToBuyerEvent = null;

			// Act
			// Assert
			await Assert
				.ThrowsAsync<ArgumentNullException>(async () => await saga.FinishSagaAsync(offerWasAddedToBuyerEvent));
		}

		[Fact]
		public async Task Try_finish_saga_without_receiving_product_could_be_bought_and_buyer_was_not_banned_events_first_should_throw_an_exception()
		{
			// Arrange
			var saga = MakeOfferSagaFactory.Create();
			var offerWasAddedToBuyerEvent = new OfferWasAddedToBuyerEvent(null, null);

			var expectedExceptionMessage = "Can't finish saga before all transitions are completed!";

			// Act
			// Assert
			var exception = await Assert.ThrowsAsync<InvalidOperationException>(
				async () => await saga.FinishSagaAsync(offerWasAddedToBuyerEvent));
			Assert.Equal(expectedExceptionMessage, exception.Message);
		}

		[Fact]
		public async Task Try_finish_completed_saga_should_throw_an_exception()
		{
			// Arrange
			var saga = await MakeOfferSagaFactory.CreateCompletedAsync();
			var offerWasAddedToBuyerEvent = new OfferWasAddedToBuyerEvent(null, null);

			var expectedExceptionMessage = "Can't finish completed saga!";

			// Act
			// Assert
			var exception = await Assert.ThrowsAsync<InvalidOperationException>(
				async () => await saga.FinishSagaAsync(offerWasAddedToBuyerEvent));
			Assert.Equal(expectedExceptionMessage, exception.Message);
		}

		[Fact]
		public async Task Finish_saga_should_change_status_to_completed()
		{
			// Arrange
			var saga = MakeOfferSagaFactory.Create();
			var offerWasAddedToBuyerEvent = new OfferWasAddedToBuyerEvent(null, null);
			var productCouldBeBoughtEvent = new ProductCouldBeBoughtEvent(null, null);
			var buyerWasNotBannedEvent = new BuyerWasNotBannedEvent(null, null);

			// Act
			await saga.TransitionAsync(productCouldBeBoughtEvent);
			await saga.TransitionAsync(buyerWasNotBannedEvent);
			await saga.FinishSagaAsync(offerWasAddedToBuyerEvent);

			// Assert
			Assert.True(saga.IsCompleted);
		}

		[Fact]
		public async Task Finish_saga_should_send_make_offer_command()
		{
			// Arrange
			var mediatorMock = new Mock<IMediator>();
			var saga = MakeOfferSagaFactory.Create(mediatorMock.Object);

			var offerWasAddedToBuyerEvent = new OfferWasAddedToBuyerEvent(null, null);
			var productCouldBeBoughtEvent = new ProductCouldBeBoughtEvent(null, null);
			var buyerWasNotBannedEvent = new BuyerWasNotBannedEvent(null, null);

			// Act
			await saga.TransitionAsync(productCouldBeBoughtEvent);
			await saga.TransitionAsync(buyerWasNotBannedEvent);
			await saga.FinishSagaAsync(offerWasAddedToBuyerEvent);

			// Assert
			mediatorMock.Verify(mm => mm.Send(It.IsAny<MakeOfferCommand>(), It.IsAny<CancellationToken>()));
		}
	}
}
