using Marketplace.Domain.SharedKernel;
using System;
using System.Threading.Tasks;

namespace Marketplace.Domain.Common
{
	public abstract class Saga<TData>
	{
		const string SAGA_IS_ALREADY_STARTED_EXCEPTION_MESSAGE = "The saga is already started!";

		private bool isStarted;

		public Saga(Id id, TData data)
		{
			this.Data = data;
			this.Id = id;
			this.isStarted = false;
		}

		public async Task StartSagaAsync()
		{
			if (this.isStarted)
				throw new InvalidOperationException(SAGA_IS_ALREADY_STARTED_EXCEPTION_MESSAGE);

			await this.OnStartSagaAsync();

			this.isStarted = true;
		}

		public abstract Task OnStartSagaAsync();

		public Id Id { get; }

		protected TData Data { get; }

		public bool IsCompleted { get; protected set; }

		public Result Result { get; protected set; }
	}
}