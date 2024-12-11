using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShortUrl.Backend.Models;
using ShortUrl.Frontend.Services;

namespace ShortUrl.Frontend.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IApiClient _apiClient;

    [BindProperty] public IEnumerable<ShortenData>? ShortenDatas {set;get;}
    [BindProperty] public ShowUrl? ShowShortUrl {set;get;}

    public IndexModel(ILogger<IndexModel> logger, IApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public async Task OnGet()
    {
        var sessions = await _apiClient.GetShortenDatasAsync();

        
        ShortenDatas = sessions;
    }
}
