using System;
using System.Reflection;

namespace ServiceLayerRegistrar.Interfaces
{
    public interface IServiceCollectionRegistrar
    {
        void RegisterScopedServices(Assembly assembly, Type interfaceType);
    }
}
