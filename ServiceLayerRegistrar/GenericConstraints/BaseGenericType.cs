using System;

namespace ServiceLayerRegistrar.GenericConstraints
{
	public abstract class BaseGenericType
	{
		internal abstract bool IsMatch(Type type);
	}
}
