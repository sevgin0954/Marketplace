using Marketplace.Domain.Sales.BuyerAggregate.Commands;
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
	public class TransitionSpecs
	{
		[Fact]
		public async Task On_receiving_buyer_was_banned_event_with_null_reference_should_throw_an_exception()
		{
			// Arrange
			var saga = MakeOfferSagaFactory.Create();
			BuyerWasBannedEvent buyerWasBannedEvent = null;

			// Act
			// Assert
			await Assert
				.ThrowsAsync<ArgumentNullException>(async () => await saga.TransitionAsync(buyerWasBannedEvent));
		}

		[Fact]
		public async Task On_receiving_buyer_was_banned_event_should_complete_saga()
		{
			// Arrange
			var saga = MakeOfferSagaFactory.Create();
			var buyerWasBannedEvent = new BuyerWasBannedEvent(null, null);

			// Act
			await saga.TransitionAsync(buyerWasBannedEvent);

			// Assert
			Assert.True(saga.IsCompleted);
		}

		[Fact]
		public async Task On_receiving_buyer_was_banned_event_should_send_discard_making_offer_to_buyer()
		{
			// Arrange
			var mediatorMock = new Mock<IMediator>();
			var saga = MakeOfferSagaFactory.Create(mediatorMock.Object);

			var buyerWasBannedEvent = new BuyerWasBannedEvent(null, null);

			// Act
			await saga.TransitionAsync(buyerWasBannedEvent);

			// Assert
			mediatorMock.Verify(mm => mm.Send(It.IsAny<DiscardMakingOfferCommand>(), It.IsAny<CancellationToken>()));
		}

		[Fact]
		public async Task On_receiving_buyer_was_not_banned_event_with_null_reference_should_throw_an_exception()
		{
			// Arrange
			var saga = MakeOfferSagaFactory.Create();
			BuyerWasNotBannedEvent buyerWasNotBannedEvent = null;

			// Act
			// Assert
			await Assert
				.ThrowsAsync<ArgumentNullException>(async () => await saga.TransitionAsync(buyerWasNotBannedEvent));
		}

		[Fact]
		public async Task On_receiving_product_could_not_be_bought_should_complete_saga()
		{
			// Arrange
			var saga = MakeOfferSagaFactory.Create();
			var productCouldNotBeBoughtEvent = new ProductCouldNotBeBoughtEvent(null, null, null);

			// Act
			await saga.TransitionAsync(productCouldNotBeBoughtEvent);

			// Assert
			Assert.True(saga.IsCompleted);
		}

		[Fact]
		public async Task On_receiving_product_could_not_be_bought_should_send_discard_making_offer_to_buyer()
		{
			// Arrange
			var mediatorMock = new Mock<IMediator>();
			var saga = MakeOfferSagaFactory.Create(mediatorMock.Object);

			var productCouldNotBeBoughtEvent = new ProductCouldNotBeBoughtEvent(null, null, null);

			// Act
			await saga.TransitionAsync(productCouldNotBeBoughtEvent);

			// Assert
			mediatorMock.Verify(mm => mm.Send(It.IsAny<DiscardMakingOfferCommand>(), It.IsAny<CancellationToken>()));
		}

		[Fact]
		public async Task On_receiving_buyer_was_not_banned_event_and_product_could_be_bought_event_should_send_finish_making_offer_command_to_buyer()
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
			mediatorMock.Verify(mm => mm.Send(It.IsAny<FinishMakingOfferCommand>(), It.IsAny<CancellationToken>()));
		}

		[Fact]
		public async Task On_receiving_buyer_was_not_banned_event_only_not_send_finish_making_offer_command_to_buyer()
		{
			// Arrange
			var mediatorMock = new Mock<IMediator>();
			var saga = MakeOfferSagaFactory.Create(mediatorMock.Object);

			var buyerWasNotBannedEvent = new BuyerWasNotBannedEvent(null, null);

			// Act
			await saga.TransitionAsync(buyerWasNotBannedEvent);

			// Assert
			mediatorMock
				.Verify(mm => mm.Send(It.IsAny<FinishMakingOfferCommand>(), It.IsAny<CancellationToken>()), Times.Never);
		}

		[Fact]
		public async Task On_receiving_product_could_be_bought_event_only_not_send_finish_making_offer_command_to_buyer()
		{
			// Arrange
			var mediatorMock = new Mock<IMediator>();
			var saga = MakeOfferSagaFactory.Create(mediatorMock.Object);


			var productCouldBeBoughtEvent = new ProductCouldBeBoughtEvent(null, null);

			// Act
			await saga.TransitionAsync(productCouldBeBoughtEvent);

			// Assert
			mediatorMock
				.Verify(mm => mm.Send(It.IsAny<FinishMakingOfferCommand>(), It.IsAny<CancellationToken>()), Times.Never);
		}
	}
}