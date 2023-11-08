using System;

namespace Marketplace.Domain.Common
{
	public struct Result
	{
		public Result(bool isFailure, string? errorMessage = null)
		{
			if (isFailure && string.IsNullOrWhiteSpace(errorMessage))
				throw new InvalidOperationException();

			this.IsFailure = isFailure;
			this.ErrorMessage = errorMessage;
		}

		public bool IsFailure { get; }

		public bool IsSuccess => !this.IsFailure;

		public string? ErrorMessage { get; }

		public object? ResultObject { get; private set; } = null;

		public static Result Ok()
		{
			return new Result(false, null);
		}

		public static Result Fail(string errorMessage, object? resultObject = null)
		{
			var result = new Result(true, errorMessage);
			result.ResultObject = result;

			return result;
		}
	}
}
