
using ShortUrl.Backend.Models;

namespace ShortUrl.Frontend.Services
{
    public interface IApiClient
    {
        Task<IEnumerable<ShortenData>> GetShortenDatasAsync();
        Task<ShowUrl> GetByShortAsync(string shortUrl);
        Task<ShortenData> GetShortenStatsAsync(string shortUrl);
        Task GenerateShortUrl(UrlDTO urlDTO);
        Task DeleteShortUrl(string shortUrl);
        Task UpdateShortUrl(string shortUrl, string updatedUrl);
    }
}