using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Exceptions;
using Marketplace.Domain.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.IdentityAndAccess.UserAggregate.Commands
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
			private readonly IRepository<User, Id> userRepository;

			public RegisterUserCommandHandler(IRepository<User, Id> userRepository)
			{
				this.userRepository = userRepository;
			}

			public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
			{
				var userId = new Id();
				var password = new Password(request.PasswordHash, request.PasswordSalt);
				var newUser = new User(userId, request.UserName, request.Email, password);

				this.userRepository.Add(newUser);
				var isUserRegistered = await this.userRepository.SaveChangesAsync();
				if (isUserRegistered == 0)
					throw new NotPersistentException(nameof(newUser));

				return Result.Ok();
			}
		}
	}
}
