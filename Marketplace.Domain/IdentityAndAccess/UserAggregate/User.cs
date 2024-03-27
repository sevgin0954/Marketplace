using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;
using System;

namespace Marketplace.Domain.IdentityAndAccess.UserAggregate
{
    public class User : AggregateRoot
    {
        public User(Id id, string userName, Email email, Password password)
            : base(id)
        {
            this.UserName = userName;
            this.Email = email;
            this.IsAdmin = false;
            this.Password = password;
        }

        public string UserName { get; }

        public Email Email { get; }

        public bool IsAdmin { get; }

        public Password Password { get; private set; }

        public void ChangePassword(string newPasswordHash, string newSalt)
        {
            if (newPasswordHash == this.Password.Hash || newSalt == this.Password.Salt)
                throw new ArgumentException("The new hash and salt should not be the same!");

            this.Password = new Password(newPasswordHash, newSalt);
        }
    }
}
