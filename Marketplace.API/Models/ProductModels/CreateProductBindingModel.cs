using AutoMapper;
using AutoMapperRegistrar.Interfaces;
using Marketplace.Domain.SharedKernel;
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
			configuration.CreateMap<CreateProductBindingModel, CreateProductCommand>()
				.ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => new Id(src.UserId)))
				.ForMember(dest => dest.Price, opt => opt.MapFrom(src => new Price(src.Price, Enum.Parse<Currency>(src.Currency))));
		}
	}
}
