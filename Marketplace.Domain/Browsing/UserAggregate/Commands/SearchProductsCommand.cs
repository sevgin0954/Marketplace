using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Exceptions;
using Marketplace.Domain.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Browsing.UserAggregate.Commands
{
	public class SearchProductsCommand : IRequest<Result>
	{
		public SearchProductsCommand(string userId, string[] matchingKeywords)
		{
			this.UserId = userId;
			this.MatchingKeywords = matchingKeywords;
		}

		public string UserId { get; }

		public string[] MatchingKeywords { get; }

		internal class SearchProductsCommandHandler : IRequestHandler<SearchProductsCommand, Result>
		{
			private readonly IRepository<User, Id> userRepository;

			public SearchProductsCommandHandler(IRepository<User, Id> userRepository)
			{
				this.userRepository = userRepository;
			}

			public async Task<Result> Handle(SearchProductsCommand request, CancellationToken cancellationToken)
			{
				var userId = new Id(request.UserId);
				var user = await this.userRepository.GetByIdAsync(userId);

				if (user == null)
					throw new NotFoundException(nameof(user));

				var search = new Search(request.MatchingKeywords[0], request.MatchingKeywords[1], request.MatchingKeywords[2]);
				user.SearchProduct(search);

				var isUserPersistedSuccessfully = await this.userRepository.SaveChangesAsync(cancellationToken);
				if (isUserPersistedSuccessfully == false)
					throw new NotPersistentException(nameof(user));

				return Result.Ok();
			}
		}
	}
}
