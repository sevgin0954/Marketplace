using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Marketplace.Domain.Common
{
	public interface ISagaRepository<TSaga, TData> 
		where TSaga : Saga<TData> 
		where TData : class
	{
		Task<int> AddAsync(TSaga element);

		Task<TSaga> GetByIdAsync(string id);

		Task<IList<TSaga>> FindAsync(Expression<Func<TSaga, bool>> predicate);

		Task<int> SaveChangesAsync();
	}
}
