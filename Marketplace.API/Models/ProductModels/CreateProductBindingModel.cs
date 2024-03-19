using AutoMapper;
using AutoMapperRegistrar.Interfaces;
using Marketplace.Domain.SharedKernel;
using Marketplace.Domain.SharedKernel.Commands;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.API.Models.ProductModels
{
	public class CreateProductBindingModel : IMappableTo<CreateProductCommand>, ICustomMappings
	{
		// TODO: Make length constraints glaboal for entire solution
		[Required]
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		public decimal Price { get; set; }

		[Required]
		public string Currency { get; set; }

		[Required]
		public string UserId { get; set; }

		public void CreateMappings(IProfileExpression configuration)
		{
			configuration.CreateMap<CreateProductBindingModel, CreateProductCommand>()
				.ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => new Id(src.UserId)))
				.ForMember(dest => dest.Price, opt => opt.MapFrom(src => new Price(src.Price, Enum.Parse<Currency>(src.Currency.ToUpper()))));
		}
	}
}
