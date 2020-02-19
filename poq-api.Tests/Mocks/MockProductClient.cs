using Newtonsoft.Json;
using poq_api.Business;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace poq_api.Tests.Mocks
{
    public class MockProductClient : IMockyService
    {
        public async Task<MockyResponse> GetProducts()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceStream = assembly.GetManifestResourceStream("poq-api.Tests.Mocks.products.json");
            var productResultJson = string.Empty;
            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                productResultJson = await reader.ReadToEndAsync();
            }

            var result = JsonConvert.DeserializeObject<MockyResponse>(productResultJson);
            return result;
        }
    }
}
