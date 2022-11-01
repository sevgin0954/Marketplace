using Marketplace.Domain.Sales.MakeOfferSagaNS;
using Marketplace.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Marketplace.Domain.Common
{
	internal interface ISagaDataRepository<TSagaData, TSagaId> : IRepository<TSagaData, TSagaId>
		where TSagaData : SagaData
		where TSagaId : Id
	{
		Task<ICollection<MakeOfferSagaData>> FindAsync(Expression<Func<TSagaData, bool>> predicate);
	}
}
