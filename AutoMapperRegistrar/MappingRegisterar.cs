using AutoMapper;
using AutoMapperRegistrar.Interfaces;

namespace AutoMapperRegistrar
{
	public class MappingRegisterar
	{
		private readonly IProfileExpression configurationExpression;

		public MappingRegisterar(IProfileExpression configurationExpression)
		{
			this.configurationExpression = configurationExpression;
		}

		public void RegisterMappings(ICollection<MappingType> mappings)
		{
			if (mappings == null || mappings.Count == 0)
				throw new ArgumentException($"Null or empty {nameof(mappings)}");

			foreach (var currentMapping in mappings)
			{
				this.configurationExpression.CreateMap(currentMapping.Source, currentMapping.Destination);
			}
		}

		public void RegisterCustomMappings(ICollection<ICustomMappings> customMappings)
		{
			if (customMappings == null || customMappings.Count == 0)
				return;

			foreach (var currentMapping in customMappings)
			{
				currentMapping.CreateMappings(this.configurationExpression);
			}
		}
	}
}