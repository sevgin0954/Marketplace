using Marketplace.Domain.SharedKernel;

namespace Marketplace.Domain.Common
{
	public class SagaData
	{
		public SagaData(Id id)
		{
			this.Id = id;
		}

		public Id Id { get; }

		public bool IsSagaStarted { get; set; }
	}
}
