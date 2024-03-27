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
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.config["Auth:ClientSecret"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				this.config["Auth:Issuer"],
				null,
				new List<Claim>() { new Claim("id", userId) },
				expires: DateTime.Now.AddMinutes(120),
				signingCredentials: credentials
			);

			var tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

			return tokenAsString;
		}
	}
}
