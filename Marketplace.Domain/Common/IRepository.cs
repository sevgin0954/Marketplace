using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Marketplace.Domain.Common
{
	public interface IRepository<T>
	{
		Task<int> AddAsync(T element);

		Task<T> GetByIdAsync(string id);

		Task<IList<T>> GetAllAsync();

		Task<IList<T>> FindAsync(Expression<Func<T, bool>> predicate);

		Task<int> SaveChangesAsync();
	}
}
