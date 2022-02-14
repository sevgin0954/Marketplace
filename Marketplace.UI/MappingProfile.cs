using AutoMapper;
using SalesProductEntity = Marketplace.Infrastructure.Sales.ProductPersistence.Product;
using Marketplace.UI.Models.HomeModels;
using SalesProductAggregate = Marketplace.Domain.Sales.ProductAggregate.Product;
using Marketplace.Infrastructure.Sales.ProductPersistence;
using Marketplace.Domain.Sales.ProductAggregate;

namespace Marketplace.UI
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			this.CreateMap<SalesProductAggregate, ProductViewModel>();

			this.CreateMap<ProductStatus, Status>()
				.ConstructUsing(src => new Status(src.ToString()));

			this.CreateMap<SalesProductAggregate, SalesProductEntity>()
				.ForMember(
					dest => dest.Status,
					opt => opt.MapFrom(src => new Status("asaas"))
				).ReverseMap();
		}
	}
}
