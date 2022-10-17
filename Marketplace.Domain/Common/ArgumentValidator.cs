using System;

namespace Marketplace.Domain.Common
{
	internal static class ArgumentValidator
	{
		public static void NotNullValidator(object obj, string argumentName)
		{
			if (obj == null)
				throw new ArgumentNullException(argumentName);
		}

		public static void NotNullOrEmpty(string str, string argumentName)
		{
			var EXCEPTION_MESSAGE = $"The argument {argumentName} cant be null or empty";

			if (string.IsNullOrWhiteSpace(str))
				throw new ArgumentException(EXCEPTION_MESSAGE);
		}

		public static void MaxLength(string str, int maxLength, string argumentName)
		{
			if (str.Length > maxLength)
			{
				var exceptionMessage = $"The string should be less than {maxLength} characters long!";
				throw new ArgumentOutOfRangeException(argumentName, exceptionMessage);
			}
		}

		public static void MinLength(string str, int minLength, string argumentName)
		{
			if (str.Length > minLength)
			{
				var exceptionMessage = $"The string should be at least {minLength} characters long!";
				throw new ArgumentOutOfRangeException(argumentName, exceptionMessage);
			}
		}

		public static void MinValue(int value, int minValue, string argumentName)
		{
			if (value < minValue)
			{
				var exceptionMessage = $"The value should not be less than {minValue}";
				throw new ArgumentOutOfRangeException(argumentName, exceptionMessage);
			}
		}
	}
}
