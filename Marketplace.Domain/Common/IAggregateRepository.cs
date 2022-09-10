using Marketplace.Domain.SharedKernel;

namespace Marketplace.Domain.Common
{
	public interface IAggregateRepository<T, TId> : IRepository<T> where T : AggregateRoot<TId> where TId : Id
	{
	}
}
