using System.Threading.Tasks;

namespace Marketplace.Domain.Common
{
	public interface IRepository<T> where T : AggregateRoot
	{
		Task<T> GetByIdAsync(string id);

		Task<T> GetAll();

		Task<int> SaveChangesAsync();
	}
}
