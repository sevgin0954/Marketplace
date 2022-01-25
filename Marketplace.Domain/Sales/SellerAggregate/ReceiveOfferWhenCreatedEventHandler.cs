using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.Events;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.SellerAggregate
{
	public class ReceiveOfferWhenCreatedEventHandler : IHandler<OfferCreatedEvent>
	{
		private readonly IRepository<Seller> sellerRepository;

		public ReceiveOfferWhenCreatedEventHandler(IRepository<Seller> sellerRepository)
		{
			this.sellerRepository = sellerRepository;
		}

		public async Task HandleAsync(OfferCreatedEvent domainEvent)
		{
			var seller = await this.sellerRepository.GetByIdAsync(domainEvent.SellerId);
			var offer = new Offer(domainEvent.BuyerId, domainEvent.ProductId, seller.Id);
			seller.ReceiveOffer(offer);
		}
	}
}
