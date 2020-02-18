using RestEase;
using System.Collections.Generic;
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


        public async Task<List<Product>> GetProducts()
        {
            var productResult = await Api.GetProducts();
            return productResult.Products;
        }
    }
}
