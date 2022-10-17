using Marketplace.Domain.SharedKernel;
using MediatR;
using System.Collections.Generic;
// TODO: Implement base value object
// TODO: Remove IREADONLYCOLLECTIONS FROM AGGREGATES, THEY ARE ONLY FOR WRITING
namespace Marketplace.Domain.Common
{
	public abstract class AggregateRoot<TId> : Entity<TId> where TId : Id
    {
        public AggregateRoot(TId id)
            : base(id) { }

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
