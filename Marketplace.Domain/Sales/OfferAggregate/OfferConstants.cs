namespace Marketplace.Domain.Sales.OfferAggregate
{
	public static class OfferConstants
	{
		internal const string CANT_DISCARD_NON_PENDING_OFFER = "Can't discard non pending offer!";
		internal const string CANT_ACCEPT_NON_PENDING_OFFER = "Can't accept non pending offer!";
		internal const string CANT_REJECT_NON_PENDING_OFFER = "Can't reject non pending offer!";

		public const int MESSAGE_MAX_LENGTH = 800;
		public const int REJECT_MESSAGE_MAX_LENGTH = 800;
	}
}
