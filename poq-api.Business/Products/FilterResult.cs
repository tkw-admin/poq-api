using System.Collections.Generic;

namespace poq_api.Business.Products
{
    public class FilterResult
    {
        public FilterOptions FilterOptions { get; set; }
        public List<Product> Products { get; set; }
    }
}
