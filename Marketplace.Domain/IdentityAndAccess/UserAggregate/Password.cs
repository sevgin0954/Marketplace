using Marketplace.Domain.Common;

namespace Marketplace.Domain.IdentityAndAccess.UserAggregate
{
	public record Password : ValueObject
	{
		public Password(string hash, string salt)
		{
			this.Hash = hash;
			this.Salt = salt;
		}

		public string Hash { get; }

		public string Salt { get; }
	}
}
