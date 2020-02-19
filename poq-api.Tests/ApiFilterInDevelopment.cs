using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using poq_api.Business.Products;
using poq_api.Tests.Mocks;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace poq_api.Tests
{
    public class ApiFilterInDevelopmentUnitTests
    {
        private HttpClient Client { get; }

        public ApiFilterInDevelopmentUnitTests()
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.json");

            var webhostBuilder = new WebHostBuilder();
            webhostBuilder.ConfigureAppConfiguration((context, conf) =>
            {
                conf.AddJsonFile(configPath);
            });
            webhostBuilder.UseStartup<Startup>();
            webhostBuilder.ConfigureTestServices(config =>
            {
                config.AddSingleton<IProductClient>(new MockProductClient());
            });
            webhostBuilder.UseEnvironment("Development");

            var server = new TestServer(webhostBuilder);
            Client = server.CreateClient();
        }

        [Test]
        public async Task CheckDevelopmentEndpointApiFilter()
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/filter");

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            var message = response.EnsureSuccessStatusCode();
            Assert.AreEqual(true, message.IsSuccessStatusCode);
        }

    }
}
