using Marketplace.API.Services;
using System.Security.Claims;
using System.Security.Principal;

namespace Marketplace.API
{
	public class SetIdentityMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly IJwtTokenService jwtTokenService;

		public SetIdentityMiddleware(RequestDelegate next, IJwtTokenService jwtTokenService)
		{
			_next = next;
			this.jwtTokenService = jwtTokenService;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			var token = context.Request.Cookies.Keys.Where(k => k == GlobalConstants.JWT_TOKEN_COOKIE_KEY).FirstOrDefault();
			if (token != null)
			{
				var id = await this.jwtTokenService.GetClaimFromTokenAsync(token, GlobalConstants.JWT_TOKEN_ID_CLAIM_NAME);
				var userRoles = new string[0];
				var idClaim = new Claim(GlobalConstants.JWT_TOKEN_ID_CLAIM_NAME, id);
				var user = new GenericPrincipal(new ClaimsIdentity(new[] { idClaim }), userRoles);

				context.User = user;
			}


			await _next(context);
		}
	}

	public static class SetIdentityMiddlewareExtensions
	{
		public static IApplicationBuilder UseSetIdentity(
			this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<SetIdentityMiddleware>();
		}
	}
}