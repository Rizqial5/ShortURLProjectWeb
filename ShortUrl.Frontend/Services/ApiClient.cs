using System.Net;
using ShortUrl.Backend.Models;

namespace ShortUrl.Frontend.Services
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task DeleteShortUrl(string shortUrl)
        {
            throw new NotImplementedException();
        }

        public Task GenerateShortUrl(UrlDTO urlDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<ShowUrl> GetByShortAsync(string shortUrl)
        {
            var response = await _httpClient.GetAsync($"/{shortUrl}");
            if(response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<ShowUrl>();
        }

        public async Task<IEnumerable<ShortenData>> GetShortenDatasAsync()
        {
            var response = await _httpClient.GetAsync($"/lists");

            if(response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<IEnumerable<ShortenData>>();
        }

        public async Task<ShortenData> GetShortenStatsAsync(string shortUrl)
        {
            throw new NotImplementedException();
        }

        public Task UpdateShortUrl(string shortUrl, string updatedUrl)
        {
            throw new NotImplementedException();
        }
    }
}