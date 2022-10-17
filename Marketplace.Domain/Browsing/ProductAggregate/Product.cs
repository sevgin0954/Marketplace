using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Domain.Browsing.ProductAggregate
{
	public class Product : AggregateRoot<Id>
	{
		private string name = null!;
		private string description = null!;

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

				ArgumentValidator.MaxLength(trimedName, ProductConstants.MAX_NAME_LENGTH, nameof(this.Name));
				ArgumentValidator.MinLength(trimedName, ProductConstants.MIN_NAME_LENGTH, nameof(this.Name));

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

				ArgumentValidator.MaxLength(trimedDescription, ProductConstants.MAX_DESCRIPTION_LENGTH, nameof(this.Description));
				ArgumentValidator.MinLength(trimedDescription, ProductConstants.MIN_DESCRIPTION_LENGTH, nameof(this.Description));

				if (trimedDescription.Length > 0)
					this.description = trimedDescription;
			}
		}
	}
}