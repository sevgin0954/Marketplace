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
				var isType1Closed = type1.IsGenericTypeDefinition == false;
				var isType2Closed = type2.IsGenericTypeDefinition == false;
				if (isType1Closed || isType2Closed)
				{
					return MatchGenericTypeArgumentsAndParameters(type1.GetGenericArguments(), type2.GetGenericArguments());
				}
			}

			return type1.Equals(type2);
		}

		private static bool MatchGenericTypeArgumentsAndParameters(Type[] interface1types, Type[] interface2Types)
		{
			if (interface1types.Length != interface2Types.Length)
				return false;

			var isAllGenericTypesMatch = true;

			for (var i = 0; i < interface1types.Length; i++)
			{
				if (MatchGenericArgumentOrType(interface1types[i], interface2Types[i]) == false)
				{
					isAllGenericTypesMatch = false;
					break;
				}
			}

			return isAllGenericTypesMatch;
		}

		private static bool MatchGenericArgumentOrType(Type type1, Type type2)
		{
			var isType1CustomGenericType = typeof(BaseGenericConstraint).IsAssignableFrom(type1);
			var isType2CustomGenericType = typeof(BaseGenericConstraint).IsAssignableFrom(type2);

			var isMatch = false;

			var assembly = Assembly.GetExecutingAssembly();
			if (isType1CustomGenericType && isType2CustomGenericType)
			{
				throw new ArgumentException("Both generic types or parameters are custom generic constraints!");
			}
			else if (isType1CustomGenericType)
			{
				var type1Instance = assembly.CreateInstance(type1.FullName) as BaseGenericConstraint;
				isMatch = type1Instance.IsMatch(type2);
			}
			else if (isType2CustomGenericType)
			{
				var type2Instance = assembly.CreateInstance(type2.FullName) as BaseGenericConstraint;
				isMatch = type2Instance.IsMatch(type1);
			}
			else
			{
				isMatch = type1.Equals(type2);
			}

			return isMatch;
		}
	}
}
