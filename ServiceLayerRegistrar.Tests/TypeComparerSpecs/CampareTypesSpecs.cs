using ServiceLayerRegistrar.Tests.TestClasses;
using Xunit;

namespace ServiceLayerRegistrar.Tests.TypeComparerSpecs
{
	public class CampareTypesSpecs
	{
		[Theory]
		[InlineData(typeof(string), null)]
		[InlineData(null, typeof(string))]
		[InlineData(null, null)]
		public void Compare_types_when_type_is_null_should_throw_an_exception(Type type1, Type type2)
		{
			// Arrange
			// Act
			// Assert
			Assert.Throws<ArgumentNullException>(() => TypeComparer.DoesTypeMatch(type1, type2));
		}

		[Theory]
		[InlineData(typeof(string), typeof(string))]
		[InlineData(typeof(TestNonGenericClass1), typeof(TestNonGenericClass1))]
		public void Compare_equal_non_generic_types_should_return_true(Type type1, Type type2)
		{
			// Arrange

			// Act
			var result = TypeComparer.DoesTypeMatch(type1, type2);

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
			var result = TypeComparer.DoesTypeMatch(type1, type2);

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
			var result = TypeComparer.DoesTypeMatch(type1, type2);

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
			var result = TypeComparer.DoesTypeMatch(type1, type2);

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
			var result = TypeComparer.DoesTypeMatch(type1, type2);

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
			var result = TypeComparer.DoesTypeMatch(type1, type2);

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void Compare_same_types_but_the_searched_type_is_open_and_other_closed_should_return_true()
		{
			// Arrange
			var searchedOpenType = typeof(TestGenericClass1<,>);
			var closedType = typeof(TestGenericClass1<TestGenericParameter1, TestGenericParameter2>);

			// Act
			var result = TypeComparer.DoesTypeMatch(closedType, searchedOpenType);

			// Assert
			Assert.True(result);
		}

		[Fact]
		public void Compare_equal_types_with_different_references_should_return_true()
		{
			// Arrange
			var type1 = new TestGenericClass1<TestGenericParameter1, TestGenericParameter2>().GetType();
			var type2 = new TestGenericClass1<TestGenericParameter1, TestGenericParameter2>().GetType();

			// Act
			var result = TypeComparer.DoesTypeMatch(type1, type2);

			// Assert
			Assert.True(result);
		}
	}
}