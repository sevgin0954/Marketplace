using System;

namespace Marketplace.Domain.Common.Exceptions
{
	public class CommandNotSendException : Exception
	{
		public CommandNotSendException(string commandType, string errorMessage)
		{
			this.CommandType = commandType;
			this.ErrorMessage = errorMessage;
		}

		public string CommandType { get; }

		public string ErrorMessage { get; }
	}
}
