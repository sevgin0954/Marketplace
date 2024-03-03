﻿using AutoMapper;
using Marketplace.API.Models.UserModels;
using Marketplace.Domain.IdentityAndAccess.UserAggregate.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Marketplace.API.Services;
using Marketplace.Query.UserQueries;

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
			var command = this.mapper.Map<RegisterUserCommand>(model);
			var result = await this.mediator.Send(command);
			if (result.IsSuccess)
				return this.Ok();
			else
				return this.BadRequest();
		}

		[Route("users/login")]
		[HttpPost]
		public async Task<IActionResult> Login(LoginBindingModel model)
		{
			var getPasswordSaltQuery = this.mapper.Map<GetPasswordSaltQuery>(model);
			var userPasswordSalt = await this.mediator.Send(getPasswordSaltQuery);

			if (userPasswordSalt == null)
				return this.NotFound();

			var userHashedPassword = PasswordHasher.ComputeHash(model.Password, userPasswordSalt);
			var loginRequest = this.mapper.Map<LogInUserQuery>(model);
			loginRequest.PasswordHash = userHashedPassword;

			var userId = await this.mediator.Send(loginRequest);
			if (userId != null)
			{
				var token = this.jwtTokenService.GenerateNewToken(userId);
				return this.Ok(token);
			}
			else
			{
				return this.NotFound();
			}
		}
	}
}