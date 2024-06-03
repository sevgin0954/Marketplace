using Marketplace.Domain.Common;

namespace Marketplace.Domain.Browsing
{
    public record Category : ValueObject
    {
        public Category(string name, string subcateryName)
        {
            Name = name;
            SubcateryName = subcateryName;
        }


        public string Name { get; }

        public string SubcateryName { get; }
    }
}
