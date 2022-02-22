using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.SellerAggregate.Events;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.ProductOffersAggregate.Handlers
{
	public class AcceptOfferWhenOfferAcceptedEventHandler : IHandler<OfferAcceptedEvent>
	{
		private readonly IRepository<ProductOffers> offersRepository;

		public AcceptOfferWhenOfferAcceptedEventHandler(IRepository<ProductOffers> offersRepository)
		{
			this.offersRepository = offersRepository;
		}

		public async Task HandleAsync(OfferAcceptedEvent domainEvent)
		{
			var offers = await this.offersRepository.GetByIdAsync(domainEvent.ProductId);
			var productOffer = new ProductOffer(domainEvent.Quantity, domainEvent.BuyerId, domainEvent.ProductId);
			offers.AcceptOffer(productOffer);
		}
	}
}
