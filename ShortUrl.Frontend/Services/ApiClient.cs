using ShortUrl.Backend.Models;

namespace ShortUrl.Frontend.Services
{
    public class ApiClient : IApiClient
    {
        public Task DeleteShortUrl(string shortUrl)
        {
            throw new NotImplementedException();
        }

        public Task GenerateShortUrl(UrlDTO urlDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ShowUrl> GetByShortAsync(string shortUrl)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ShortenData>> GetShortenDatasAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ShortenData> GetShortenStatsAsync(string shortUrl)
        {
            throw new NotImplementedException();
        }

        public Task UpdateShortUrl(string shortUrl, string updatedUrl)
        {
            throw new NotImplementedException();
        }
    }
}