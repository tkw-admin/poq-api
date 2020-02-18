using Newtonsoft.Json;
using poq_api.Business.Products;
using System.IO;
using System.Threading.Tasks;

namespace poq_api.Tests.Mocks
{
    public class MockProductClient : IProductClient
    {
        public async Task<ProductResult> GetProducts()
        {
            var projectDir = Directory.GetCurrentDirectory();
            var jsonPath = Path.Combine(projectDir, "Mocks/products.json");

            var productResultJson = await File.ReadAllTextAsync(jsonPath);
            var result = JsonConvert.DeserializeObject<ProductResult>(productResultJson);
            return result;
        }
    }
}
