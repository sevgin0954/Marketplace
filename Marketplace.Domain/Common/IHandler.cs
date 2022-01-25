using System.Threading.Tasks;

namespace Marketplace.Domain.Common
{
	public interface IHandler<T> where T : IDomainEvent
	{
		Task HandleAsync(T domainEvent);
	}
}
