using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Domain.Browsing.ProductAggregate
{
	public class Product : AggregateRoot<Id>
	{
		private string name;
		private string description;

		public Product(Id id, string name, string description)
			: base(id)
		{
			this.Name = name;
			this.Description = description;
		}

		public string Name
		{
			get { return this.name; }
			set
			{
				ArgumentValidator.NotNullOrEmpty(value, nameof(this.Name));

				var trimedName = value.Trim();

				ArgumentValidator.StringInRange(
					trimedName, 
					ProductConstants.MIN_NAME_LENGTH, 
					ProductConstants.MAX_NAME_LENGTH, 
					nameof(this.Name));

				this.name = trimedName;
			}
		}

		public string Description
		{
			get { return this.description; }
			set
			{
				if (value == null)
					return;

				var trimedDescription = value.Trim();

				ArgumentValidator.StringInRange(
					trimedDescription, 
					ProductConstants.MIN_DESCRIPTION_LENGTH, 
					ProductConstants.MAX_DESCRIPTION_LENGTH, 
					nameof(this.Description));

				if (trimedDescription.Length > 0)
					this.description = trimedDescription;
			}
		}
	}
}