using AutoMapper;

namespace AutoMapperRegistrar.Interfaces
{
	public interface ICustomMappings
	{
		void CreateMappings(IProfileExpression configuration);
	}
}
