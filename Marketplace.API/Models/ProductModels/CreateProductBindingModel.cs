using AutoMapperRegistrar.Interfaces;

namespace Marketplace.API.Models.ProductModels
{
	public class CreateProductBindingModel
	{
		public string Name { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public decimal Price { get; set; }

		public string Currency = string.Empty;

		public string SellerId { get; set; } = string.Empty;
	}
}
