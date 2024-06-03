using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Domain.Browsing.CategoryAggregate
{
    public class Category : AggregateRoot
    {
        public Category(string name, Category parentCategory = null)
            : base(new Id(name))
        {
            Name = name;
            ParentCategory = parentCategory;
        }


        public string Name { get; }

        public Category ParentCategory { get; }
    }
}
