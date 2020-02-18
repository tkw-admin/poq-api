using Microsoft.AspNetCore.Mvc;
using poq_api.Business;
using poq_api.Business.Products;
using poq_api.Configuration;
using System.Threading.Tasks;

namespace poq_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private IProductClient ProductClient { get; set; }

        public FilterController(IProductClient productClient)
        {
            ProductClient = productClient;
        }

        // GET api/filter
        [HttpGet]
        public async Task<ActionResult<FilterResult>> Get(int? maxprice, string size, string highlight)
        {
            var productService = ServiceFactory.CreateProductService(ProductClient);
            return await productService.FilterProducts(maxprice, size, highlight);
        }
    }
}