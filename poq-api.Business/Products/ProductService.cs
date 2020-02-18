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

            if (minprice.HasValue)
                products = products.Where(x => x.Price >= minprice.Value).ToList();
            if (maxprice.HasValue)
                products = products.Where(x => x.Price <= maxprice.Value).ToList();
            if (!string.IsNullOrEmpty(size))
            {
                var sizes = size.Split(",", StringSplitOptions.RemoveEmptyEntries);
                products = products.Where(x => x.Sizes.Intersect(sizes).Any()).ToList();
            }
            HighlightDescriptionWords(products, highlight);

            result.Products = products;
            return result;
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
