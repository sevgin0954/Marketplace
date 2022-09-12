﻿using System;

namespace Marketplace.Domain.Common
{
	internal static class ArgumentValidator
	{
		public static void NotNullValidator(object obj, string argumentName)
		{
			if (obj == null)
				throw new ArgumentNullException(argumentName);
		}

		public static void NotNullOrEmpty(string obj, string argumentName)
		{
			var EXCEPTION_MESSAGE = $"The argument {argumentName} cant be null or empty";

			if (string.IsNullOrWhiteSpace(obj))
				throw new ArgumentException(EXCEPTION_MESSAGE);
		}
	}
}
