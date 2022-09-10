using Marketplace.Domain.SharedKernel;

namespace Marketplace.Domain.Common
{
	public abstract class Saga<TData>
	{
		public Saga(Id id, TData data)
		{
			this.Data = data;
			this.Id = id;
		}

		public Id Id { get; }

		protected TData Data { get; }

		public bool IsCompleted { get; protected set; }

		public Result Result { get; protected set; }

	}
}
