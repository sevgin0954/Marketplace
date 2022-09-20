using Marketplace.Domain.Sales.BuyerAggregate.Events;
using Marketplace.Domain.Sales.OfferAggregate.Commands;
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
		public async Task Finish_saga_should_change_status_to_completed()
		{
			// Arrange
			var saga = MakeOfferSagaFactory.Create();
			var offerWasAddedToBuyerEvent = new OfferWasAddedToBuyerEvent(null, null);

			// Act
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

			// Act
			await saga.FinishSagaAsync(offerWasAddedToBuyerEvent);

			// Assert
			mediatorMock.Verify(mm => mm.Send(It.IsAny<MakeOfferCommand>(), It.IsAny<CancellationToken>()));
		}
	}
}
