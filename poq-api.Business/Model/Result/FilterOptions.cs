using System;
using System.Collections.Generic;
using System.Text;

namespace poq_api.Business
{
    public class FilterOptions
    {
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public List<string> Sizes { get; set; }
        public List<string> CommonWords { get; set; }
    }
}
