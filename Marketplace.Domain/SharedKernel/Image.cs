using System;

namespace Marketplace.Domain.SharedKernel
{
	public record Image
	{
		public Image(string id, int displayPriority = 1)
		{
			if (string.IsNullOrEmpty(id))
				throw new ArgumentException("Id should not be null or empty!");

			this.Id = id;
			this.DisplayPriority = displayPriority;
		}

		public string Id { get; }

		public int DisplayPriority { get; }
	}
}
