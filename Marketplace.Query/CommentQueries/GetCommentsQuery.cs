using Marketplace.Query.ProductQueries;
using MediatR;

namespace Marketplace.Query.CommentQueries
{
	public class GetCommentsQuery : IRequest<IList<ProductDto>>
	{
	}
}
