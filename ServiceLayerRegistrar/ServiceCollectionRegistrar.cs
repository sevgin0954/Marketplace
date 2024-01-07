using Microsoft.Extensions.DependencyInjection;
using ServiceLayerRegistrar.CustomGenericConstraints;
using ServiceLayerRegistrar.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ServiceLayerRegistrar
{
    public class ServiceCollectionRegistrar : IServiceCollectionRegistrar
    {
        private readonly IServiceCollection services;

        public ServiceCollectionRegistrar(IServiceCollection services)
        {
            this.services = services;
        }

        public void AddScopedServices(Assembly classesAssembly, Type interfaceType)
        {
            var assemblyTypeFinder = new AssemblyTypeFinder(classesAssembly);

            var interfaceGenericArguments = interfaceType.GenericTypeArguments;
            var isInterfaceCustom = interfaceGenericArguments.Any(a => typeof(BaseGenericConstraint).IsAssignableFrom(a));
            if (isInterfaceCustom)
            {
                var interfacesMatchingCustomInterface = assemblyTypeFinder.FindDistinctInterfacesMatchingInterface(interfaceType);

                foreach (var currentInterface in interfacesMatchingCustomInterface)
                {
                    var classToRegister = this.GetClosestMatchingClassForInterface(assemblyTypeFinder, currentInterface);
					this.AddScopedService(currentInterface, classToRegister);
				}
			}
            else
            {
				var classToRegister = this.GetClosestMatchingClassForInterface(assemblyTypeFinder, interfaceType);
				this.AddScopedService(interfaceType, classToRegister);
			}
        }

        private void AddScopedService(Type interfaceType, Type classType)
        {
            var isInterfaceOpenGenericType = interfaceType.IsGenericTypeDefinition;
            var isClassOpenGenericType = classType.IsGenericTypeDefinition;

			if (isInterfaceOpenGenericType == true && isClassOpenGenericType == false)
            {
                var classGenericArguments = this.ConverGenericTypesToConcreteIfAny(classType.GenericTypeArguments);
				interfaceType = interfaceType.MakeGenericType(classGenericArguments);
            }
            else if (isClassOpenGenericType == true && isInterfaceOpenGenericType == false)
            {
				var interfaceGenericArguments = 
                    this.ConverGenericTypesToConcreteIfAny(interfaceType.GenericTypeArguments);
				classType = classType.MakeGenericType(interfaceGenericArguments);
			}
            else
            {
				this.services.AddScoped(interfaceType, classType);
			}
		}

        private Type[] ConverGenericTypesToConcreteIfAny(Type[] types)
        {
            var convertedTypes = new List<Type>();

            foreach (var currentType in types)
            {
                if (currentType.IsGenericParameter)
                {
                    var currentBaseType = currentType.BaseType;
                    if (currentType == null)
                        throw new Exception();

                    convertedTypes.Add(currentBaseType);
                }
                else
                {
                    convertedTypes.Add(currentType);
                }
            }

            return convertedTypes.ToArray();
        }

        private Type GetClosestMatchingClassForInterface(AssemblyTypeFinder assemblyTypeFinder, Type interfaceType)
        {
			var classesImplementingInteface = assemblyTypeFinder.FindAllClassesMatchingInterface(interfaceType);

			var classTypeToRegister = this.GetLowestInheritanceLevelClassImlementingInterface(
				classesImplementingInteface,
				interfaceType);

            return classTypeToRegister;
		}

        private Type GetLowestInheritanceLevelClassImlementingInterface(ICollection<Type> classesTypes, Type interfaceType)
        {
			var minDepthLevel = int.MaxValue;
			Type lowestDepthLevelClassType = null;

			foreach (var currentClassType in classesTypes)
			{
				var currentDepthLevel =
					this.FindMinimumDepthLevelAtWhichClassImplementsInterface(currentClassType, interfaceType);
                var isAnyClassAtLowerDepthLevelExist = minDepthLevel == currentDepthLevel && lowestDepthLevelClassType != null;
				if (isAnyClassAtLowerDepthLevelExist)
				{
					var exceptionMessage = 
                        "More than one classes at the same level of inheritance to register for " +
						$"${interfaceType} interface";
					throw new InvalidOperationException(exceptionMessage);
				}
				if (currentDepthLevel < minDepthLevel)
				{
					lowestDepthLevelClassType = currentClassType;
					minDepthLevel = currentDepthLevel;
				}
			}

			if (lowestDepthLevelClassType == null)
			{
				throw new ArgumentException($"No classes found for the selelcted interface - ${interfaceType}");
			}

			return lowestDepthLevelClassType;
		}

        private int FindMinimumDepthLevelAtWhichClassImplementsInterface(
            Type classType,
            Type interfaceType,
            int currentDepthLevel = 0,
            int minDepthLevel = -1)
        {
            var directInterfaces = TypeFilterer.GetDirectDerivedTypes(classType);

            if (directInterfaces.Count == 0)
            {
                return -1;
            }
			if (minDepthLevel != -1)
			{
				return minDepthLevel;
			}
            
			if (this.CheckIfInteraceExists(directInterfaces, interfaceType) == true)
            {
                return currentDepthLevel;
            }

            foreach (var currentDirectInterface in directInterfaces)
            {
				return this.FindMinimumDepthLevelAtWhichClassImplementsInterface(
				currentDirectInterface,
				interfaceType, 
                currentDepthLevel + 1,
				minDepthLevel);
			}

            return minDepthLevel;
        }

        private bool CheckIfInteraceExists(IEnumerable<Type> interfacesToSearch, Type searchInterface)
        {
            foreach (var currentInterface in interfacesToSearch)
            {
                var isInterfaceMatch = TypeComparer.CompareTypes(currentInterface, searchInterface);
                if (isInterfaceMatch == true)
                {
                    return isInterfaceMatch;
                }
            }

            return false;
        }
    }
}