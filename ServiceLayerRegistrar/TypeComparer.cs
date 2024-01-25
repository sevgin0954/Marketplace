using ServiceLayerRegistrar.CustomGenericConstraints;
using System;
using System.Reflection;

namespace ServiceLayerRegistrar
{
	internal static class TypeComparer
	{
		public static bool DoesTypeMatch(Type typeToMatch, Type matchType)
		{
			ArgumentValidator.ThrowExceptionIfNull(new object[] { matchType, typeToMatch }, nameof(matchType), nameof(typeToMatch));

			if (matchType.Name != typeToMatch.Name)
				return false;
			if (matchType.Namespace != typeToMatch.Namespace)
				return false;

			if (matchType.IsGenericTypeDefinition)
				return true;

			var areTypesGeneric = matchType.IsGenericType;
			if (areTypesGeneric)
			{
				var type1Generics = matchType.GetGenericArguments();
				var type2Generics = typeToMatch.GetGenericArguments();

				return MatchTypeGenerics(type1Generics, type2Generics);
			}

			return true;
		}

		private static bool MatchTypeGenerics(Type[] type1Generics, Type[] type2Generics)
		{
			var isAllGenericTypesMatch = true;

			for (var i = 0; i < type1Generics.Length; i++)
			{
				if (MatchGenericArgumentsOrParameters(type1Generics[i], type2Generics[i]) == false)
				{
					isAllGenericTypesMatch = false;
					break;
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
				if (type1.IsGenericTypeDefinition)
					type1 = type1.BaseType;
				if (type2.IsGenericTypeDefinition)
					type2 = type2.BaseType;

				doesTypesMatch = type1.Equals(type2);
			}

			return doesTypesMatch;
		}
	}
}
