using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Marketplace.API.Services
{
	public class JwtTokenService : IJwtTokenService
	{
		private readonly IConfiguration config;

		public JwtTokenService(IConfiguration config)
		{
			this.config = config;
		}

		public string GenerateNewToken(string userId)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.config[ConfigurationConstants.AuthClientSecretKey]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				this.config[ConfigurationConstants.AuthIssuerKey],
				null,
				new List<Claim>() { new Claim("id", userId) },
				expires: DateTime.Now.AddMinutes(GlobalConstants.JwtTokenLifetimeMinutes),
				signingCredentials: credentials
			);

			var tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

			return tokenAsString;
		}

		public async Task<string> GetClaimFromTokenAsync(string jwtToken, string claimName)
		{
			var secretKeyBytes = Encoding.UTF8.GetBytes(this.config[ConfigurationConstants.AuthClientSecretKey]);
			var validationParameters = new TokenValidationParameters()
			{
				ValidateLifetime = true,
				ValidateAudience = false,
				ValidIssuer = this.config[ConfigurationConstants.AuthIssuerKey],
				IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes)
			};

			var jwtTokenHandler = new JwtSecurityTokenHandler();
			var tokenValidationResult = await jwtTokenHandler.ValidateTokenAsync(jwtToken, validationParameters);

			if (tokenValidationResult.IsValid == false)
				throw new ArgumentException("Invalid jwt token!", nameof(jwtToken));

			var doesClaimExist = tokenValidationResult.Claims.Any(c => c.Key == claimName);
			if (doesClaimExist == false)
				throw new ArgumentException($"Token does not have claim '{claimName}'", nameof(claimName));

			return tokenValidationResult.Claims[claimName] as string;
		}
	}
}
