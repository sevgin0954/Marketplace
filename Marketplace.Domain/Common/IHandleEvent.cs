using System.Threading.Tasks;

namespace Marketplace.Domain.Common
{
	public interface IHandleEvent<TEvent> where TEvent : class
	{
		Task Transition(TEvent message);
	}
}
