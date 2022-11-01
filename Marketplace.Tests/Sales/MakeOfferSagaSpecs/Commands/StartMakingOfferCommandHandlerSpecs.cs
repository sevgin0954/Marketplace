using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.MakeOfferSagaNS;
using Marketplace.Domain.Sales.ProductAggregate.Commands;
using Marketplace.Domain.Sales.SellerAggregate.Commands;
using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using BuyerStartMakingOfferCommand = Marketplace.Domain.Sales.BuyerAggregate.Commands.StartMakingOfferCommand;
using static Marketplace.Domain.Sales.MakeOfferSagaNS.Commands.MakeOfferCommand;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Tests.Sales.MakeOfferSagaSpecs.Commands
{
	public class StartMakingOfferCommandHandlerSpecs
	{
		[Fact]
		public async Task Without_an_already_created_saga_should_create_new_saga()
		{
			// Arrange
			var repositoryMock = this.GetDefaultSetupedRepositoryMock();
			var mediatorMock = new Mock<IMediator>();

			var handler = new StartMakingOfferCommandHandler(repositoryMock.Object, mediatorMock.Object);

			// Act
			await this.Handle(handler);

			// Assert
			repositoryMock.Verify(r => r.Add(It.Is<MakeOfferSaga>(s => s.IsCompleted == false)));
			repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()));
		}

		[Fact]
		public async Task Should_start_the_created_saga_and_send_check_is_buyer_banned_command()
		{
			// Arrange
			var repositoryMock = this.GetDefaultSetupedRepositoryMock();
			var mediatorMock = new Mock<IMediator>();

			var handler = new StartMakingOfferCommandHandler(repositoryMock.Object, mediatorMock.Object);

			// Act
			await this.Handle(handler);

			// Assert
			mediatorMock
				.Verify(m => 
					m.Send(It.IsAny<CheckIsBuyerBannedCommand>(), It.IsAny<CancellationToken>()), 
					Times.Once);
		}

		[Fact]
		public async Task Should_start_the_created_saga_and_send_check_can_buy_product_command()
		{
			// Arrange
			var repositoryMock = this.GetDefaultSetupedRepositoryMock();
			var mediatorMock = new Mock<IMediator>();

			var handler = new StartMakingOfferCommandHandler(repositoryMock.Object, mediatorMock.Object);

			// Act
			await this.Handle(handler);

			// Assert
			mediatorMock
				.Verify(m =>
					m.Send(It.IsAny<CheckCanBuyProductCommand>(), It.IsAny<CancellationToken>()),
					Times.Once);
		}

		[Fact]
		public async Task Should_start_the_created_saga_and_send_start_making_offer_command()
		{
			// Arrange
			var repositoryMock = this.GetDefaultSetupedRepositoryMock();
			var mediatorMock = new Mock<IMediator>();

			var handler = new StartMakingOfferCommandHandler(repositoryMock.Object, mediatorMock.Object);

			// Act
			await this.Handle(handler);

			// Assert
			mediatorMock
				.Verify(m =>
					m.Send(It.IsAny<BuyerStartMakingOfferCommand>(), It.IsAny<CancellationToken>()),
					Times.Once);
		}

		[Fact]
		public async Task With_created_saga_in_completed_state_should_create_new_saga_in_not_completed_state()
		{
			var repositoryMock = this.GetDefaultSetupedRepositoryMock();

			var completedSaga = await MakeOfferSagaFactory.CreateCompletedAsync();
			this.SetupRepositoryGetByIdAsyncMock(repositoryMock, completedSaga);

			var mediatorMock = new Mock<IMediator>();

			var handler = new StartMakingOfferCommandHandler(repositoryMock.Object, mediatorMock.Object);

			// Act
			await this.Handle(handler);

			// Assert
			repositoryMock.Verify(r => r.Add(It.Is<MakeOfferSaga>(s => s.IsCompleted == false)));
			repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()));
		}

		private Mock<ISagaDataRepository<MakeOfferSaga, MakeOfferSagaData>> GetDefaultSetupedRepositoryMock()
		{
			var repositoryMock = new Mock<ISagaDataRepository<MakeOfferSaga, MakeOfferSagaData>>();

			var returnedValue = 1;
			repositoryMock
				.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.Returns(Task.FromResult(returnedValue));

			return repositoryMock;
		}

		private void SetupRepositoryGetByIdAsyncMock(
			Mock<ISagaDataRepository<MakeOfferSaga, MakeOfferSagaData>> repositoryMock, 
			MakeOfferSaga returnedSaga)
		{
			repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Id>())).Returns(Task.FromResult(returnedSaga));
		}

		private async Task<Result> Handle(StartMakingOfferCommandHandler handler)
		{
			var cancelationToken = new CancellationToken();
			var command = StartMakingOfferCommandFactory.Create();

			return await handler.Handle(command, cancelationToken);
		}
	}
}
