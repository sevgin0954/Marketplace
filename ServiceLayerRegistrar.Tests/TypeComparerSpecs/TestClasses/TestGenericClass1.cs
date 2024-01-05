using ServiceLayerRegistrar.Tests.TypeComparerSpecs.TypeInterfaces;

namespace ServiceLayerRegistrar.Tests.TypeComparerSpecs.TestClasses
{
    internal class TestGenericClass1<T1, T2> : TestGenericInterface1<T1, T2>
		where T1 : class
        where T2 : TestGenericParameter2
	{
    }
}
