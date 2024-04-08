using AutoMapperRegistrar.Interfaces;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Persistence.Browsing
{
	public class ImageEntity : IMappableBothDirections<Image>
	{
		public string Id { get; set; }

		public int DisplayPriority { get; set; }
	}
}
