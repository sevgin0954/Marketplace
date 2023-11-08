using AutoMapper;
using AutoMapperRegistrar.Interfaces;
using Marketplace.Domain.SharedKernel.Commands;

namespace Marketplace.API.Models.ProductModels
{
	public class CreateProductBindingModel : IMappableTo<CreateProductCommand>, ICustomMappings
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public decimal Price { get; set; }

		public string Currency { get; set; }

		public string UserId { get; set; }

		public void CreateMappings(IProfileExpression configuration)
		{
			configuration.CreateMap<CreateProductCommand, CreateProductBindingModel>()
				.ForMember(dest => dest.Currency, config => config.MapFrom(src => src.Price.Currency.ToString()))
				.ForMember(dest => dest.UserId, config => config.MapFrom(src => src.SellerId));
		}
	}
}
