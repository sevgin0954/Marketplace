namespace Marketplace.API.Attributes
{
	public class MapFromJwtTokenAttribute : Attribute
	{
		public MapFromJwtTokenAttribute(string jwtTokenClaimName)
		{
			this.JwtTokenClaimName = jwtTokenClaimName;
		}

		public string JwtTokenClaimName { get; }
	}
}
