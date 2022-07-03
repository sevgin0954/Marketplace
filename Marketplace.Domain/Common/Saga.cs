using MediatR;
using System.Collections.Generic;

namespace Marketplace.Domain.Common
{
	public abstract class Saga<TData> where TData : class
	{
        private readonly DomainEvents domainEvents = new();

        protected TData Data { get; set; }

		protected bool IsCompleted { get; set; }

        public virtual IReadOnlyList<INotification> DomainEvents => domainEvents.Events;

        protected virtual void AddDomainEvent(INotification newEvent)
        {
            this.domainEvents.Add(newEvent);
        }

        public virtual void ClearEvents()
        {
            this.domainEvents.ClearEvents();
        }
    }
}
