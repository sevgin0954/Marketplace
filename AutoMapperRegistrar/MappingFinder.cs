using AutoMapperRegistrar.Interfaces;
using System.Reflection;

namespace AutoMapperRegistrar
{
	public class MappingFinder
	{
		public static ICollection<MappingType> GetTypesWithMapFrom(Assembly assembly)
		{
			var typesWithMapFrom = GetTypesDerivedFrom(assembly, typeof(IMappableFrom<>));

			var mappingTypes = new List<MappingType>();

			foreach (var currentType in typesWithMapFrom)
			{
				var interfaceGenericType = GetInterfacesGenericTypes(currentType, typeof(IMappableFrom<>));

				var currentMappingTypes = interfaceGenericType
					.Select(t => new MappingType(t, currentType))
					.ToList();

				mappingTypes.AddRange(currentMappingTypes);
			}

			return mappingTypes;
		}

		public static ICollection<MappingType> GetTypesWithMapTo(Assembly assembly)
		{
			var typesWithMapTo = GetTypesDerivedFrom(assembly, typeof(IMappableTo<>));

			var mappingTypes = new List<MappingType>();

			foreach (var currentType in typesWithMapTo)
			{
				var interfaceGenericType = GetInterfacesGenericTypes(currentType, typeof(IMappableFrom<>));

				var currentMappingTypes = interfaceGenericType
					.Select(t => new MappingType(currentType, t))
					.ToList();

				mappingTypes.AddRange(currentMappingTypes);
			}

			return mappingTypes;
		}

		public static ICollection<ICustomMappings> GetTypesWitCustomMapping(Assembly assembly)
		{
			var typesWithCustomMappings = 
				 GetTypesDerivedFrom(assembly, typeof(ICustomMappings))
				.Select(t => (ICustomMappings)t)
				.ToList();

			return typesWithCustomMappings;
		}

		private static ICollection<Type> GetTypesDerivedFrom(Assembly assembly, Type type)
		{
			var types = assembly
				.GetTypes()
				.Where(t => type.IsAssignableFrom(t))
				.ToList();

			return types;
		}

		private static ICollection<Type> GetInterfacesGenericTypes(Type type, Type interfaceType)
		{
			if (interfaceType.IsInterface == false || interfaceType.IsGenericType == false)
				throw new ArgumentException($"type {interfaceType} is not a generic interface!");

			if (interfaceType.IsAssignableFrom(type) == false)
				throw new ArgumentException($"{type} is does not implement {interfaceType}!");

			var genericTypes = type.GetInterfaces()
				.Where(i => i.GetGenericTypeDefinition() == interfaceType)
				.Select(i => i.GetGenericArguments().First())
				.ToList();
			return genericTypes;
		}
	}
}
