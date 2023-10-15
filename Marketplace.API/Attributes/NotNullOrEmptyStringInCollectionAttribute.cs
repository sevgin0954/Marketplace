namespace Marketplace.API.Attributes
{
    public class NotNullOrEmptyStringInCollectionAttribute : BaseCollectionElementsValidationAttribute
    {
        protected override Predicate<object> IsValidPredicate => obj => string.IsNullOrEmpty(obj as string);

        protected override Type ElementsType => typeof(string);

        protected override string ErrorMessage => "The collections contains null or empty strings!";
    }
}
