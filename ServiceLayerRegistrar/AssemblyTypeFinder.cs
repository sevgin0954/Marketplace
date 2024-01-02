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

		public ICollection<Type> GetInterfacesMatchingCustomInterface(Type customInterface)
		{
			var matchingClasses = this.GetAllClassesMatchingInterface(customInterface);
			var allInterfaces = matchingClasses
				.SelectMany(c => c.GetInterfaces())
				.ToList();

			var matchingInterfaces = allInterfaces
				.Where(i => TypeComparer.CompareTypes(i, customInterface))
				.Distinct()
				.ToList();

			return matchingInterfaces;
		}

		public ICollection<Type> GetAllClassesMatchingInterface(Type interfaceType)
		{
			var filterClassesFunc = new Func<Type, bool>(
				t => t.IsClass &&
				t.GetInterface(interfaceType.Name) != null &&
				t.GetInterfaces().Any(i => TypeComparer.CompareTypes(i, interfaceType)) &&
				t.IsAbstract == false
			);
			var classesImplementingInteface = TypeFilterer.FilterTypesFromAssembly(this.assembly, filterClassesFunc);

			return classesImplementingInteface;
		}
	}
}
