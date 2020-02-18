using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using poq_api.Business;
using poq_api.Business.Products;

namespace poq_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        // GET api/filter
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get(int maxprice, string size, string highlight)
        {
            var productService = ServiceFactory.CreateProductService("http://www.mocky.io/v2/5e307edf3200005d00858b49");
            return await productService.GetProducts();
        }

    }
}