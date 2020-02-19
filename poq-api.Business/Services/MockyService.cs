using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace poq_api.Business
{
    public class MockyService : IMockyService
    {
        private static readonly TimeSpan _defaultCacheDuration = TimeSpan.FromSeconds(30);
        private static readonly string _mockCacheKey = "mockService";

        private readonly HttpClient _httpClient;
        private readonly IAppLogger<MockyService> _logger;
        private readonly IMemoryCache _cache;

        public MockyService(HttpClient httpClient, IMemoryCache cache, IAppLogger<MockyService> logger)
        {
            _httpClient = httpClient;
            _cache = cache;
            _logger = logger;
        }

        public async Task<MockyResponse> GetProducts()
        {
            return await _cache.GetOrCreateAsync(_mockCacheKey, async entry =>
            {
                entry.SlidingExpiration = _defaultCacheDuration;
                var response = await _httpClient.GetAsync("");
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsAsync<MockyResponse>();
                _logger.LogInformation($"Mock.io get data: {JsonConvert.SerializeObject(result)}");
                return result;
            });
        }
    }
}
