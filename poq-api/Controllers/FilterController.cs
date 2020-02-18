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
        private EndpointConfiguration Endpoints { get; set; }

        public FilterController(EndpointConfiguration endpoints)
        {
            Endpoints = endpoints;
        }

        // GET api/filter
        [HttpGet]
        public async Task<ActionResult<FilterResult>> Get(int? minprice, int? maxprice, string size, string highlight)
        {
            var productService = ServiceFactory.CreateProductService(Endpoints.ProductsUrl);
            return await productService.FilterProducts(minprice, maxprice, size, highlight);
        }

    }
}