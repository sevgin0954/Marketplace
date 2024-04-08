namespace Marketplace.API.Attributes.Validation
{
    public class NotNullOrEmptyStringInCollectionAttribute : BaseCollectionElementsValidationAttribute
    {
        public NotNullOrEmptyStringInCollectionAttribute()
        {
            this.ErrorMessage = "The collections contains null or empty strings!";
		}

        protected override Predicate<object> IsValidPredicate => obj => string.IsNullOrEmpty(obj as string) == false;

        protected override Type ElementsType => typeof(string);
    }
}
