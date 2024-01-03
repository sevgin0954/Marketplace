using System;

namespace ServiceLayerRegistrar.CustomGenericConstraints
{
	public class NonSpecifiedClass : BaseGenericConstraint
	{
		internal override bool IsMatch(Type type)
		{
			var isClassTypeNotSpecified = type.BaseType.BaseType == null;
			if (type.IsClass && type.IsGenericTypeParameter && isClassTypeNotSpecified)
			{
				return true;
			}

			return false;
		}
	}
}
