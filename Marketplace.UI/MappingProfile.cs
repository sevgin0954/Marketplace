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
using SalesSellerAggregate = Marketplace.Domain.Sales.SellerAggregate.Seller;
using Marketplace.UI.Areas.Users.Models.OffersModels;

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


			// TODO: Mapper.CreateMap<MyEnum, string>().ConvertUsing(src => src.ToString());
			this.CreateMap<ShippingOrderEntity, ShippingOrderAggregate>()
				.ForMember(dest => dest.CanceledOrderBy, opt => opt.MapFrom(src => src.CanceledOrderBy.ToString()))
				.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

			this.CreateMap<SalesSellerAggregate, OffersViewModel>()
				.ForMember(dest => dest.AcceptedOffersCount, opt => opt.MapFrom(src => src.SoldOutProductIds.Count))
				.ForMember(dest => dest.PendingOffersCount, opt => opt.MapFrom(src => src.ReceivedOffers.Count))
				.ForMember(dest => dest.RejectedOffersCount, opt => opt.MapFrom(src => src.ReceivedOffers.Count));
		}
	}
}