﻿using Marketplace.Domain.Sales.SellerAggregate.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.MakeOfferSagaNS.EventHandlers
{
	internal class TransitionWhenBuyerIsBannedEventHandler : INotificationHandler<BuyerWasBannedEvent>
	{
		private readonly IMakeOfferSagaRepository makeOfferSagaRepository;

		public TransitionWhenBuyerIsBannedEventHandler(
			IMakeOfferSagaRepository makeOfferSagaRepository)
		{
			this.makeOfferSagaRepository = makeOfferSagaRepository;
		}

		public async Task Handle(BuyerWasBannedEvent notification, CancellationToken cancellationToken)
		{
			var makeOfferSaga = await makeOfferSagaRepository
				.GetAllNotCompletedByIds(notification.BuyerId, notification.SellerId);
			
			foreach (var currentMakeOfferSaga in makeOfferSaga)
			{
				await currentMakeOfferSaga.TransitionAsync(notification);
				await this.makeOfferSagaRepository.SaveChangesAsync();
			}
		}
	}
}
