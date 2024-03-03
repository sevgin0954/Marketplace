using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Exceptions;
using Marketplace.Domain.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.SellerAggregate.Commands
{
	internal class CheckIsBuyerBannedCommand : IRequest<Result>
	{
		public CheckIsBuyerBannedCommand(string sellerId, string buyerId)
		{
			this.SellerId = sellerId;
			this.BuyerId = buyerId;
		}

		public string SellerId { get; }

		public string BuyerId { get; }

		internal class CheckIsBuyerBannedCommandHandler : IRequestHandler<CheckIsBuyerBannedCommand, Result>
		{
			private readonly IRepository<Seller, Id> sellerRepository;

			public CheckIsBuyerBannedCommandHandler(IRepository<Seller, Id> sellerRepository)
			{
				this.sellerRepository = sellerRepository;
			}

			public async Task<Result> Handle(CheckIsBuyerBannedCommand request, CancellationToken cancellationToken)
			{
				var sellerId = new Id(request.SellerId);
				var seller = await this.sellerRepository.GetById(sellerId);
				if (seller == null)
				{
					throw new NotFoundException(nameof(seller));
				}

				var buyerId = new Id(request.BuyerId);
				seller.CheckIsBuyerBannedEventCheck(buyerId);

				return Result.Ok();
			}
		}
	}
}
