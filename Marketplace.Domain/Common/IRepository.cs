using Marketplace.Domain.SharedKernel;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Common
{
	public interface IRepository<T, TId> 
		where T : class
		where TId : Id
	{
		void Add(T aggregate);

		void Remove(TId id);

		IQueryable<T> GetById(TId id);

		IQueryable<T> GetAll();

		Task<bool> CheckIfExistAsync(TId id);

		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
