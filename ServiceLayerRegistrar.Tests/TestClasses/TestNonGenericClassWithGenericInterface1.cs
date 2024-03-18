using ServiceLayerRegistrar.Tests.TestInterfaces;

namespace ServiceLayerRegistrar.Tests.TestClasses
{
	public class TestNonGenericClassWithGenericInterface1 : TestGenericInterface1<TestGenericParameter1, TestGenericParameter1>
	{
	}
}
