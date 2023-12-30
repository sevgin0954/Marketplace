using System;
using System.Reflection;

namespace ServiceLayerRegistrar.Interfaces
{
    public interface IServiceCollectionRegistrar
    {
        void AddScopedServices(Assembly assembly, Type interfaceType);
    }
}
