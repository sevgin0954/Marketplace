using System;

namespace ServiceLayerRegistrar.GenericTypes
{
	public abstract class BaseGenericType
	{
		internal abstract bool IsMatch(Type type);
	}
}
