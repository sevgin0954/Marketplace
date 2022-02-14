using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Domain.Common
{
	public interface IRepository<T> where T : AggregateRoot
	{
		Task<int> AddAsync(T element);

		Task<T> GetByIdAsync(string id);

		Task<IList<T>> GetAllAsync();

		Task<int> SaveChangesAsync();
	}
}
