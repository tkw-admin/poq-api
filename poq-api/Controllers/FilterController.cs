using Microsoft.AspNetCore.Mvc;
using poq_api.Business.Products;
using System.Threading.Tasks;

namespace poq_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private IProductService ProductService { get; set; }

        public FilterController(IProductService productService)
        {
            ProductService = productService;
        }

        // GET api/filter
        [HttpGet]
        public async Task<ActionResult<FilterResult>> Get([FromQuery]FilterQuery query)
        {
            var result = await ProductService.FilterProducts(query);
            return result;
        }
    }
}