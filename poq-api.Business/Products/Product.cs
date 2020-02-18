﻿using System.Collections.Generic;

namespace poq_api.Business.Products
{
    public class Product
    {
        public string Title { get; set; }
        public int Price { get; set; }
        public List<string> Sizes { get; set; }
        public string Description { get; set; }
    }
}
