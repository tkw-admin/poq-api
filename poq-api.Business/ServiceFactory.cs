using poq_api.Business.Products;

namespace poq_api.Business
{
    public static class ServiceFactory
    {
        public static IProductService CreateProductService(string url)
        {
            var productService = new ProductService(url, null, null);
            return productService;
        }
    }
}
