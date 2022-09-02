namespace Marketplace.Domain.Common
{
	public interface ISagaRepository<TSaga, TSagaData> : IRepository<TSaga>
		where TSaga : Saga<TSagaData>
	{
	}
}
