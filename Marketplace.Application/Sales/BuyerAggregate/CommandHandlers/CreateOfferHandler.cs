using CSharpFunctionalExtensions;
using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.BuyerAggregate;
using Marketplace.Domain.Sales.BuyerAggregate.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Application.Sales.BuyerAggregate.CommandHandlers
{
	public class CreateOfferHandler : IRequestHandler<CreateOfferCommand, Result>
	{
		private readonly IRepository<Buyer> buyerRepository;

		public CreateOfferHandler(IRepository<Buyer> buyerRepository)
		{
			this.buyerRepository = buyerRepository;
		}

		public async Task<Result> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
		{
			var buyer = await this.buyerRepository.GetByIdAsync(request.BuyerId);

			if (buyer == null)
			{
				return Result.Failure("Buyer not found");
			}

			buyer.StartAddingPendingOffer(request.ProductId, request.);

			await buyerRepository.SaveChangesAsync();

			return Result.Success();
		}
	}
}
