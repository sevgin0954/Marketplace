using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Domain.IdentityAndAccess.UserAggregate
{
    public class User : AggregateRoot<Id>
    {
        public User(Id id, string userName, Email email)
            : base(id)
        {
            UserName = userName;
            Email = email;
        }

        public string UserName { get; }

        public Email Email { get; }
    }
}
