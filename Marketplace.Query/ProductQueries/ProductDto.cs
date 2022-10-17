namespace Marketplace.Query.ProductQueries
{
    public class ProductDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string SellerId { get; set; }

        public string SellerName { get; set; }

        public string Status { get; set; }

        public PriceDto Price { get; set; }
    }
}
