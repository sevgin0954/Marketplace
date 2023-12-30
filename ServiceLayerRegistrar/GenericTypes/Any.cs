using System;

namespace ServiceLayerRegistrar.GenericTypes
{
	public class Any : BaseGenericType
	{
		internal override bool IsMatch(Type type)
		{
			return true;
		}
	}
}
