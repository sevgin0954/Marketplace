using ServiceLayerRegistrar.Tests.TestClasses;
using ServiceLayerRegistrar.Tests.TestInterfaces;

namespace ServiceLayerRegistrar.Tests.TestClasses
{
    internal class TestGenericClass1<T1, T2> : TestGenericInterface1<T1, T2>
		where T1 : class
        where T2 : TestGenericParameter2
	{
    }
}
