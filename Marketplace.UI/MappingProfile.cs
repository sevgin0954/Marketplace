using AutoMapper;
using SalesProductEntity = Marketplace.Infrastructure.Sales.ProductPersistence.Product;
using Marketplace.UI.Models.HomeModels;
using SalesProductAggregate = Marketplace.Domain.Sales.ProductAggregate.Product;
using Marketplace.Domain.Sales.ProductAggregate;
using Marketplace.UI.Areas.Users.Models.ProductsModels;
using ShippingBuyer = Marketplace.Domain.Shipping.BuyerAggregate.Buyer;
using ShippingOrderAggregate = Marketplace.Domain.Shipping.OrderAggregate.Order;
using ShippingOrderEntity = Marketplace.Infrastructure.Shipping.OrderPersistence.Order;
using Marketplace.UI.Areas.Users.Models.OrdersModels;
using System.Collections.Generic;
using SalesEntityStatus = Marketplace.Infrastructure.Sales.ProductPersistence.Status;

namespace Marketplace.UI
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			this.CreateMap<SalesProductAggregate, ProductViewModel>();

			this.CreateMap<ProductStatus, SalesEntityStatus>()
				.ConstructUsing(src => new SalesEntityStatus(src.ToString()));

			this.CreateMap<SalesProductAggregate, SalesProductEntity>()
				.ForMember(
					dest => dest.Status,
					opt => opt.MapFrom(src => new SalesEntityStatus("asaas"))
				).ReverseMap();

			this.CreateMap<ShippingBuyer, CreateProductBindingModel>();

			this.CreateMap<ShippingOrderAggregate, OrderViewModel>()
				.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

			this.CreateMap<IList<ShippingOrderAggregate>, OrdersViewModel>()
				.ForMember(dest => dest.TotalOrders, opt => opt.MapFrom(src => src.Count));

			this.CreateMap<ShippingOrderEntity, ShippingOrderAggregate>();
		}
	}
}
