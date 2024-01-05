using ServiceLayerRegistrar.CustomGenericConstraints;
using System;
using System.Reflection;

namespace ServiceLayerRegistrar
{
	internal static class TypeComparer
	{
		public static bool CompareTypes(Type type1, Type type2)
		{
			ArgumentValidator.ThrowExceptionIfNull(new object[] { type1, type2 }, nameof(type1), nameof(type2));

			if (type1.Name != type2.Name)
				return false;
			if (type1.Namespace != type2.Namespace)
				return false;

			var areTypesGeneric = type1.IsGenericType;
			if (areTypesGeneric)
			{
				return MatchTypeGenerics(type1, type2);
			}

			return type1.Equals(type2);
		}

		private static bool MatchTypeGenerics(Type type1, Type type2)
		{
			var isAllGenericTypesMatch = true;

			var isType1Closed = type1.IsGenericTypeDefinition == false;
			var isType2Closed = type2.IsGenericTypeDefinition == false;

			if (isType1Closed == isType2Closed)
				return type1.Equals(type2);

			if (isType1Closed || isType2Closed)
			{
				var type1Generics = type1.GetGenericArguments();
				var type2Generics = type2.GetGenericArguments();

				for (var i = 0; i < type1Generics.Length; i++)
				{
					if (MatchGenericArgumentsOrParameters(type1Generics[i], type2Generics[i]) == false)
					{
						isAllGenericTypesMatch = false;
						break;
					}
				}
			}

			return isAllGenericTypesMatch;
		}

		private static bool MatchGenericArgumentsOrParameters(Type type1, Type type2)
		{
			var isType1CustomGenericType = typeof(BaseGenericConstraint).IsAssignableFrom(type1);
			var isType2CustomGenericType = typeof(BaseGenericConstraint).IsAssignableFrom(type2);

			if (isType1CustomGenericType && isType2CustomGenericType)
				throw new ArgumentException("Both generic types or parameters are custom generic constraints!");

			var doesTypesMatch = false;

			var assembly = Assembly.GetExecutingAssembly();
			if (isType1CustomGenericType)
			{
				var type1Instance = assembly.CreateInstance(type1.FullName) as BaseGenericConstraint;
				doesTypesMatch = type1Instance.IsMatch(type2);
			}
			else if (isType2CustomGenericType)
			{
				var type2Instance = assembly.CreateInstance(type2.FullName) as BaseGenericConstraint;
				doesTypesMatch = type2Instance.IsMatch(type1);
			}
			else
			{
				if (type1.IsGenericTypeParameter || type2.IsGenericTypeParameter)
					doesTypesMatch = true;
				else
					doesTypesMatch = type1.Equals(type2);
			}

			return doesTypesMatch;
		}
	}
}
