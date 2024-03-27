using AutoMapperRegistrar.Interfaces;
using System.Reflection;

namespace AutoMapperRegistrar
{
	// TODO: Refactor
	public class MappingFinder
	{
		public static ICollection<MappingType> GetTypesWithMapFrom(params Assembly[] assemblies)
		{
			var resultTypes = new List<MappingType>();

			foreach (var currentAssembly in assemblies)
			{
				var currentTypes = GetTypesWithMapFrom(currentAssembly);
				resultTypes.AddRange(currentTypes);
			}

			return resultTypes;
		}

		private static ICollection<MappingType> GetTypesWithMapFrom(Assembly assembly)
		{
			var typesWithMapFrom = GetTypesDerivedFrom(assembly, typeof(IMappableFrom<>));

			var mappingTypes = new List<MappingType>();

			foreach (var currentType in typesWithMapFrom)
			{
				var typeDerivedInterfaces = GetDerivedGenericInterfaces(currentType, typeof(IMappableFrom<>));
				var interfacesGenericArguments = GetInterfacesGenericTypeArguments(typeDerivedInterfaces);

				var currentMappingTypes = interfacesGenericArguments
					.Select(t => new MappingType(t, currentType))
					.ToList();

				mappingTypes.AddRange(currentMappingTypes);
			}

			return mappingTypes;
		}

		public static ICollection<MappingType> GetTypesWithMapTo(params Assembly[] assemblies)
		{
			var resultTypes = new List<MappingType>();

			foreach (var currentAssembly in assemblies)
			{
				var currentTypes = GetTypesWithMapTo(currentAssembly);
				resultTypes.AddRange(currentTypes);
			}

			return resultTypes;
		}

		private static ICollection<MappingType> GetTypesWithMapTo(Assembly assembly)
		{
			// TODO: Rename
			var typesWithMapTo = GetTypesDerivedFrom(assembly, typeof(IMappableTo<>));

			var mappingTypes = new List<MappingType>();

			foreach (var currentType in typesWithMapTo)
			{
				var typeDerivedInterfaces = GetDerivedGenericInterfaces(currentType, typeof(IMappableTo<>));
				var interfacesGenericArguments = GetInterfacesGenericTypeArguments(typeDerivedInterfaces);

				var currentMappingTypes = interfacesGenericArguments
					.Select(t => new MappingType(currentType, t))
					.ToList();

				mappingTypes.AddRange(currentMappingTypes);
			}

			return mappingTypes;
		}

		public static ICollection<MappingType> GetTypesWithMapBothDirections(params Assembly[] assemblies)
		{
			var resultTypes = new List<MappingType>();

			foreach (var currentAssembly in assemblies)
			{
				var currentTypes = GetTypesWithMapBothDirections(currentAssembly);
				resultTypes.AddRange(currentTypes);
			}

			return resultTypes;
		}

		private static ICollection<MappingType> GetTypesWithMapBothDirections(Assembly assembly)
		{
			var typesWithMapTo = GetTypesDerivedFrom(assembly, typeof(IMappableBothDirections<>));

			var mappingTypes = new List<MappingType>();

			foreach (var currentType in typesWithMapTo)
			{
				var typeDerivedInterfaces = GetDerivedGenericInterfaces(currentType, typeof(IMappableBothDirections<>));
				var interfacesGenericArguments = GetInterfacesGenericTypeArguments(typeDerivedInterfaces);

				var currentMappingTypes = interfacesGenericArguments
					.Select(t => new MappingType(currentType, t, true))
					.ToList();

				mappingTypes.AddRange(currentMappingTypes);
			}

			return mappingTypes;
		}

		public static ICollection<ICustomMappings> GetTypesWitCustomMapping(params Assembly[] assemblies)
		{
			var resultTypes = new List<ICustomMappings>();

			foreach (var currentAssembly in assemblies)
			{
				var currentTypes = GetTypesWitCustomMapping(currentAssembly);
				resultTypes.AddRange(currentTypes);
			}

			return resultTypes;
		}

		private static ICollection<ICustomMappings> GetTypesWitCustomMapping(Assembly assembly)
		{
			var typesWithCustomMappings = 
				 GetTypesDerivedFrom(assembly, typeof(ICustomMappings))
				.Select(t => Activator.CreateInstance(t) as ICustomMappings)
				.ToList();

			return typesWithCustomMappings;
		}

		private static ICollection<Type> GetTypesDerivedFrom(Assembly assembly, Type type)
		{
			var types = assembly.GetTypes().ToList();

			if (type.IsGenericType)
			{
				types = types
					.Where(t => t
						.GetInterfaces()
						.Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == type.GetGenericTypeDefinition())
				).ToList();
			}
			else
			{
				types = types
					.Where(t => t.GetInterfaces().Any(i => i == type))
					.ToList();
			}

			return types;
		}

		private static ICollection<Type> GetDerivedGenericInterfaces(Type type, Type searchedInterfaceTypeGenericTypeDefinition)
		{
			var derivedTypes = new List<Type>();

			derivedTypes = type.GetInterfaces()
				.Where(t => t.IsGenericType)
				.Where(t => t.GetGenericTypeDefinition() == searchedInterfaceTypeGenericTypeDefinition)
				.ToList();

			return derivedTypes;
		}
		
		private static ICollection<Type> GetInterfacesGenericTypeArguments(ICollection<Type> interfaces)
		{
			var result = interfaces.Select(i => i.GenericTypeArguments[0]).ToList();

			return result;
		}
	}
}
