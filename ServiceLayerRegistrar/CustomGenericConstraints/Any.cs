using System;

namespace ServiceLayerRegistrar.CustomGenericConstraints
{
	public class Any : BaseGenericConstraint
	{
		internal override bool IsMatch(Type type)
		{
			return true;
		}
	}
}
