using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace poq_api.Business.Products
{
    public class ProductService : IProductService
    {
        private IProductClient Api { get; set; }

        public ProductService(string path, string username, string password)
        {
            CreateClientEndpoint(path, username, password);
        }

        private void CreateClientEndpoint(string path, string username, string password)
        {
            Api = RestClient.For<IProductClient>(path);
            //var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
            //Api.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        }

        public async Task<FilterResult> FilterProducts(int? minprice, int? maxprice, string size, string highlight)
        {
            var result = new FilterResult();

            var productResult = await Api.GetProducts();
            var products = productResult.Products;

            products = FilterProducts(products, minprice, maxprice, size);
            DetermineFilterOptions(products, result);
            HighlightDescriptionWords(products, highlight);
            result.Products = products;

            return result;
        }

        private static void DetermineFilterOptions(List<Product> products, FilterResult result)
        {
            result.FilterOptions = new FilterOptions();
            result.FilterOptions.MinPrice = products.Min(x => x.Price);
            result.FilterOptions.MaxPrice = products.Max(x => x.Price);
            result.FilterOptions.Sizes = products.SelectMany(x => x.Sizes).Distinct().ToList();

            var allWords = products.SelectMany(x => x.Description.Replace(".", string.Empty).Split(" ", StringSplitOptions.RemoveEmptyEntries));
            var commonWords = (from word in allWords
                               group word by word into g
                               orderby g.Count() descending
                               select g).Skip(5).Select(x => x.Key).ToList();
            result.FilterOptions.CommonWords = commonWords;
        }

        private static List<Product> FilterProducts(List<Product> products, int? minprice, int? maxprice, string size)
        {
            if (minprice.HasValue)
                products = products.Where(x => x.Price >= minprice.Value).ToList();
            if (maxprice.HasValue)
                products = products.Where(x => x.Price <= maxprice.Value).ToList();
            if (!string.IsNullOrEmpty(size))
            {
                var sizes = size.Split(",", StringSplitOptions.RemoveEmptyEntries);
                products = products.Where(x => x.Sizes.Intersect(sizes).Any()).ToList();
            }
            return products;
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
