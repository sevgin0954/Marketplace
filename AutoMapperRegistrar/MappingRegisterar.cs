using AutoMapper;
using AutoMapperRegistrar.Interfaces;

namespace AutoMapperRegistrar
{
	public class MappingRegisterar
	{
		const string EMPTY_OR_NULL_EXCEPTION_MESSAGE = "Null or empty ";

		private readonly IProfileExpression configurationExpression;

		public MappingRegisterar(IProfileExpression profileExpression)
		{
			this.configurationExpression = profileExpression;
		}

		public void RegisterMappings(ICollection<MappingType> mappings)
		{
			if (mappings == null || mappings.Count == 0)
				throw new ArgumentException(EMPTY_OR_NULL_EXCEPTION_MESSAGE + nameof(mappings));

			foreach (var currentMapping in mappings)
			{
				var currentMap = this.configurationExpression.CreateMap(currentMapping.Source, currentMapping.Destination);
				if (currentMapping.IsReversible)
					currentMap.ReverseMap();
			}
		}

		public void RegisterCustomMappings(ICollection<ICustomMappings> customMappings)
		{
			if (customMappings == null || customMappings.Count == 0)
				throw new ArgumentException(EMPTY_OR_NULL_EXCEPTION_MESSAGE + nameof(customMappings));

			foreach (var currentMapping in customMappings)
			{
				currentMapping.CreateMappings(this.configurationExpression);
			}
		}
	}
}