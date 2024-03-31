namespace Marketplace.API.Services
{
	public interface IJwtTokenService
	{
		string GenerateNewToken(string userId);
		Task<string> GetClaimFromTokenAsync(string jwtToken, string claimName);
	}
}
