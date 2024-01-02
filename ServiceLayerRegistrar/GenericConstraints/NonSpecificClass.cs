using System;

namespace ServiceLayerRegistrar.GenericConstraints
{
	public class NonSpecificClass : BaseGenericType
	{
		internal override bool IsMatch(Type type)
		{
			var isNonSpecific = type.BaseType.BaseType == null;
			if (type.IsClass && isNonSpecific)
			{
				return true;
			}

			return false;
		}
	}
}
