using MediatR;
using System.Collections.Generic;

namespace Marketplace.Domain.Common
{
	public class AggregateRoot : Entity
	{
        private readonly DomainEvents domainEvents = new ();
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
