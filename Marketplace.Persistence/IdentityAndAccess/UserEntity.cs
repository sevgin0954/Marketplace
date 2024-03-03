﻿namespace Marketplace.Persistence.IdentityAndAccess
{
	public class UserEntity
	{
		public string Id { get; set; }

		public string UserName { get; set; }

		public string Email { get; set; }

		public bool IsAdmin { get; set; }

		public string PasswordHash { get; set; }

		public string PasswordSalt { get; set; }
	}
}
