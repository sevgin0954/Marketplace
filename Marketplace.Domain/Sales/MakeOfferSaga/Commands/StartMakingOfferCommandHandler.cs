﻿using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.MakeOfferSagaNS.Commands
{
	public class StartMakingOfferCommand : IRequest<Result>
	{
		const string OFFER_ALREADY_EXISTS = "Offer was already made to his product!";

		public StartMakingOfferCommand(string buyerId, string productId, string sellerId, string message, int quantity)
		{
			this.BuyerId = buyerId;
			this.ProductId = productId;
			this.SellerId = sellerId;
			this.Message = message;
			this.Quantity = quantity;
		}

		public string BuyerId { get; }

		public string ProductId { get; }

		public string SellerId { get; }

		public string Message { get; }

		public int Quantity { get; }

		internal class StartMakingOfferCommandHandler : IRequestHandler<StartMakingOfferCommand, Result>
		{
			private readonly ISagaRepository<MakeOfferSaga, MakeOfferSagaData> makeOfferSagaRepository;
			private readonly Mediator mediator;

			internal StartMakingOfferCommandHandler(
				ISagaRepository<MakeOfferSaga, MakeOfferSagaData> makeOfferSagaRepository,
				Mediator mediator)
			{
				this.makeOfferSagaRepository = makeOfferSagaRepository;
				this.mediator = mediator;
			}

			public async Task<Result> Handle(StartMakingOfferCommand notification, CancellationToken cancellationToken)
			{
				Result result;

				var buyerId = new Id(notification.BuyerId);
				var productId = new Id(notification.ProductId);
				var makeOfferSagaId = new MakeOfferSagaId(buyerId, productId);

				var makeOfferSaga = await this.makeOfferSagaRepository.GetByIdAsync(makeOfferSagaId);
				if (makeOfferSaga == null || makeOfferSaga.IsCompleted)
				{
					var sellerId = new Id(notification.SellerId);
					var saga = this.CreateSaga(
						buyerId,
						productId,
						sellerId,
						notification.Message, 
						notification.Quantity
					);

					await saga.StartSagaAsync();

					var rowsChanged = await this.makeOfferSagaRepository.AddAsync(saga);
					if (rowsChanged == 0)
						throw new InvalidOperationException();

					result = Result.Ok();
				}
				else
				{
					result = Result.Fail(OFFER_ALREADY_EXISTS);
				}

				return result;
			}

			private MakeOfferSaga CreateSaga(Id buyerId, Id productId, Id sellerId, string message, int quantity)
			{
				var sagaData = new MakeOfferSagaData(buyerId, productId, sellerId, message, quantity);
				var sagaId = new MakeOfferSagaId(buyerId, productId);
				var saga = new MakeOfferSaga(sagaData, sagaId, this.mediator);

				return saga;
			}
		}
	}
}