using AutoMapper;
using AutoMapperRegistrar.Interfaces;
using Moq;
using Xunit;

namespace AutoMapperRegistrar.Tests.MappingRegisterarSpecs
{
	public class RegisterCustomMappingsSpecs
	{
		[Fact]
		public void With_null_mappings_parameter_should_throw_an_exception()
		{
			// Arrange
			var registerar = MappingRegisterarFactory.Create();
			ICollection<ICustomMappings> mappings = null!;

			// Act
			// Assert
			Assert.Throws<ArgumentException>(() => registerar.RegisterCustomMappings(mappings));
		}

		[Fact]
		public void With_emtpy_mappings_parameter_should_throw_an_exception()
		{
			// Arrange
			var registerar = MappingRegisterarFactory.Create();
			var mappings = new List<ICustomMappings>();

			// Act
			// Assert
			Assert.Throws<ArgumentException>(() => registerar.RegisterCustomMappings(mappings));
		}

		[Fact]
		public void With_mappings_should_create_mappings()
		{
			// Arrange
			var registerar = MappingRegisterarFactory.Create();

			var mockedMapping1 = new Mock<ICustomMappings>();
			var mockedMapping2 = new Mock<ICustomMappings>();

			var mappings = new List<ICustomMappings>()
			{
				mockedMapping1.Object, mockedMapping2.Object
			};

			// Act
			registerar.RegisterCustomMappings(mappings);

			// Assert
			mockedMapping1.Verify(m => m.CreateMappings(It.IsAny<IProfileExpression>()), Times.Once);
			mockedMapping2.Verify(m => m.CreateMappings(It.IsAny<IProfileExpression>()), Times.Once);
		}

		[Fact]
		public void With_mappings_should_create_mappings_with_correct_profile_expression()
		{
			// Arrange
			var registerar = MappingRegisterarFactory.Create();

			var mockedMapping = new Mock<ICustomMappings>();

			var mappings = new List<ICustomMappings>()
			{
				mockedMapping.Object
			};

			// Act
			registerar.RegisterCustomMappings(mappings);

			// Assert
			mockedMapping.Verify(m => m.CreateMappings(It.IsAny<IProfileExpression>()), Times.Once);
		}
	}
}
