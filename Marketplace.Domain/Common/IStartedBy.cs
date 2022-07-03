using MediatR;

namespace Marketplace.Domain.Common
{
	public interface IStartedBy<TEvent> where TEvent : INotification
	{
		void TransitionFromStart(TEvent message);
	}
}
