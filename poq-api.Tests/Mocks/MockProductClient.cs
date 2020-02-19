using Newtonsoft.Json;
using poq_api.Business;
using System.IO;
using System.Threading.Tasks;

namespace poq_api.Tests.Mocks
{
    public class MockProductClient : IMockyService
    {
        public async Task<MockyResponse> GetProducts()
        {
            var projectDir = Directory.GetCurrentDirectory();
            var jsonPath = Path.Combine(projectDir, "Mocks/products.json");

            var productResultJson = await File.ReadAllTextAsync(jsonPath);
            var result = JsonConvert.DeserializeObject<MockyResponse>(productResultJson);
            return result;
        }
    }
}
