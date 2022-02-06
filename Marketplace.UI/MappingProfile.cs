using AutoMapper;
using Marketplace.Domain.Sales.ProductAggregate;
using Marketplace.UI.Models.HomeModels;

namespace Marketplace.UI
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			this.CreateMap<Product, ProductViewModel>();
		}
	}
}
