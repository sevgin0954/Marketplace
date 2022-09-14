﻿using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Exceptions;
using Marketplace.Domain.Sales.OfferAggregate;
using Marketplace.Domain.Sales.OfferAggregate.Commands;
using Marketplace.Domain.SharedKernel;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Marketplace.Domain.Sales.OfferAggregate.Commands.AcceptOfferCommand;

namespace Marketplace.Tests.Sales.OfferSpecs.CommandsSpecs
{
	public class AcceptOfferCommandSpecs
	{
		[Fact]
		public async Task Accept_offer_command_with_incorrect_product_id_should_throw_an_exception()
		{
			// Arrange
			const string ERROR_MESSAGE = "Offer was not found!";

			var offerRepositoryMock = new Mock<IAggregateRepository<Offer, OfferId>>();
			Offer offer = null;
			this.SetupMockedRepositoryGetByIdAsync(offerRepositoryMock, offer);

			var commandHandler = new AcceptOfferCommandHandler(offerRepositoryMock.Object);

			// Act
			// Assert
			var exception = await Assert
				.ThrowsAsync<NotFoundException>(() => this.CallCommandHandlerHandleMethod(commandHandler));
			Assert.Equal(ERROR_MESSAGE, exception.Message);
		}

		[Fact]
		public async Task Accept_offer_command_with_incorrect_buyer_id_should_throw_an_exception()
		{
			// Arrange
			const string ERROR_MESSAGE = "Offer was not found!";

			var offerRepositoryMock = new Mock<IAggregateRepository<Offer, OfferId>>();
			Offer offer = null;
			this.SetupMockedRepositoryGetByIdAsync(offerRepositoryMock, offer);

			var commandHandler = new AcceptOfferCommandHandler(offerRepositoryMock.Object);

			// Act
			// Assert
			var exception = await Assert
				.ThrowsAsync<NotFoundException>(() => this.CallCommandHandlerHandleMethod(commandHandler));
			Assert.Equal(ERROR_MESSAGE, exception.Message);
		}

		[Fact]
		public async Task Accept_offer_command_should_return_failed_result_when_cant_save_successfully()
		{
			// Arrange
			var sellerId = new Id();

			var offerRepositoryMock = new Mock<IAggregateRepository<Offer, OfferId>>();

			var offer = OfferFactory.CreateWithSellerId(sellerId);
			this.SetupMockedRepositoryGetByIdAsync(offerRepositoryMock, offer);

			var saveChangesReturnValue = 0;
			this.SetupMockedRepositorySaveChangesAsync(offerRepositoryMock, saveChangesReturnValue);

			var commandHandler = new AcceptOfferCommandHandler(offerRepositoryMock.Object);

			var command = this.CreateAcceptOfferCommand(sellerId);
			var cancelationToken = new CancellationToken();

			// Act
			var result = await commandHandler.Handle(command, cancelationToken);

			// Assert
			Assert.False(result.IsSuccess);
		}

		[Fact]
		public async Task Accept_offer_command_should_return_result_OK_when_successfull()
		{
			// Arrange
			var sellerId = new Id();

			var offerRepositoryMock = new Mock<IAggregateRepository<Offer, OfferId>>();

			var offer = OfferFactory.CreateWithSellerId(sellerId);
			this.SetupMockedRepositoryGetByIdAsync(offerRepositoryMock, offer);

			var saveChangesReturnValue = 1;
			this.SetupMockedRepositorySaveChangesAsync(offerRepositoryMock, saveChangesReturnValue);

			var commandHandler = new AcceptOfferCommandHandler(offerRepositoryMock.Object);

			var command = this.CreateAcceptOfferCommand(sellerId);
			var cancelationToken = new CancellationToken();

			// Act
			var result = await commandHandler.Handle(command, cancelationToken);

			// Assert
			Assert.True(result.IsSuccess);
		}

		[Fact]
		public async Task Accept_offer_command_should_accept_the_correct_offer_offer()
		{
			// Arrange
			var sellerId = new Id();
			var buyerId = new Id();
			var productId = new Id();

			var repositoryMock = new Mock<IAggregateRepository<Offer, OfferId>>();

			var offer = OfferFactory.CreateWithSellerId(sellerId);
			this.SetupMockedRepositoryGetByIdAsync(repositoryMock, offer);

			var saveChangesReturnValue = 1;
			this.SetupMockedRepositorySaveChangesAsync(repositoryMock, saveChangesReturnValue);

			var commandHandler = new AcceptOfferCommandHandler(repositoryMock.Object);

			var command = new AcceptOfferCommand(productId.Value, sellerId.Value, buyerId.Value);
			var cancelationToken = new CancellationToken();

			// Act
			await commandHandler.Handle(command, cancelationToken);

			// Assert
			repositoryMock.Verify(rm => rm.GetByIdAsync(
				It.Is<OfferId>(o => o.BuyerId == buyerId && o.ProductId == productId))
			);
			Assert.Equal(OfferStatus.Accepted, offer.Status);
		}

		[Fact]
		public async Task Accept_offer_command_should_save_the_accepted_offer_to_database()
		{
			// Arrange
			var sellerId = new Id();

			var offerRepositoryMock = new Mock<IAggregateRepository<Offer, OfferId>>();

			var offer = OfferFactory.CreateWithSellerId(sellerId);
			this.SetupMockedRepositoryGetByIdAsync(offerRepositoryMock, offer);

			var saveChangesReturnValue = 1;
			this.SetupMockedRepositorySaveChangesAsync(offerRepositoryMock, saveChangesReturnValue);

			var commandHandler = new AcceptOfferCommandHandler(offerRepositoryMock.Object);

			var command = this.CreateAcceptOfferCommand(sellerId);
			var cancelationToken = new CancellationToken();

			// Act
			await commandHandler.Handle(command, cancelationToken);

			// Assert
			offerRepositoryMock.Verify(orm => orm.SaveChangesAsync(), Times.Once);
		}

		private async Task<Result> CallCommandHandlerHandleMethod(AcceptOfferCommandHandler handler)
		{
			var command = this.CreateAcceptOfferCommand();

			return await CallCommandHandlerHandeMethod(handler, command);
		}

		private async Task<Result> CallCommandHandlerHandeMethod(
			AcceptOfferCommandHandler handler, 
			AcceptOfferCommand command)
		{
			var cancelationToken = new CancellationToken();

			return await handler.Handle(command, cancelationToken);
		}

		private void SetupMockedRepositorySaveChangesAsync(
			Mock<IAggregateRepository<Offer, OfferId>> mock, 
			int returnValue)
		{
			mock.Setup(orm => orm.SaveChangesAsync())
				.Returns(Task.FromResult(returnValue));
		}

		private void SetupMockedRepositoryGetByIdAsync(
			Mock<IAggregateRepository<Offer, OfferId>> mock, 
			Offer returnValue)
		{
			mock
				.Setup(orm => orm.GetByIdAsync(It.IsAny<OfferId>()))
				.Returns(Task.FromResult(returnValue));
		}

		private AcceptOfferCommand CreateAcceptOfferCommand()
		{
			var sellerId = new Id();

			return this.CreateAcceptOfferCommand(sellerId);
		}

		private AcceptOfferCommand CreateAcceptOfferCommand(Id sellerId)
		{
			var productId = new Id();
			var buyerId = new Id();
			var command = new AcceptOfferCommand(productId.Value, sellerId.Value, buyerId.Value);

			return command;
		}
	}
}
