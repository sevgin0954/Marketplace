using Marketplace.Domain.SharedKernel;

namespace Marketplace.Domain.Common
{
	public interface ISagaRepository<TSaga, TSagaData> : IRepository<TSaga, Id>
		where TSaga : Saga<TSagaData>
	{
	}
}
