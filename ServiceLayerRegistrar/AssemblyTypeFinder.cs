using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;

namespace ServiceLayerRegistrar
{
	internal class AssemblyTypeFinder
	{
		private readonly Assembly assembly;

		public AssemblyTypeFinder(Assembly assembly)
		{
			this.assembly = assembly;
		}

		public ICollection<Type> FindDistinctInterfacesMatchingInterface(Type interfaceType)
		{
			ArgumentValidator.ThrowExceptionIfNull(interfaceType, nameof(interfaceType));

			var matchingClasses = this.FindAllClassesMatchingInterface(interfaceType);
			var classesInterfaces = matchingClasses
				.SelectMany(c => c.GetInterfaces())
				.ToList();

			var matchingInterfaces = classesInterfaces
				.Where(i => TypeComparer.DoesTypeMatch(i, interfaceType))
				.Distinct()
				.ToList();

			if (matchingInterfaces.Count == 0)
				throw new ArgumentException($"No interfaces matching the selected interface - {interfaceType.Name}");

			return matchingInterfaces;
		}

		public ICollection<Type> FindAllClassesMatchingInterface(Type interfaceType)
		{
			ArgumentValidator.ThrowExceptionIfNull(interfaceType, nameof(interfaceType));

			var filterClassesFunc = new Func<Type, bool>(
				t => t.IsClass &&
				t.GetInterface(interfaceType.Name) != null &&
				t.GetInterfaces().Any(i => TypeComparer.DoesTypeMatch(i, interfaceType)) &&
				t.IsAbstract == false
			);
			var classesImplementingInteface = TypeFilterer.FilterTypesFromAssembly(this.assembly, filterClassesFunc);

			return classesImplementingInteface;
		}
	}
}
