using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace poq_api.Business.Products
{
    public class ProductService : IProductService
    {
        private const int MostCommonWordsToSkip = 5;
        private const int MostCommonWordsToTake = 10;

        private IProductClient Api { get; set; }
        private ILogger Logger { get; set; }

        public ProductService(IProductClient client, ILogger<ProductService> logger)
        {
            Api = client;
            Logger = logger;
        }

        public async Task<FilterResult> FilterProducts(int? maxprice, string size, string highlight)
        {
            var result = new FilterResult();

            var productResult = await Api.GetProducts();
            Logger.Log(LogLevel.Debug, "mocky.io response: " + JsonConvert.SerializeObject(productResult));
            var products = productResult.Products;

            result.FilterOptions = DetermineFilterOptions(products);
            products = FilterProducts(products, maxprice, size);
            HighlightDescriptionWords(products, highlight);
            result.Products = products;

            return result;
        }

        private List<Product> FilterProducts(List<Product> products, int? maxprice, string size)
        {
            if (maxprice.HasValue)
                products = products.Where(x => x.Price <= maxprice.Value).ToList();
            if (!string.IsNullOrEmpty(size))
            {
                var sizes = size.Split(",", StringSplitOptions.RemoveEmptyEntries);
                products = products.Where(x => x.Sizes.Intersect(sizes).Any()).ToList();
            }
            return products;
        }

        private FilterOptions DetermineFilterOptions(List<Product> products)
        {
            var filterOptions = new FilterOptions();
            if (products.Any())
            {
                filterOptions.MinPrice = products.Min(x => x.Price);
                filterOptions.MaxPrice = products.Max(x => x.Price);
            }
            filterOptions.Sizes = products.SelectMany(x => x.Sizes).Distinct().ToList();

            var allWords = products.SelectMany(x => x.Description.Replace(".", string.Empty).Split(" ", StringSplitOptions.RemoveEmptyEntries));
            var commonWords = (from word in allWords
                               group word by word into g
                               orderby g.Count() descending
                               select g).Skip(MostCommonWordsToSkip).Take(MostCommonWordsToTake).Select(x => x.Key).ToList();
            filterOptions.CommonWords = commonWords;
            return filterOptions;
        }

        private void HighlightDescriptionWords(List<Product> products, string highlight)
        {
            if (string.IsNullOrEmpty(highlight))
                return;

            var words = highlight.Split(",", StringSplitOptions.RemoveEmptyEntries);
            foreach (var product in products)
                foreach (var word in words)
                {
                    product.Description = Regex.Replace(product.Description, word, "<em>" + word + "</em>");
                }
        }
    }
}
