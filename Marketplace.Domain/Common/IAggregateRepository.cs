using Marketplace.Domain.SharedKernel;

namespace Marketplace.Domain.Common
{
	public interface IAggregateRepository<T, TId> : IRepository<T, TId> where T : AggregateRoot<TId> where TId : Id
	{
	}
}