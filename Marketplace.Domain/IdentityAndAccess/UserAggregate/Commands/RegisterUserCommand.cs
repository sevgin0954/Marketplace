using Marketplace.Domain.Common;
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

		internal class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result>
		{
			public Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
			{
				throw new System.NotImplementedException();
			}
		}
	}
}
