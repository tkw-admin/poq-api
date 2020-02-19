using System;
using System.Collections.Generic;
using System.Text;

namespace poq_api.Business
{
    public class FilterResult
    {
        public FilterOptions FilterOptions { get; set; }
        public List<Product> Products { get; set; }
    }
}
