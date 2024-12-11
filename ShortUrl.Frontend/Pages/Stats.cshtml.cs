using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ShortUrl.Backend.Models;
using ShortUrl.Frontend.Services;

namespace ShortUrl.Frontend.Pages
{
    public class Stats : PageModel
    {
        private readonly ILogger<Stats> _logger;
        private readonly IApiClient _apiClient;

        [BindProperty]
        public ShortenData? ShortenData {get;set;}

        public Stats(ILogger<Stats> logger, IApiClient apiClient)
        {
            _logger = logger;
            _apiClient = apiClient;
        }

        public async Task OnGet(string shortCode)
        {
            var sessions = await _apiClient.GetShortenStatsAsync(shortCode);

            ShortenData = sessions ;
        }
    }
}