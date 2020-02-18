using poq_api.Business.Products;
using RestEase;

namespace poq_api.Business
{
    public static class ServiceFactory
    {
        public static IProductService CreateProductService(IProductClient client)
        {
            var productService = new ProductService(client);
            return productService;
        }

        public static IProductClient CreateProductClient(string url, string username, string password)
        {
            var apiClient = RestClient.For<IProductClient>(url);
            //var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
            //apiClient.Authorization = new AuthenticationHeaderValue("Basic", credentials);
            return apiClient;
        }
    }
}
