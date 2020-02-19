using Microsoft.AspNetCore.Mvc;
using poq_api.Business;
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
        public async Task<IActionResult> Get([FromQuery]FilterQueryRequest query)
        {
            _logger.LogInformation("Call search products....");
            var response = await _productService.FilterProducts(query);
            return Ok(response);
        }
    }
}