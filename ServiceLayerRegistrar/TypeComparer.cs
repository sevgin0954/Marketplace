using ServiceLayerRegistrar.GenericTypes;
using System;
using System.Reflection;

namespace ServiceLayerRegistrar
{
	internal static class TypeComparer
	{
		public static bool CompareInterfaces(Type interface1, Type interface2)
		{
			if (interface1.IsGenericType != interface2.IsGenericType)
				return false;

			var isInterface1OpenGenericType = IsTypeOpenGeneric(interface1);
			var isInterface2OpenGenericType = IsTypeOpenGeneric(interface2);
			if (isInterface1OpenGenericType || isInterface2OpenGenericType)
			{
				return interface1.Name == interface2.Name;
			}
			else
			{
				return IsGenericTypeArgumentsMatch(interface1.GenericTypeArguments, interface2.GenericTypeArguments);
			}
		}

		private static bool IsGenericTypeArgumentsMatch(Type[] interface1Arguments, Type[] interface2Arguments)
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

		public static bool IsTypeOpenGeneric(Type type)
		{
			if (type.IsGenericType == false || type.GetGenericArguments().Length == 0)
				return false;

			if (type.GetGenericArguments().Length == type.GenericTypeArguments.Length)
				return false;

			return true;
		}
	}
}
