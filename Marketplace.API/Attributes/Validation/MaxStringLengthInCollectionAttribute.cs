namespace Marketplace.API.Attributes.Validation
{
    public class MaxStringLengthInCollectionAttribute : BaseCollectionElementsValidationAttribute
    {
        private readonly int maxLength;

        public MaxStringLengthInCollectionAttribute(int maxLength)
        {
            this.maxLength = maxLength;
            this.ErrorMessage = "String is too large!";
		}

        protected override Predicate<object> IsValidPredicate => obj => (obj as string).Length <= maxLength;

        protected override Type ElementsType => typeof(string);
    }
}
