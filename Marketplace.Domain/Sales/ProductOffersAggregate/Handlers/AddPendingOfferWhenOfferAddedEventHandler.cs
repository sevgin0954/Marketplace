using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.SellerAggregate.Events;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.ProductOffersAggregate.Handlers
{
	public class AddPendingOfferWhenOfferAddedEventHandler : IHandler<OfferAddedEvent>
	{
		private readonly IRepository<ProductOffers> offersRepository;

		public AddPendingOfferWhenOfferAddedEventHandler(IRepository<ProductOffers> offersRepository)
		{
			this.offersRepository = offersRepository;
		}

		public async Task HandleAsync(OfferAddedEvent domainEvent)
		{
			var offers = await this.offersRepository.GetByIdAsync(domainEvent.ProductId);
			var productOffer = new ProductOffer(domainEvent.Quantity, domainEvent.BuyerId, domainEvent.ProductId);
			offers.AddPendingOffer(productOffer);
		}
	}
}
