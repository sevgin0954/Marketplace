namespace Marketplace.Domain.Common
{
	public interface IAggregateRepository<T> : IRepository<T> where T : AggregateRoot
	{
	}
}
