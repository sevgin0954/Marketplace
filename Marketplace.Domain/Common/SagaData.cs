using Marketplace.Domain.SharedKernel;

namespace Marketplace.Domain.Common
{
	public class SagaData : AggregateRoot
	{
		public SagaData(Id id)
			: base(id) { }

		public bool IsSagaStarted { get; set; }
	}
}
