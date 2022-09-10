using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Marketplace.Tests")]
namespace Marketplace.Domain.Common.Constants
{
	internal static class ErrorConstants
	{
		public static string NO_RECORD_ALTERED = "No records was altered!";
		public static string BUYER_CANT_BE_THE_INITIATOR = "Buyer can't be the initiator for this operation!";
	}
}
