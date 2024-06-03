using Marketplace.API.Attributes;
using Marketplace.API.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace Marketplace.API.Filters
{
	public class MapFromJwtTokenActionFilter : IActionFilter
	{
		private readonly IJwtTokenService jwtTokenService;

		public MapFromJwtTokenActionFilter(IJwtTokenService jwtTokenService)
		{
			this.jwtTokenService = jwtTokenService;
		}

		public void OnActionExecuted(ActionExecutedContext context) { }

		public void OnActionExecuting(ActionExecutingContext context)
		{
			var models = context.ActionArguments;

			foreach (var currentModel in models)
			{
				var properties = models.SelectMany(m => m.Value.GetType().GetProperties()).ToList();
				var propertiesWithJwtAttribute = properties.Where(p => Attribute.IsDefined(p, typeof(MapFromJwtTokenAttribute)));

				this.MapJwtValueToPropertiesAsync(currentModel.Value, propertiesWithJwtAttribute, context)
					.GetAwaiter()
					.GetResult();
			}
		}

		private async Task MapJwtValueToPropertiesAsync(object parentClass, IEnumerable<PropertyInfo> properties, ActionExecutingContext context)
		{
			foreach (var currentProperty in properties)
			{
				var jwtAttribute = currentProperty.GetCustomAttribute<MapFromJwtTokenAttribute>();

				var authorizationHeaderValue = context.HttpContext.Request.Headers.Authorization;
				var jwtToken = authorizationHeaderValue
					.ToString()
					.Remove(0, GlobalConstants.AUTHENTICATION_METHOD.Length)
					.Trim();

				var tokenClaim = await jwtTokenService.GetClaimFromTokenAsync(jwtToken, jwtAttribute.JwtTokenClaimName);

				currentProperty.SetValue(parentClass, tokenClaim);
				;
			}
		}
	}
}
