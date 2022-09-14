using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.OfferAggregate;
using Marketplace.Domain.Sales.OfferAggregate.Commands;
using Marketplace.Domain.SharedKernel;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Marketplace.Domain.Sales.OfferAggregate.Commands.MakeOfferCommand;

namespace Marketplace.Tests.Sales.OfferAggregateSpecs.CommandsSpecs
{
	public class MakeOfferCommandSpecs
	{
		[Fact]
		public async Task Make_offer_should_create_offer_with_correct_id()
		{
			// Arrange
			var buyerId = Guid.NewGuid().ToString();
			var productId = Guid.NewGuid().ToString();
			var command = this.CreateCommand(buyerId, productId);

			var repositoryMock = new Mock<IRepository<Offer, OfferId>>();
			repositoryMock.Setup(rm => rm.AddAsync(It.IsAny<Offer>())).Verifiable();

			var commandHandler = new MakeOfferCommandHandler(repositoryMock.Object);

			var expectedOfferId = new OfferId(new Id(productId), new Id(buyerId));

			// Act
			await this.CallCommandHandler(commandHandler, command);

			// Assert
			repositoryMock.Verify(
				rm => rm.AddAsync(It.Is<Offer>(o => o.Id == expectedOfferId)), 
				Times.Once);
		}

		[Fact]
		public async Task Make_offer_should_create_offer_with_correct_buyer_id()
		{
			// Arrange
			var buyerId = Guid.NewGuid().ToString();
			var command = this.CreateCommandWithBuyerId(buyerId);

			var repositoryMock = new Mock<IRepository<Offer, OfferId>>();
			repositoryMock.Setup(rm => rm.AddAsync(It.IsAny<Offer>())).Verifiable();

			var commandHandler = new MakeOfferCommandHandler(repositoryMock.Object);

			// Act
			await this.CallCommandHandler(commandHandler, command);

			// Assert
			repositoryMock.Verify(
				rm => rm.AddAsync(It.Is<Offer>(o => o.BuyerId.Value == buyerId)),
				Times.Once);
		}

		[Fact]
		public async Task Make_offer_should_create_offer_with_correct_product_id()
		{
			// Arrange
			var productId = Guid.NewGuid().ToString();
			var command = this.CreateCommandWithProductId(productId);

			var repositoryMock = new Mock<IRepository<Offer, OfferId>>();
			repositoryMock.Setup(rm => rm.AddAsync(It.IsAny<Offer>())).Verifiable();

			var commandHandler = new MakeOfferCommandHandler(repositoryMock.Object);

			// Act
			await this.CallCommandHandler(commandHandler, command);

			// Assert
			repositoryMock.Verify(
				rm => rm.AddAsync(It.Is<Offer>(o => o.ProductId.Value == productId)),
				Times.Once);
		}

		[Fact]
		public async Task When_offer_is_not_saved_should_return_failed_result()
		{
			// Arrange
			var command = this.CreateCommand();

			var repositoryMock = new Mock<IRepository<Offer, OfferId>>();
			var saveChangesReturnValue = 0;
			repositoryMock.Setup(rm => rm.SaveChangesAsync()).Returns(Task.FromResult(saveChangesReturnValue));

			var commandHandler = new MakeOfferCommandHandler(repositoryMock.Object);

			// Act
			var result = await this.CallCommandHandler(commandHandler, command);

			// Assert
			Assert.False(result.IsSuccess);
		}

		[Fact]
		public async Task When_new_offer_is_created_and_saved_should_return_ok_result()
		{
			// Arrange
			var command = this.CreateCommand();

			var repositoryMock = new Mock<IRepository<Offer, OfferId>>();
			var saveChangesReturnValue = 1;
			repositoryMock.Setup(rm => rm.SaveChangesAsync()).Returns(Task.FromResult(saveChangesReturnValue));

			var commandHandler = new MakeOfferCommandHandler(repositoryMock.Object);

			// Act
			var result = await this.CallCommandHandler(commandHandler, command);

			// Assert
			Assert.False(result.IsSuccess);
		}

		private MakeOfferCommand CreateCommand()
		{
			var buyerId = Guid.NewGuid().ToString();

			return this.CreateCommandWithBuyerId(buyerId);
		}

		private MakeOfferCommand CreateCommandWithBuyerId(string buyerId)
		{
			var productId = Guid.NewGuid().ToString();

			return this.CreateCommand(buyerId, productId);
		}

		private MakeOfferCommand CreateCommandWithProductId(string productId)
		{
			var buyerId = Guid.NewGuid().ToString();

			return this.CreateCommand(buyerId, productId);
		}

		private MakeOfferCommand CreateCommand(string buyerId, string productId)
		{
			var message = "default message";
			var quantity = 1;
			var sellerId = Guid.NewGuid().ToString();
			var command = new MakeOfferCommand(buyerId, productId, message, quantity, sellerId);

			return command;
		}

		private async Task<Result> CallCommandHandler(MakeOfferCommandHandler handler, MakeOfferCommand command)
		{
			var cancellationToken = new CancellationToken();

			return await handler.Handle(command, cancellationToken);
		}
	}
}
