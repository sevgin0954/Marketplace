using System.Security.Claims;

namespace Marketplace.API.Services
{
	public interface IJwtTokenService
	{
		string GenerateNewToken(List<Claim> claims);
		Task<string> GetClaimFromTokenAsync(string jwtToken, string claimName);
	}
}
