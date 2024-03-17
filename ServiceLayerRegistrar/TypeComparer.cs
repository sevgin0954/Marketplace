using ServiceLayerRegistrar.CustomGenericConstraints;
using System;
using System.Reflection;

namespace ServiceLayerRegistrar
{
	internal static class TypeComparer
	{
		public static bool DoesTypeMatch(Type typeToMatch, Type searchedType)
		{
			ArgumentValidator.ThrowExceptionIfNull(new object[] { searchedType, typeToMatch }, nameof(searchedType), nameof(typeToMatch));

			if (searchedType.Name != typeToMatch.Name)
				return false;
			if (searchedType.Namespace != typeToMatch.Namespace)
				return false;

			if (searchedType.IsGenericTypeDefinition)
				return true;

			var areTypesGeneric = searchedType.IsGenericType;
			if (areTypesGeneric)
			{
				var searchedTypeGenerics = searchedType.GetGenericArguments();
				var typeGenerics = typeToMatch.GetGenericArguments();

				return MatchTypeGenerics(typeGenerics, searchedTypeGenerics);
			}

			return true;
		}

		private static bool MatchTypeGenerics(Type[] typeGenerics, Type[] searchedTypeGenerics)
		{
			var isAllGenericTypesMatch = true;

			for (var i = 0; i < typeGenerics.Length; i++)
			{
				if (MatchGenericArgumentsOrParameters(typeGenerics[i], searchedTypeGenerics[i]) == false)
				{
					isAllGenericTypesMatch = false;
					break;
				}
			}

			return isAllGenericTypesMatch;
		}

		private static bool MatchGenericArgumentsOrParameters(Type type, Type searchedType)
		{
			var isType1CustomGenericType = typeof(BaseGenericConstraint).IsAssignableFrom(type);
			var isType2CustomGenericType = typeof(BaseGenericConstraint).IsAssignableFrom(searchedType);

			if (isType1CustomGenericType && isType2CustomGenericType)
				throw new ArgumentException("Both generic types or parameters are custom generic constraints!");

			var doesTypesMatch = false;

			var assembly = Assembly.GetExecutingAssembly();
			if (isType1CustomGenericType)
			{
				var type1Instance = assembly.CreateInstance(type.FullName) as BaseGenericConstraint;
				doesTypesMatch = type1Instance.IsMatch(searchedType);
			}
			else if (isType2CustomGenericType)
			{
				var type2Instance = assembly.CreateInstance(searchedType.FullName) as BaseGenericConstraint;
				doesTypesMatch = type2Instance.IsMatch(type);
			}
			else
			{
				if (type.IsGenericTypeDefinition || type.ContainsGenericParameters)
					type = type.BaseType;
				if (searchedType.IsGenericTypeDefinition || searchedType.ContainsGenericParameters)
					searchedType = searchedType.BaseType;

				doesTypesMatch = type.Equals(searchedType) || searchedType.IsAssignableFrom(type);
			}

			return doesTypesMatch;
		}
	}
}
