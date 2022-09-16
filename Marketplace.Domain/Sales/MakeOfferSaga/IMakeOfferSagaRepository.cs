using Marketplace.Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.MakeOfferSagaNS
{
	internal interface IMakeOfferSagaRepository : ISagaRepository<MakeOfferSaga, MakeOfferSagaData>
	{
		Task<ICollection<MakeOfferSaga>> GetAllNotCompletedByIds(string sellerId, string buyerId);
	}
}