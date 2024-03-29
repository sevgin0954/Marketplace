﻿using Microsoft.Extensions.DependencyInjection;
using Moq;
using ServiceLayerRegistrar.Tests.TestClasses;
using ServiceLayerRegistrar.Tests.TestInterfaces;
using System.Reflection;
using Xunit;

namespace ServiceLayerRegistrar.Tests.ServiceRegistrarSpecs
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

		[Fact]
		public void With_open_generic_interface_should_register_only_one_matching_interface_with_the_class_as_both_open_generics()
		{
			// Arrange
			var class1 = typeof(TestGenericClass1<,>);
			var class2 = typeof(TestGenericClass2<,>);
			var assembly = this.GetAssembly(class1, class2);

			var searchedInterface = typeof(TestGenericInterface1<,>);

			var serviceCollection = new ServiceCollection();
			var registerer = this.GetServiceCollectionRegistrar(serviceCollection);

			// Act
			registerer.RegisterScopedServices(assembly, searchedInterface);

			// Assert
			var expectedRegisteredInterface1 = typeof(TestGenericInterface1<TestGenericParameter1, TestGenericParameter2>);
			var registeredClass1 = this.GetRegisteredService(serviceCollection, expectedRegisteredInterface1);

			Assert.Equal(class1.Name, registeredClass1.Name);
			Assert.Equal(registeredClass1.GenericTypeArguments, expectedRegisteredInterface1.GenericTypeArguments);
			Assert.Single(serviceCollection);
		}

		[Fact]
		public void With_closed_generic_interface_should_register_all_matching_non_generic_classes()
		{
			// Arrange
			var class1 = typeof(TestNonGenericClassWithGenericInterface1);
			var class2 = typeof(TestNonGenericClassWithGenericInterface2);
			var assembly = this.GetAssembly(class1, class2);

			var searchedInterface = typeof(TestGenericInterface1<BaseGenericParameter, BaseGenericParameter>);

			var serviceCollection = new ServiceCollection();
			var registerer = this.GetServiceCollectionRegistrar(serviceCollection);

			// Act
			registerer.RegisterScopedServices(assembly, searchedInterface);

			// Assert
			var registeredClass1Interface = typeof(TestGenericInterface1<TestGenericParameter1, TestGenericParameter1>);
			var registeredClass1 = this.GetRegisteredService(serviceCollection, registeredClass1Interface);
			Assert.Equal(class1, registeredClass1);

			var registeredClass2Interface = typeof(TestGenericInterface1<TestGenericParameter2, TestGenericParameter2>);
			var registeredClass2 = this.GetRegisteredService(serviceCollection, registeredClass2Interface);
			Assert.Equal(class2, registeredClass2);

			Assert.Equal(2, serviceCollection.Count);
		}

		class TestGenericClass11<T1, T2> : TestGenericClass1<T1, T2>
			where T1 : BaseGenericParameter
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

		private ServiceRegistrar GetServiceCollectionRegistrar()
		{
			var serviceCollectionMock = new ServiceCollection();
			var registerer = this.GetServiceCollectionRegistrar(serviceCollectionMock);

			return registerer;
		}

		private ServiceRegistrar GetServiceCollectionRegistrar(IServiceCollection serviceCollection)
		{
			var registerer = new ServiceRegistrar(serviceCollection);

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
