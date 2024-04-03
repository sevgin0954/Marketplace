using AutoMapper;
using Moq;
using System.Reflection;
using Xunit;

namespace AutoMapperRegistrar.Tests.MappingRegisterarSpecs
{
	public class RegisterMappingsSpecs
	{
		[Fact]
		public void With_null_mappings_parameter_should_throw_an_exception()
		{
			// Arrange
			var registerar = MappingRegisterarFactory.Create();
			ICollection<MappingType> mappings = null!;

			// Act
			// Assert
			Assert.Throws<ArgumentException>(() => registerar.RegisterMappings(mappings));
		}

		[Fact]
		public void With_emtpy_mappings_parameter_should_throw_an_exception()
		{
			// Arrange
			var registerar = MappingRegisterarFactory.Create();
			var mappings = new List<MappingType>();

			// Act
			// Assert
			Assert.Throws<ArgumentException>(() => registerar.RegisterMappings(mappings));
		}

		[Fact]
		public void With_mappings_should_add_mappigs()
		{
			// Arrange
			var configMock = new Mock<IProfileExpression>();
			var registerar = new MappingRegisterar(configMock.Object);

			var mapping1 = this.GetMapping1();
			var mapping2 = this.GetMapping2();

			var mappings = new List<MappingType>()
			{
				mapping1,mapping2
			};

			// Act
			registerar.RegisterMappings(mappings);

			// Assert
			configMock.Verify(e => e.CreateMap(mapping1.Source, mapping1.Destination), Times.Once);
			configMock.Verify(e => e.CreateMap(mapping2.Source, mapping2.Destination), Times.Once);
		}

		private MappingType GetMapping1()
		{
			var sourceType1 = "".GetType();
			var destinationType1 = "".GetType();
			var mapping1 = this.CreateMappingType(sourceType1, destinationType1);

			return mapping1;
		}

		private MappingType GetMapping2()
		{
			var sourceType2 = new Object().GetType();
			var destinationType2 = new Object().GetType();
			var mapping2 = this.CreateMappingType(sourceType2, destinationType2);

			return mapping2;
		}

		private MappingType CreateMappingType(params Type[] consructorParams)
		{
			var constructorInfo = typeof(MappingType)
				.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(int) }, null);
			var instance = (MappingType)constructorInfo.Invoke(consructorParams);

			return instance;
		}
	}
}
