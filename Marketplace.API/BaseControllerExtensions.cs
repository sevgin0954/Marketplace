using Marketplace.API.Controllers;

namespace Marketplace.API
{
	public static class BaseControllerExtensions
	{
		public static string GetCurrentUserId(this BaseController baseController)
		{
			var userIdClaim = baseController.HttpContext.User.Claims
				.Where(c => c.Type == GlobalConstants.JWT_TOKEN_ID_CLAIM_NAME)
				.FirstOrDefault();

			return userIdClaim?.Value;
		}
	}
}
