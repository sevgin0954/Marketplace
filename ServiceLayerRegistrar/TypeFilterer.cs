using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System;
using System.Linq;

namespace ServiceLayerRegistrar
{
	internal static class TypeFilterer
	{
		public static ICollection<Type> FilterTypesFromAssembly(Assembly assembly, Func<Type, bool> filterFunc)
		{
			var classesTypes = new List<Type>();

			var servicesTypes = assembly.GetTypes();
			foreach (var type in servicesTypes)
			{
				var isCompilerGenerated = type.GetCustomAttribute<CompilerGeneratedAttribute>() != null;
				if (isCompilerGenerated == false && filterFunc(type))
				{
					classesTypes.Add(type);
				}
			}

			return classesTypes;
		}

		public static ICollection<Type> GetDirectDerivedTypes(Type classType)
		{
			var classImplementedInterfaces = classType.GetInterfaces().ToHashSet<Type>();

			var directInterfaces = new List<Type>();

			var classBaseType = classType.BaseType;
			if (classBaseType != null)
			{
				var classImplementedTypes = new HashSet<Type>(classImplementedInterfaces);
				classImplementedTypes.Add(classBaseType);
				var directTypes = RemoveUndirectIntefaces(classImplementedTypes);

				directInterfaces = directTypes;
			}
			else
			{
				directInterfaces = RemoveUndirectIntefaces(classImplementedInterfaces);
			}

			return directInterfaces;
		}

		private static List<Type> RemoveUndirectIntefaces(HashSet<Type> types)
		{
			var typesClone = new HashSet<Type>(types);

			foreach (var currentType in typesClone)
			{
				var currentTypeInterfaces = currentType.GetInterfaces();
				foreach (var currentTypeImplementedInterface in currentTypeInterfaces)
				{
					if (typesClone.Contains(currentTypeImplementedInterface) == true)
					{
						typesClone.Remove(currentTypeImplementedInterface);
					}
				}
			}

			return typesClone.ToList();
		}
	}
}
