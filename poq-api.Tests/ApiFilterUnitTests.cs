using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using poq_api;
using poq_api.Business.Products;
using poq_api.Tests.Mocks;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Tests
{
    public class ApiFilterUnitTests
    {
        private HttpClient Client { get; }

        public ApiFilterUnitTests()
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

            var server = new TestServer(webhostBuilder);
            Client = server.CreateClient();
        }

        [Test]
        public async Task CheckEndpointApiFilter()
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/filter");

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            var message = response.EnsureSuccessStatusCode();
            Assert.AreEqual(true, message.IsSuccessStatusCode);
        }

        [Test]
        public async Task CheckProductsCount()
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/filter");

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FilterResult>(json);
            Assert.AreEqual(48, result.Products.Count);
        }

        [Test]
        public async Task CheckMaxPrice()
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/filter");

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FilterResult>(json);
            Assert.AreEqual(25, result.FilterOptions.MaxPrice);
        }

        [Test]
        public async Task CheckMinPrice()
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/filter");

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FilterResult>(json);
            Assert.AreEqual(10, result.FilterOptions.MinPrice);
        }

        [Test]
        public async Task CheckSizes()
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/filter");

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FilterResult>(json);
            var expectedSizes = new List<string> { "small", "medium", "large" };
            Assert.AreEqual(expectedSizes, result.FilterOptions.Sizes);
        }

        [Test]
        public async Task CheckCommonWords()
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/filter");

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FilterResult>(json);
            var expectedCommonWords = new List<string> { "shirt", "hat", "trouser", "green", "blue", "red", "belt", "bag", "shoe", "tie" };
            Assert.AreEqual(expectedCommonWords, result.FilterOptions.CommonWords);
        }
    }
}