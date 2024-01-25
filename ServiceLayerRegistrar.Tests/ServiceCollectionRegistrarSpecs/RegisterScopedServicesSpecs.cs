using Microsoft.Extensions.DependencyInjection;
using Moq;
using ServiceLayerRegistrar.CustomGenericConstraints;
using ServiceLayerRegistrar.Tests.TestClasses;
using ServiceLayerRegistrar.Tests.TestInterfaces;
using System.Diagnostics;
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
			Assert.Throws<ArgumentException>(() => registerer.RegisterScopedServices(assembly, interfaceToRegister));
		}

		// TODO: IF INTERFACE IS OPEN AND CLASS NOT AND IF CLASS IS OPEN AND THE SEARCHED INTERFACE IS NOT

		[Fact]
		public void With_open_generic_interface_should_register_all_matching_interfaces_with_the_classes()
		{
			// Arrange
			var class1 = typeof(TestGenericClass1<TestGenericParameter1, TestGenericParameter2>);
			var class2 = typeof(TestGenericClass1<TestGenericParameter2, TestGenericParameter2>);
			var class3 = typeof(TestGenericClass2<,>);
			var assembly = this.GetAssembly(class1, class2, class3);

			var searchedInterface = typeof(TestGenericInterface1<,>);

			var serviceCollection = new ServiceCollection();
			var registerer = this.GetServiceCollectionRegistrar(serviceCollection);

			// Act
			registerer.RegisterScopedServices(assembly, searchedInterface);

			// Assert
			var expectedRegisteredInterface1 = typeof(TestGenericInterface1<TestGenericParameter1, TestGenericParameter2>);
			var registeredClass1 = this.GetRegisteredService(serviceCollection, expectedRegisteredInterface1);

			var expectedRegisteredInterface2 = typeof(TestGenericInterface1<TestGenericParameter2, TestGenericParameter2>);
			var registeredClass2 = this.GetRegisteredService(serviceCollection, expectedRegisteredInterface2);

			Assert.Equal(class1, registeredClass1);
			Assert.Equal(class2, registeredClass2);
			Assert.Equal(2, serviceCollection.Count);
		}

		[Fact]
		public void With_closed_generic_interface_and_no_custom_generic_constraints_should_register_only_one_class_and_interface()
		{
			// Arrange
			var class1 = typeof(TestGenericClass1<TestGenericParameter1, TestGenericParameter2>);
			var class2 = typeof(TestGenericClass1<TestGenericParameter2, TestGenericParameter2>);
			var class3 = typeof(TestGenericClass1<,>);
			var class4 = typeof(TestGenericClass2<,>);
			var assembly = this.GetAssembly(class1, class2, class3, class4);

			var searchedInterface = typeof(TestGenericInterface1<TestGenericParameter1, TestGenericParameter2>);

			var serviceCollection = new ServiceCollection();
			var registerer = this.GetServiceCollectionRegistrar(serviceCollection);

			// Act
			registerer.RegisterScopedServices(assembly, searchedInterface);

			// Assert
			var registeredClass1 = this.GetRegisteredService(serviceCollection, searchedInterface);
			Assert.Equal(class1, registeredClass1);
			Assert.Single(serviceCollection);
		}

		class TestGenericClass11<T1, T2> : TestGenericClass1<T1, T2>
			where T1 : class
			where T2 : TestGenericParameter2
		{ }

		[Fact]
		public void With_closed_generic_interface_and_no_custom_generic_constraints_interface_and_two_classes_implementing_same_interface_should_match_the_class_implementing_the_interface_at_the_lower_level()
		{
			// Arrange
			var class1 = typeof(TestGenericClass1<TestGenericParameter1, TestGenericParameter2>);
			var class2 = typeof(TestGenericClass11<TestGenericParameter2, TestGenericParameter2>);

			var assembly = this.GetAssembly(class1, class2);

			var searchedInterface = typeof(TestGenericInterface1<TestGenericParameter1, TestGenericParameter2>);

			var serviceCollection = new ServiceCollection();
			var registerer = this.GetServiceCollectionRegistrar(serviceCollection);

			// Act
			registerer.RegisterScopedServices(assembly, searchedInterface);

			// Assert
			var registeredClass1 = this.GetRegisteredService(serviceCollection, searchedInterface);
			Assert.Equal(class1, registeredClass1);
			Assert.Single(serviceCollection);
		}

		[Fact]
		public void With_closed_generic_interface_and_two_classes_implementing_the_interface_at_the_same_level_should_throw_an_exception()
		{
			// Arrange
			var class1 = typeof(TestGenericClass1<TestGenericParameter1, TestGenericParameter2>);
			var class2 = typeof(TestGenericClass1<TestGenericParameter1, TestGenericParameter2>);

			var assembly = this.GetAssembly(class1, class2);

			var searchedInterface = typeof(TestGenericInterface1<TestGenericParameter1, TestGenericParameter2>);

			var serviceCollection = new ServiceCollection();
			var registerer = this.GetServiceCollectionRegistrar(serviceCollection);

			// Act
			// Assert
			var exception = Assert.Throws<InvalidOperationException>(() => 
				registerer.RegisterScopedServices(assembly, searchedInterface));
			Assert.Contains("More than one classes at the same level of inheritance to register", exception.Message);
		}

		private ServiceCollectionRegistrar GetServiceCollectionRegistrar()
		{
			var serviceCollectionMock = new ServiceCollection();
			var registerer = this.GetServiceCollectionRegistrar(serviceCollectionMock);

			return registerer;
		}

		private ServiceCollectionRegistrar GetServiceCollectionRegistrar(IServiceCollection serviceCollection)
		{
			var registerer = new ServiceCollectionRegistrar(serviceCollection);

			return registerer;
		}

		private Assembly GetAssembly(params Type[] assemblyInterfaces)
		{
			var mockedAssembly = new Mock<Assembly>();
			mockedAssembly.Setup(a => a.GetTypes()).Returns(assemblyInterfaces);

			return mockedAssembly.Object;
		}

		private Type GetRegisteredService(IServiceCollection serviceCollection, Type searchedInterface)
		{
			var provider = serviceCollection.BuildServiceProvider();
			var registeredClass = provider.GetRequiredService(searchedInterface);

			return registeredClass.GetType();
		}
	}
}
