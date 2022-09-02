using Marketplace.Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.MakeOfferSaga
{
	public interface IMakeOfferSagaRepository : ISagaRepository<MakeOffer, MakeOfferSagaData>
	{
		Task<ICollection<MakeOffer>> GetAllNotCompletedByIds(string sellerId, string buyerId);
	}
}
