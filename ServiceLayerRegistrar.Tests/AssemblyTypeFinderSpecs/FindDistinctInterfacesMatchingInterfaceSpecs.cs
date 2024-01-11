using Moq;
using ServiceLayerRegistrar.Tests.TestClasses.TestAnimal;
using ServiceLayerRegistrar.Tests.TestClasses.TestVehicle;
using ServiceLayerRegistrar.Tests.TestInterfaces;
using System.Reflection;
using Xunit;

namespace ServiceLayerRegistrar.Tests.AssemblyTypeFinderSpecs
{
	public class FindDistinctInterfacesMatchingInterfaceSpecs
	{
		[Fact]
		public void With_null_interface_type_should_throw_an_exception()
		{
			// Arrange
			var assemblyTypeFinder = this.CreateAssemblyTypeFinder();

			// Act
			// Assert
			Assert.Throws<ArgumentNullException>(() => assemblyTypeFinder.FindDistinctInterfacesMatchingInterface(null));
		}

		[Fact]
		public void When_match_doesnt_exist_should_return_an_empty_colletion()
		{
			// Arrange
			var assemblyTypeFinder = this.CreateAssemblyTypeFinder();

			// Act
			var result = assemblyTypeFinder.FindDistinctInterfacesMatchingInterface(typeof(TestNonGenericInterface1));

			// Assert
			Assert.Empty(result);
		}

		[Fact]
		public void With_class_in_the_assembly_imlementing_the_searched_interface_should_return_the_interface()
		{
			// Arrange
			var vehicleClass = typeof(TestVehicleClass1);
			var assemblyTypeFinder = this.CreateAssemblyTypeFinder(vehicleClass);

			var searchedInterface = typeof(ITestVehicleInterface);

			// Act
			var result = assemblyTypeFinder.FindDistinctInterfacesMatchingInterface(searchedInterface);

			// Assert
			var expectedResult = new Type[] { searchedInterface };
			Assert.Equal(expectedResult, result);
		}

		[Fact]
		public void With_classes_in_the_assembly_when_only_one_of_the_classes_implements_the_searched_interface_should_return_only_one_interface()
		{
			// Arrange
			var vehicleClass = typeof(TestVehicleClass1);
			var animalClass = typeof(TestAnimalClass1);
			var assemblyTypeFinder = this.CreateAssemblyTypeFinder(vehicleClass, animalClass);

			var searchedInterface = typeof(ITestVehicleInterface);

			// Act
			var result = assemblyTypeFinder.FindDistinctInterfacesMatchingInterface(searchedInterface);

			// Assert
			var expectedResult = new Type[] { searchedInterface };
			Assert.Equal(expectedResult, result);
		}

		[Fact]
		public void When_match_more_than_one_interface_of_the_same_type_should_return_the_interface_only_once()
		{
			// Arrange
			var vehicleClass1 = typeof(TestVehicleClass1);
			var vehicleClass2 = typeof(TestVehicleClass2);
			var assemblyTypeFinder = this.CreateAssemblyTypeFinder(vehicleClass1, vehicleClass2);

			var searchedInterface = typeof(ITestVehicleInterface);

			// Act
			var result = assemblyTypeFinder.FindDistinctInterfacesMatchingInterface(searchedInterface);

			// Assert
			var expectedResult = new Type[] { searchedInterface };
			Assert.Equal(expectedResult, result);
		}

		private AssemblyTypeFinder CreateAssemblyTypeFinder()
		{
			var assemblyMock = new Mock<Assembly>();
			assemblyMock.Setup(a => a.GetTypes()).Returns(new Type[] { });
			var assemblyTypeFinder = new AssemblyTypeFinder(assemblyMock.Object);

			return assemblyTypeFinder;
		}

		private AssemblyTypeFinder CreateAssemblyTypeFinder(params Type[] classTypesInAssembly)
		{
			var assemblyMock = new Mock<Assembly>();
			assemblyMock.Setup(a => a.GetTypes()).Returns(classTypesInAssembly);
			var assemblyTypeFinder = new AssemblyTypeFinder(assemblyMock.Object);

			return assemblyTypeFinder;
		}
	}
}