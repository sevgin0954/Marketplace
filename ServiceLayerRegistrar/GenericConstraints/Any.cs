using System;

namespace ServiceLayerRegistrar.GenericConstraints
{
	public class Any : BaseGenericType
	{
		internal override bool IsMatch(Type type)
		{
			return true;
		}
	}
}
