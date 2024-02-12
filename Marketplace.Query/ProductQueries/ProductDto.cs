using AutoMapper;
using AutoMapperRegistrar.Interfaces;
using PersistenceSales = Marketplace.Persistence.Sales;
using PersistenceBrowsing = Marketplace.Persistence.Browsing;
using PersistenceIdeneityAndAccess = Marketplace.Persistence.IdentityAndAccess;

namespace Marketplace.Query.ProductQueries
{
    public class ProductDto : 
        IMappableFrom<PersistenceSales.ProductEntity>, 
        IMappableFrom<PersistenceBrowsing.ProductEntity>,
		IMappableFrom<PersistenceIdeneityAndAccess.UserEntity>,
		ICustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string SellerId { get; set; }

        public string SellerName { get; set; }

        public string Status { get; set; }

        public PriceDto Price { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
			configuration.CreateMap<PersistenceSales.ProductEntity, ProductDto>()
				.ForPath(dest => dest.Price.Value, opt => opt.MapFrom(src => src.Price))
				.ForPath(dest => dest.Price.Currency, opt => opt.MapFrom(src => src.PriceCurrency));

            configuration.CreateMap<PersistenceIdeneityAndAccess.UserEntity, ProductDto>()
                .ForMember(dest => dest.SellerName, opt => opt.MapFrom(src => src.UserName));

		}
    }
}
