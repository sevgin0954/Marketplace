using Marketplace.Domain.Sales.MakeOfferSagaNS;
using Marketplace.Domain.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Domain.Common
{
	internal interface ISagaDataRepository<TSagaData, TSagaId> : IRepository<TSagaData, TSagaId>
		where TSagaData : SagaData
		where TSagaId : Id
	{
		Task<ICollection<MakeOfferSaga>> GetAllNotCompletedByIds(string sellerId, string buyerId);
	}
}
