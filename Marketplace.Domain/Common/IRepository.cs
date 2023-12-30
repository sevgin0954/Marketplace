using Marketplace.Domain.SharedKernel;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Common
{
	public interface IRepository<T, TId> 
		where T : class
		where TId : Id
	{
		void Add(T aggregate);

		Task<T> GetByIdAsync(TId id);

		Task<IList<T>> GetAllAsync();

		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
