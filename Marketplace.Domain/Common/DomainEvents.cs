using MediatR;
using System.Collections.Generic;

namespace Marketplace.Domain.Common
{
	public class DomainEvents
	{
        private readonly List<INotification> events = new List<INotification>();
        public virtual IReadOnlyList<INotification> Events => events;

        public virtual void Add(INotification newEvent)
        {
            this.events.Add(newEvent);
        }

        public virtual void ClearEvents()
        {
            this.events.Clear();
        }
    }
}
