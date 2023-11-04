using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Domain.IdentityAndAccess
{
	public class User : AggregateRoot<Id>
	{
		public User(Id id, string userName, Email email) 
			: base(id)
		{
			this.UserName = userName;
			this.Email = email;
		}

		public string UserName { get; }

		public Email Email { get; }
	}
}
