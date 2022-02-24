using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.SellerAggregate.Events;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.ProductOffersAggregate.Handlers
{
	public class AddAcceptedOfferWhenOfferAcceptedEventHandler : IHandler<OfferAcceptedEvent>
	{
		private readonly IRepository<ProductOffers> offersRepository;

		public AddAcceptedOfferWhenOfferAcceptedEventHandler(IRepository<ProductOffers> offersRepository)
		{
			this.offersRepository = offersRepository;
		}

		public async Task HandleAsync(OfferAcceptedEvent domainEvent)
		{
			var offers = await this.offersRepository.GetByIdAsync(domainEvent.ProductId);
			var productOffer = new ProductOffer(domainEvent.Quantity, domainEvent.BuyerId, domainEvent.ProductId);
			offers.AddAcceptedOffer(productOffer);
		}
	}
}
