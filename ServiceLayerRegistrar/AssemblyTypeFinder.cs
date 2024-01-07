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
			var allInterfaces = matchingClasses
				.SelectMany(c => c.GetInterfaces())
				.ToList();

			var matchingInterfaces = allInterfaces
				.Where(i => TypeComparer.CompareTypes(i, interfaceType))
				.Distinct()
				.ToList();

			return matchingInterfaces;
		}

		public ICollection<Type> FindAllClassesMatchingInterface(Type interfaceType)
		{
			ArgumentValidator.ThrowExceptionIfNull(interfaceType, nameof(interfaceType));

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
