﻿using Marketplace.Domain.Sales.MakeOfferSagaNS.Commands;
using System;

namespace Marketplace.Tests.Sales.MakeOfferSagaSpecs.Commands
{
	internal static class StartMakingOfferCommandFactory
	{
		public static StartMakingOfferCommand Create()
		{
			var buyerId = Guid.NewGuid().ToString();
			var productId = Guid.NewGuid().ToString();
			var sellerId = Guid.NewGuid().ToString();
			var message = "Default message.";
			var quantity = 1;

			var notification = new StartMakingOfferCommand(buyerId, productId, sellerId, message, quantity);

			return notification;
		}
	}
}