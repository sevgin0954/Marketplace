using Marketplace.Domain.Common;

namespace Marketplace.Domain.Browsing
{
    public record Category : ValueObject
    {
        public Category(string name, Category parentCategory = null)
        {
            Name = name;
			ParentCategory = parentCategory;
        }


        public string Name { get; }

        public Category ParentCategory { get; }
    }
}
