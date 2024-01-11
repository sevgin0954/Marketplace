using Microsoft.Extensions.DependencyInjection;
using Moq;
using ServiceLayerRegistrar.Tests.TestClasses;
using ServiceLayerRegistrar.Tests.TestInterfaces;
using System.Reflection;
using Xunit;

namespace ServiceLayerRegistrar.Tests.ServiceCollectionRegistrarSpecs
{
	public class RegisterScopedServicesSpecs
	{
		[Fact]
		public void With_null_assembly_should_throw_an_exception()
		{
			// Arrange
			var registerer = this.GetServiceCollectionRegistrar();

			Assembly? assembly = null;
			var interfaceType = typeof(TestGenericInterface1<,>);

			// Act
			// Assert
			Assert.Throws<ArgumentNullException>(() => registerer.RegisterScopedServices(assembly, interfaceType));
		}

		[Fact]
		public void With_null_interface_type_should_throw_an_exception()
		{
			// Arrange
			var registerer = this.GetServiceCollectionRegistrar();

			Assembly assembly = Assembly.GetExecutingAssembly();
			Type? interfaceType = null;

			// Act
			// Assert
			Assert.Throws<ArgumentNullException>(() => registerer.RegisterScopedServices(assembly, interfaceType));
		}

		[Fact]
		public void With_no_interface_matches_should_throw_an_exception()
		{
			// Arrange
			var assemblyClass = typeof(TestGenericClass2<,>);
			var assembly = this.GetAssembly(assemblyClass);

			var registerer = this.GetServiceCollectionRegistrar();

			var interfaceToRegister = typeof(TestGenericInterface1<,>);

			// Act
			// Assert
			var exception = Assert.Throws<ArgumentException>(() => registerer.RegisterScopedServices(assembly, interfaceToRegister));
			Assert.Contains("No classes found for the selelcted interface", exception.Message);
		}

		[Fact]
		public void With_open_generic_interface_should_register_all_matching_interfaces_with_the_classes()
		{
			// Arrange

			// Act

			// Assert
		}

		[Fact]
		public void With_closed_generic_interface_and_no_custom_generic_constraints_should_register_only_one_class_and_interface()
		{
			// Arrange

			// Act

			// Assert
		}

		[Fact]
		public void With_closed_generic_interface_and_no_custom_generic_constraints_interface_and_two_classes_implementing_same_interface_should_match_the_class_implementing_the_interface_at_the_lower_level()
		{
			// Arrange

			// Act

			// Assert
		}

		[Fact]
		public void With_closed_generic_interface_and_two_classes_implementing_the_interface_at_the_same_level_should_throw_an_exception()
		{
			// Arrange

			// Act

			// Assert
		}

		[Fact]
		public void With_cusom_generic_constraint_should_match_the_correct_interface_from_the_assembly()
		{
			// Arrange

			// Act

			// Assert
		}

		private ServiceCollectionRegistrar GetServiceCollectionRegistrar()
		{
			var serviceCollectionMock = new Mock<IServiceCollection>();
			var registerer = new ServiceCollectionRegistrar(serviceCollectionMock.Object);

			return registerer;
		}

		private Assembly GetAssembly(params Type[] assemblyInterfaces)
		{
			var mockedAssembly = new Mock<Assembly>();
			mockedAssembly.Setup(a => a.GetTypes()).Returns(assemblyInterfaces);

			return mockedAssembly.Object;
		}
	}
}
