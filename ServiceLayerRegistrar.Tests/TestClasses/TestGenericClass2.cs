using ServiceLayerRegistrar.Tests.TestInterfaces;

namespace ServiceLayerRegistrar.Tests.TestClasses
{
	internal class TestGenericClass2<T1, T2> : TestGenericInterface2<T1, T2>
		where T1 : BaseGenericParameter
		where T2 : TestGenericParameter2
	{
	}
}
