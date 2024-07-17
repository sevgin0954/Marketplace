using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Browsing.ProductAggregate.Commands
{
	public class RecordProductViewCommand : IRequest<bool>
	{
		public RecordProductViewCommand(string productId)
		{
			this.ProductId = productId;
		}

		public string ProductId { get; }

		internal class RecordProductViewCommandHandler : IRequestHandler<RecordProductViewCommand, bool>
		{
			private readonly IRepository<Product, Id> productRepository;

			public RecordProductViewCommandHandler(IRepository<Product, Id> productRepository)
			{
				this.productRepository = productRepository;
			}

			public async Task<bool> Handle(RecordProductViewCommand request, CancellationToken cancellationToken)
			{
				var productId = new Id(request.ProductId);
				var product = await this.productRepository.GetByIdAsync(productId);

				product.RegisterView();

				this.productRepository.Update(product);
				var isProductPersistedSuccesfully = await this.productRepository.SaveChangesAsync(cancellationToken);

				return isProductPersistedSuccesfully;
			}
		}
	}
}