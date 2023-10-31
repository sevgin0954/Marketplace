using AutoMapper;
using Moq;

namespace AutoMapperRegistrar.Tests.MappingRegisterarSpecs
{
	public class MappingRegisterarFactory
	{
		public static MappingRegisterar Create()
		{
			var configMock = new Mock<IProfileExpression>();
			var registerar = new MappingRegisterar(configMock.Object);

			return registerar;
		}
	}
}
