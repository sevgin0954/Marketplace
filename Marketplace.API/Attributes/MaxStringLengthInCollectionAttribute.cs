using System.ComponentModel.DataAnnotations;

namespace Marketplace.API.Attributes
{
    public class MaxStringLengthInCollectionAttribute : BaseCollectionElementsValidationAttribute
    {
        private readonly int maxLength;

        public MaxStringLengthInCollectionAttribute(int maxLength)
        {
            this.maxLength = maxLength;
        }

        protected override Predicate<object> IsValidPredicate => obj => (obj as string).Length <= maxLength;

        protected override Type ElementsType => typeof(string);

        protected override string ErrorMessage => "String is too large!";
    }
}
