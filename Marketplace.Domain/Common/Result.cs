using System;

namespace Marketplace.Domain.Common
{
	public struct Result
	{
		public Result(bool isFailure, string errorMessage)
		{
			if (isFailure && string.IsNullOrWhiteSpace(errorMessage))
				throw new InvalidOperationException();

			this.IsFailure = isFailure;
			this.ErrorMessage = errorMessage;
		}

		public bool IsFailure { get; }

		public bool IsSuccess => !this.IsFailure;

		public string ErrorMessage { get; }

		public static Result Ok()
		{
			return new Result(false, string.Empty);
		}

		public static Result Fail(string errorMessage)
		{
			return new Result(true, errorMessage);
		}
	}
}
