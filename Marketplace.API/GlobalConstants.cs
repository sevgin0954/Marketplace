namespace Marketplace.API
{
	public class GlobalConstants
	{
		public const string AUTHENTICATION_METHOD = "Bearer";
		public const int JWT_TOKEN_LIFETIME_MINUTES = 120;
		public const string JWT_TOKEN_COOKIE_KEY = "JWT_TOKEN_NAME";
		public const string JWT_TOKEN_ID_CLAIM_NAME = "id";
	}
}
