﻿using System.Security.Cryptography;
using System.Text;

namespace Marketplace.API.Services
{
	public static class PasswordHasher
	{
		public static string ComputeHash(string password, string salt, int iteration = 2)
		{
			if (iteration <= 0) return password;

			var passwordSalt = $"{password}{salt}";
			var sha256 = SHA256.Create();
			var byteValue = Encoding.UTF8.GetBytes(passwordSalt);
			var byteHash = sha256.ComputeHash(byteValue);
			var hash = Convert.ToBase64String(byteHash);

			return ComputeHash(hash, salt, iteration - 1);
		}

		public static string GenerateSalt()
		{
			using var rng = RandomNumberGenerator.Create();
			var byteSalt = new byte[16];
			rng.GetBytes(byteSalt);
			var salt = Convert.ToBase64String(byteSalt);
			return salt;
		}
	}
}
