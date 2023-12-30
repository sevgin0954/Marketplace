using Marketplace.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Marketplace.Domain.Common
{
	public interface ISagaDataRepository<TSagaData, TSagaId> : IRepository<TSagaData, TSagaId>
		where TSagaData : SagaData
		where TSagaId : Id
	{
		Task<ICollection<TSagaData>> FindAsync(Expression<Func<TSagaData, bool>> predicate);
	}
}
