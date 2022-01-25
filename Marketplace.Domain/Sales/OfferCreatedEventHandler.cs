using Marketplace.Domain.Common;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales
{
	public class OfferCreatedEventHandler : IHandler<OfferCreatedEvent>
	{
		private readonly IRepository<Seller> sellerRepository;

		public OfferCreatedEventHandler(IRepository<Seller> sellerRepository)
		{
			this.sellerRepository = sellerRepository;
		}

		public async Task HandleAsync(OfferCreatedEvent domainEvent)
		{
			var seller = await this.sellerRepository.GetByIdAsync(domainEvent.SellerId);
			var offer = new Offer(domainEvent.BuyerId, domainEvent.ProductId, seller.Id);
			seller.AddOffer(offer);
		}
	}
}
