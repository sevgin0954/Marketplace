namespace Marketplace.Domain.Common
{
	public abstract class Saga<TData>
	{
		protected bool isCompleted;

		public Saga(Id id, TData data)
		{
			this.Data = data;
			this.Id = id;
		}

		public Id Id { get; }

		protected TData Data { get; }

		public bool IsCompleted => this.isCompleted;

		// TODO: Make protected
		public Result Result { get; set; }

	}
}
