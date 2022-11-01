using Marketplace.Domain.Common.Exceptions;
using Marketplace.Domain.SharedKernel;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Marketplace.Domain.Common
{
	internal abstract class Saga<TData> where TData : SagaData
	{
		const string SAGA_IS_ALREADY_STARTED_EXCEPTION_MESSAGE = "The saga is already started!";

		private readonly IMediator mediator;

		public Saga(TData data, IMediator mediator)
		{
			this.Data = data;
			this.mediator = mediator;
		}

		protected async Task SendCommandOrCompleteAsync(IRequest<Result> request)
		{
			var result = await this.mediator.Send(request);
			if (result.IsFailure)
			{
				this.MarkAsComplete();
				this.Result = Result.Fail(result.ErrorMessage);
			}	
		}

		protected async Task SendCommandOrThrowExceptionAsync(IRequest<Result> request)
		{
			var result = await this.mediator.Send(request);
			if (result.IsFailure)
			{
				throw new CommandNotSendException(result.GetType().Name, result.ErrorMessage);
			}
		}

		public async Task StartSagaAsync()
		{
			if (this.Data.IsSagaStarted)
				throw new InvalidOperationException(SAGA_IS_ALREADY_STARTED_EXCEPTION_MESSAGE);

			await this.OnStartSagaAsync();

			this.Data.IsSagaStarted = true;
		}

		protected abstract Task OnStartSagaAsync();

		public async Task CompleteSagaAsync()
		{
			if (this.IsCompleted)
				throw new InvalidOperationException("Can't finish completed saga!");

			await this.OnCompleteSagaAsync();
		}

		protected abstract Task OnCompleteSagaAsync();

		protected void MarkAsComplete()
		{
			this.IsCompleted = true;
		}

		protected TData Data { get; }

		public bool IsCompleted { get; private set; }

		public Result Result { get; protected set; }
	}
}