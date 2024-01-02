using System;

namespace ServiceLayerRegistrar
{
	internal static class ArgumentValidator
	{
		public static void ThrowExceptionIfNull(object obj, string paramName)
		{
			if (obj == null)
				throw new ArgumentNullException(paramName);
		}

		public static void ThrowExceptionIfNull(object[] objects, params string[] paramName)
		{
			for (int i = 0; i < objects.Length; i++)
			{
				var currentObj = objects[i];
				var currentParamName = paramName[i];
				ThrowExceptionIfNull(currentObj, currentParamName);
			}
		}
	}
}
