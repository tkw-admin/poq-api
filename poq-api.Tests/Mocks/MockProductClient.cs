using Newtonsoft.Json;
using poq_api.Business.Products;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace poq_api.Tests.Mocks
{
    public class MockProductClient : IProductClient
    {
        public async Task<ProductResult> GetProducts()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceStream = assembly.GetManifestResourceStream("poq-api.Tests.Mocks.products.json");
            var productResultJson = string.Empty;
            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                productResultJson = await reader.ReadToEndAsync();
            }

            var result = JsonConvert.DeserializeObject<ProductResult>(productResultJson);
            return result;
        }
    }
}
