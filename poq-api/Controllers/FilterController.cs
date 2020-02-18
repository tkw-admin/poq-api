using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using poq_api.Business;
using poq_api.Business.Products;
using System.Threading.Tasks;

namespace poq_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private IProductClient ProductClient { get; set; }
        private ILogger Logger { get; set; }

        public FilterController(IProductClient productClient, ILogger<FilterController> logger)
        {
            ProductClient = productClient;
            Logger = logger;
        }

        // GET api/filter
        [HttpGet]
        public async Task<ActionResult<FilterResult>> Get(int? maxprice, string size, string highlight)
        {
            var productService = ServiceFactory.CreateProductService(ProductClient, Logger);
            return await productService.FilterProducts(maxprice, size, highlight);
        }
    }
}