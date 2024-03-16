using System;

namespace ServiceLayerRegistrar.CustomGenericConstraints
{
	public abstract class BaseGenericConstraint
	{
		internal abstract bool IsMatch(Type type);
	}
}
