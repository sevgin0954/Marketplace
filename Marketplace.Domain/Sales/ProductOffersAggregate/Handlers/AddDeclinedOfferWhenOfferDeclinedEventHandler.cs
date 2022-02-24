using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.SellerAggregate.Events;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.ProductOffersAggregate.Handlers
{
	public class AddDeclinedOfferWhenOfferDeclinedEventHandler : IHandler<OfferDeclinedEvent>
	{
		private readonly IRepository<ProductOffers> offersRepository;

		public AddDeclinedOfferWhenOfferDeclinedEventHandler(IRepository<ProductOffers> offersRepository)
		{
			this.offersRepository = offersRepository;
		}

		public async Task HandleAsync(OfferDeclinedEvent domainEvent)
		{
			var offers = await this.offersRepository.GetByIdAsync(domainEvent.ProductId);
			var productOffer = new ProductOffer(domainEvent.Quantity, domainEvent.BuyerId, domainEvent.ProductId);
			offers.AddDeclinedOffer(productOffer);
		}
	}
}
