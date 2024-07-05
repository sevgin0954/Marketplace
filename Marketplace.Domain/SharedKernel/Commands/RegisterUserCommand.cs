using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Exceptions;
using Marketplace.Domain.IdentityAndAccess.UserAggregate;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using BrowsingUser = Marketplace.Domain.Browsing.UserAggregate;
using IdentityUser = Marketplace.Domain.IdentityAndAccess.UserAggregate;
using System;

namespace Marketplace.Domain.SharedKernel.Commands
{
	public class RegisterUserCommand : IRequest<Result>
	{
		public RegisterUserCommand(string userName, Email email)
		{
			this.UserName = userName;
			this.Email = email;
		}

		public string UserName { get; }

		public Email Email { get; }

		public string PasswordHash { get; set; }

		public string PasswordSalt { get; set; }

		internal class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result>
		{
			private readonly IRepository<IdentityUser.User, Id> identityUserRepository;
			private readonly IRepository<BrowsingUser.User, Id> browsingUserRepository;

			public RegisterUserCommandHandler(
				IRepository<IdentityUser.User, Id> identityUserRepository,
				IRepository<BrowsingUser.User, Id> browsingUserRepository)
			{
				this.identityUserRepository = identityUserRepository;
				this.browsingUserRepository = browsingUserRepository;
			}

			// TODO: Move to saga
			public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
			{
				var userId = new Id();
				var password = new Password(request.PasswordHash, request.PasswordSalt);
				var newIdentityUser = new IdentityUser.User(userId, request.UserName, request.Email, password);

				this.identityUserRepository.Add(newIdentityUser);
				var isUserRegisteredSuccessfully = await this.identityUserRepository.SaveChangesAsync(cancellationToken);
				if (isUserRegisteredSuccessfully == false)
					throw new NotPersistentException(nameof(newIdentityUser));

				var newBrowsingUser = new BrowsingUser.User(userId);
				this.browsingUserRepository.Add(newBrowsingUser);

				var isBrowsingUserCreatedSuccessfully = await this.browsingUserRepository.SaveChangesAsync(cancellationToken);
				if (isBrowsingUserCreatedSuccessfully == false)
					throw new NotPersistentException(nameof(newBrowsingUser));

				return Result.Ok();
			}
		}
	}
}