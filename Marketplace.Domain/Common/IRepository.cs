using Marketplace.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Marketplace.Domain.Common
{
	public interface IRepository<T, TId> where TId : Id
	{
		Task<int> AddAsync(T element);

		Task<T> GetByIdAsync(TId id);

		Task<IList<T>> GetAllAsync();

		Task<IList<T>> FindAsync(Expression<Func<T, bool>> predicate);

		Task<int> SaveChangesAsync();
	}
}
