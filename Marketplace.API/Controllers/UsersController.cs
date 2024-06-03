using AutoMapper;
using Marketplace.API.Models.UserModels;
using Marketplace.Domain.IdentityAndAccess.UserAggregate.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Marketplace.API.Services;
using Marketplace.Query.UserQueries;
using System.Security.Claims;
using Marketplace.Domain.IdentityAndAccess.UserAggregate;

namespace Marketplace.API.Controllers
{
	[Route("users")]
	public class UsersController : Controller
	{
		private readonly IMediator mediator;
		private readonly IMapper mapper;
		private readonly IJwtTokenService jwtTokenService;

		public UsersController(
			IMediator mediator, 
			IMapper mapper,
			IJwtTokenService jwtTokenService)
		{
			this.mediator = mediator;
			this.mapper = mapper;
			this.jwtTokenService = jwtTokenService;
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterUserBindingModel model)
		{
			var passwordSalt = PasswordHasher.GenerateSalt();
			var hashedPassword = PasswordHasher.ComputeHash(model.Password, passwordSalt);

			var command = this.mapper.Map<RegisterUserCommand>(model);
			command.PasswordHash = hashedPassword;
			command.PasswordSalt = passwordSalt;


			var result = await this.mediator.Send(command);
			if (result.IsSuccess)
				return this.Ok();
			else
				return this.BadRequest();
		}

		[Route("login")]
		[HttpPost]
		public async Task<IActionResult> Login(LoginBindingModel model)
		{
			var getPasswordSaltQuery = this.mapper.Map<GetPasswordSaltQuery>(model);
			var passwordSalt = await this.mediator.Send(getPasswordSaltQuery);

			if (passwordSalt == null)
				return this.BadRequest("Invalid email address!");

			var hashedPassword = PasswordHasher.ComputeHash(model.Password, passwordSalt);
			var loginRequest = this.mapper.Map<LogInUserQuery>(model);
			loginRequest.PasswordHash = hashedPassword;

			var user = await this.mediator.Send(loginRequest);
			if (user != null)
			{
				var claims = new List<Claim>() { new Claim(GlobalConstants.JWT_TOKEN_ID_CLAIM_NAME, user.Id) };
				var token = this.jwtTokenService.GenerateNewToken(claims);

				var loginModel = this.mapper.Map<LoginViewModel>(user);
				loginModel.Token = token;

				return this.Ok(loginModel);
			}
			else
			{
				return this.BadRequest("Invalid password!");
			}
		}
	}
}