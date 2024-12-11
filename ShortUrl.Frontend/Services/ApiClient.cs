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

        public async Task DeleteShortUrl(string shortUrl)
        {
            var response = await _httpClient.DeleteAsync($"/{shortUrl}");
            if(response!.StatusCode == HttpStatusCode.NotFound)
            {
                return;
            }

            response.EnsureSuccessStatusCode();
        }

        public  async Task GenerateShortUrl(UrlDTO urlDTO)
        {
            var response = await _httpClient.PostAsJsonAsync("/shorten",urlDTO);
            
            if(response.StatusCode == HttpStatusCode.BadRequest)
            {
                return;
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task<ShowUrl> GetByShortAsync(string shortUrl)
        {
            var response = await _httpClient.GetAsync($"/{shortUrl}");
            if(response!.StatusCode == HttpStatusCode.NotFound)
            {
                return null!;
            }

            var contentReturn = await response.Content.ReadFromJsonAsync<ShowUrl>();
            return contentReturn!;
        }

        public async Task<IEnumerable<ShortenData>> GetShortenDatasAsync()
        {
            var response = await _httpClient!.GetAsync($"/lists");

            if(response.StatusCode == HttpStatusCode.NotFound)
            {
                return null!;
            }

            response.EnsureSuccessStatusCode();

            var contentReturn = await response!.Content!.ReadFromJsonAsync<IEnumerable<ShortenData>>();

            return contentReturn!;
        }

        public  Task<ShortenData> GetShortenStatsAsync(string shortUrl)
        {
            throw new NotImplementedException();
        }

        public Task UpdateShortUrl(string shortUrl, string updatedUrl)
        {
            throw new NotImplementedException();
        }
    }
}