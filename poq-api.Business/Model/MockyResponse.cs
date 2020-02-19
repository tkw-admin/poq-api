using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace poq_api.Business
{
    public partial class MockyResponse
    {
        [JsonProperty("products")]
        public List<MockyProduct> Products { get; set; }

        [JsonProperty("apiKeys")]
        public ApiKeys ApiKeys { get; set; }
    }

    public partial class ApiKeys
    {
        [JsonProperty("primary")]
        public string Primary { get; set; }

        [JsonProperty("secondary")]
        public string Secondary { get; set; }
    }

    public partial class MockyProduct
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("sizes")]
        public List<string> Sizes { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

   
}
