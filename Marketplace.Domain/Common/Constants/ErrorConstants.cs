using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Marketplace.Tests")]
namespace Marketplace.Domain.Common.Constants
{
	internal static class ErrorConstants
	{
		public static string NO_RECORD_ALTERED = "No records was altered!";
		public static string INITIATOR_SHOULD_BE_THE_BUYER = "The initiator should be the buyer!";
		public static string INITIATOR_SHOULD_BE_THE_SELLER = "The initiator should be the seller!";
	}
}
