using AutoMapper;
using AutoMapperRegistrar.Interfaces;
using Marketplace.API.Attributes;
using Marketplace.API.Attributes.Validation;
using Marketplace.Domain.Browsing.ProductAggregate;
using Marketplace.Domain.SharedKernel;
using Marketplace.Domain.SharedKernel.Commands;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.API.Models.ProductModels
{
	public class CreateProductBindingModel : IMappableTo<CreateProductCommand>, ICustomMappings
	{
		[Required]
		[StringLength(ProductConstants.MAX_NAME_LENGTH, MinimumLength = ProductConstants.MIN_NAME_LENGTH)]
		public string Name { get; set; }

		[Required]
		[StringLength(ProductConstants.MAX_DESCRIPTION_LENGTH, MinimumLength = ProductConstants.MIN_DESCRIPTION_LENGTH)]
		public string Description { get; set; }

		[Required]
		[Range(minimum: 0, maximum: double.MaxValue)]
		public decimal Price { get; set; }

		[Required]
		[StringCurrencyCode]
		public string Currency { get; set; }

		[MapFromJwtToken("id")]
		public string UserId { get; set; }

		[Required]

		[NotNullOrEmptyStringInCollection]
		public IEnumerable<string> ImageIds { get; set; }

		public void CreateMappings(IProfileExpression configuration)
		{
			configuration.CreateMap<CreateProductBindingModel, CreateProductCommand>()
				.ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => new Id(src.UserId)))
				.ForMember(dest => dest.Price, opt => opt.MapFrom(src => new Price(src.Price, Enum.Parse<Currency>(src.Currency.ToUpper()))));
		}
	}
}
