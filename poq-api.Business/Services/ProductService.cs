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

        private readonly IMockyService _mockyService;
        private readonly IAppLogger<ProductService> _logger;

        public ProductService(IMockyService client, IAppLogger<ProductService> logger)
        {
            _mockyService = client;
            _logger = logger;
        }

        public async Task<FilterResult> FilterProducts(int? maxprice, string size, string highlight)
        {
            _logger.LogInformation($"Search products...");
            return BuildResponse(await _mockyService.GetProducts(), maxprice, size, highlight);
        }

        private FilterResult BuildResponse(MockyResponse item, int? maxprice, string size, string highlights)
        {
            var result = new FilterResult();

            if (item != null && item.Products.Any())
            {
                result.FilterOptions = SetFilterOptions(item.Products);
                var products = FilterProducts(item.Products, maxprice, size);


                result.Products = products.Select(t => new Product()
                {
                    Title = t.Title,
                    Price = t.Price,
                    Sizes = t.Sizes,
                    Description = SetHighlight(t.Description, highlights)
                }).ToList();
            }

            return result;
        }

        private List<MockyProduct> FilterProducts(List<MockyProduct> products, int? maxprice, string size)
        {
            if (maxprice.HasValue)
                products = products.Where(x => x.Price <= maxprice.Value).ToList();

            if (!string.IsNullOrEmpty(size))
            {
                var sizes = size.Split(",", StringSplitOptions.RemoveEmptyEntries);
                products = products.Where(x => x.Sizes.Intersect(sizes, StringComparer.InvariantCultureIgnoreCase).Any()).ToList();
            }

            return products;
        }
       
        private static FilterOptions SetFilterOptions(List<MockyProduct> products)
        {
            return new FilterOptions()
            {
                MinPrice = products.Min(x => x.Price),
                MaxPrice = products.Max(x => x.Price),
                Sizes = products.SelectMany(x => x.Sizes).Distinct().ToList(),
                CommonWords = products.SelectMany(x => x.Description.Replace(".", string.Empty).Split(" ", StringSplitOptions.RemoveEmptyEntries))
                              .GroupBy(x => x).OrderByDescending(x => x.Count()).Skip(MostCommonWordsToSkip).Take(MostCommonWordsToTake).Select(x => x.Key)
                              .ToList()
            };
        }

        private string SetHighlight(string text, string highlights)
        {
            if (string.IsNullOrEmpty(highlights) || string.IsNullOrEmpty(text))
                return text;

            highlights.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(word =>
           {
               text = Regex.Replace(text, word, "<em>" + word + "</em>");
           });
            return text;
        }
    }
}
