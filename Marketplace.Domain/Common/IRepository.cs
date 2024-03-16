using Marketplace.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Common
{
	public interface IRepository<T, TId> 
		where T : AggregateRoot
		where TId : Id
	{
		void Add(T aggregate);

		Task MarkAsDeleted(TId id);

		Task<T> GetByIdAsync(TId id);

		ICollection<T> GetAll();

		Task<ICollection<T>> FindAsync(Expression<Func<T, bool>> predicate);

		Task<bool> CheckIfExistAsync(TId id);

		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}