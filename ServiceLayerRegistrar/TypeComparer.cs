using ServiceLayerRegistrar.GenericConstraints;
using System;
using System.Reflection;

namespace ServiceLayerRegistrar
{
	internal static class TypeComparer
	{
		public static bool CompareTypes(Type type1, Type type2)
		{
			ArgumentValidator.ThrowExceptionIfNull(new object[] { type1, type2 }, nameof(type1), nameof(type2));

			if (type1.IsGenericType != type2.IsGenericType)
				return false;

			if (type1.AssemblyQualifiedName != type2.AssemblyQualifiedName)
				return false;

			var areTypesGeneric = type1.IsGenericType;
			if (areTypesGeneric)
			{
				if (type1.IsGenericTypeDefinition != type2.IsGenericTypeDefinition)
					return false;

				var areGenericTypesClosed = type1.IsGenericTypeDefinition == false;
				if (areGenericTypesClosed)
				{
					return CompareGenericTypeArguments(type1.GenericTypeArguments, type2.GenericTypeArguments);
				}
			}

			return true;
		}

		private static bool CompareGenericTypeArguments(Type[] interface1Arguments, Type[] interface2Arguments)
		{
			if (interface1Arguments.Length != interface2Arguments.Length)
				return false;

			var isAllGenericTypesMatch = true;

			for (var i = 0; i < interface1Arguments.Length; i++)
			{
				if (CompareGenericTypeArguments(interface1Arguments[i], interface2Arguments[i]) == false)
				{
					isAllGenericTypesMatch = false;
					break;
				}
			}

			return isAllGenericTypesMatch;
		}

		public static bool CompareGenericTypeArguments(Type type1, Type type2)
		{
			var isType1CustomGenericType = typeof(BaseGenericType).IsAssignableFrom(type1);
			var isType2CustomGenericType = typeof(BaseGenericType).IsAssignableFrom(type2);

			var isMatch = false;

			var assembly = Assembly.GetExecutingAssembly();
			if (isType1CustomGenericType && isType2CustomGenericType)
			{
				throw new ArgumentException("Both types are custom types!");
			}
			else if (isType1CustomGenericType)
			{
				var type1Instance = assembly.CreateInstance(type1.FullName) as BaseGenericType;
				isMatch = type1Instance.IsMatch(type2);
			}
			else if (isType2CustomGenericType)
			{
				var type2Instance = assembly.CreateInstance(type2.FullName) as BaseGenericType;
				isMatch = type2Instance.IsMatch(type1);
			}
			else
			{
				if (type1.IsGenericParameter)
				{
					type1 = type1.BaseType;
				}
				if (type2.IsGenericParameter)
				{
					type2 = type2.BaseType;
				}

				isMatch = type1 == type2;
			}

			return isMatch;
		}
	}
}
