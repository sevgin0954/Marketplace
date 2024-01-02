﻿using ServiceLayerRegistrar.Tests.TypeComparerSpecs.TestClasses;
using System;
using Xunit;

namespace ServiceLayerRegistrar.Tests.TypeComparerSpecs
{
	public class CampareTypesSpecs
	{
		[Theory]
		[InlineData(typeof(string), null)]
		[InlineData(null, typeof(string))]
		public void Compare_types_when_one_type_is_null_should_throw_an_exception(Type type1, Type type2)
		{
			// Arrange
			// Act
			// Assert
			Assert.Throws<ArgumentNullException>(() => TypeComparer.CompareTypes(type1, type2));
		}

		[Theory]
		[InlineData(typeof(string), typeof(string))]
		[InlineData(typeof(TestNonGenericClass1), typeof(TestNonGenericClass1))]
		public void Compare_equal_non_generic_types_should_return_true(Type type1, Type type2)
		{
			// Arrange

			// Act
			var result = TypeComparer.CompareTypes(type1, type2);

			// Assert
			Assert.True(result);
		}

		[Theory]
		[InlineData(typeof(string), typeof(object))]
		[InlineData(typeof(TestNonGenericClass1), typeof(TestNonGenericClass2))]
		public void Compare_different_non_generic_types_should_return_false(Type type1, Type type2)
		{
			// Arrange

			// Act
			var result = TypeComparer.CompareTypes(type1, type2);

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void Compare_types_from_different_namespaces_with_same_names_should_return_false()
		{
			// Arrange
			var type1 = typeof(TestClasses.TestNonGenericClass1);
			var type2 = typeof(TestNonGenericClass1);

			// Act
			var result = TypeComparer.CompareTypes(type1, type2);

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void Compare_equal_open_generic_types_should_return_true()
		{
			// Arrange
			var type1 = typeof(TestGenericClass1<,>);
			var type2 = typeof(TestGenericClass1<,>);

			// Act
			var result = TypeComparer.CompareTypes(type1, type2);

			// Assert
			Assert.True(result);
		}

		[Fact]
		public void Compare_equal_closed_generic_types_should_return_true()
		{
			// Arrange
			var type1 = typeof(TestGenericClass1<TestGenericParameter1, TestGenericParameter2>);
			var type2 = typeof(TestGenericClass1<TestGenericParameter1, TestGenericParameter2>);

			// Act
			var result = TypeComparer.CompareTypes(type1, type2);

			// Assert
			Assert.True(result);
		}

		[Fact]
		public void Compare_generic_types_with_different_generic_type_arguments_should_return_false()
		{
			// Arrange
			var type1 = typeof(TestGenericClass1<TestGenericParameter1, TestGenericParameter2>);
			var type2 = typeof(TestGenericClass1<TestGenericParameter2, TestGenericParameter2>);

			// Act
			var result = TypeComparer.CompareTypes(type1, type2);

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void Compare_open_generic_type_with_closed_type_should_return_false()
		{
			// Arrange
			var openGenericType = typeof(TestGenericClass1<,>);
			var closedGenericType = typeof(TestGenericClass1<TestGenericParameter1, TestGenericParameter2>);

			// Act
			var result = TypeComparer.CompareTypes(openGenericType, closedGenericType);

			// Assert
			Assert.False(result);
		}
	}
}
