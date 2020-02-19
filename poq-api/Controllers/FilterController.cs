using Microsoft.AspNetCore.Mvc;
using poq_api.Business;
using poq_api.Business.Products;
using System.Threading.Tasks;

namespace poq_api.Controllers
{
    public class FilterController : BaseApiController
    {
        private readonly IProductService _productService;
        private readonly IAppLogger<FilterController> _logger;

        public FilterController(IProductService productService, IAppLogger<FilterController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> Get(int? maxprice, string size, string highlight)
        {
            _logger.LogInformation("Search by product....");
            var response = await _productService.FilterProducts(maxprice, size, highlight);
            return Ok(response);
        }
    }
}