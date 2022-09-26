using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers
{
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public ProductsController()
        {

        }

        public JsonResult GetProducts()
        {
            var products = // getProducts();

            return new JsonResult({
                products
            });
        }
    }
}
