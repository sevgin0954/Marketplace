namespace Marketplace.API.Services
{
	public interface IJwtTokenService
	{
		string GenerateNewToken(string userId);
	}
}
