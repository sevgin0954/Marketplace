using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.SellerAggregate.Events;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.BuyerAggregate.Handlers
{
	public class BuyerMoveOfferToAcceptedWhenOfferAcceptedEventHandler : IHandler<OfferAcceptedEvent>
	{
		private readonly IRepository<Buyer> buyerRepository;

		public BuyerMoveOfferToAcceptedWhenOfferAcceptedEventHandler(
			IRepository<Buyer> buyerRepository)
		{
			this.buyerRepository = buyerRepository;
		}

		public async Task HandleAsync(OfferAcceptedEvent domainEvent)
		{
			var buyer = await this.buyerRepository.GetByIdAsync(domainEvent.BuyerId);
			buyer.MoveOfferToAccepted(domainEvent.ProductId);

			await this.buyerRepository.SaveChangesAsync();
		}
	}
}
