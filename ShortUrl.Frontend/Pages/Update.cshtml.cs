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
    public class Update : PageModel
    {
        private readonly ILogger<Update> _logger;
        private readonly IApiClient? _apiClient;

        [BindProperty]
        public ShowUrl? SelectedUrl {get;set;}

        public Update(ILogger<Update> logger, IApiClient apiClient)
        {
            _logger = logger;
            _apiClient = apiClient;
        }

        public async Task OnGet(string shortCode)
        {
            var sessions = await _apiClient.GetByShortAsync(shortCode);

            SelectedUrl = sessions;
        }

        public async Task<IActionResult> OnPost( string inputUrl)
        {
        
            await _apiClient.UpdateShortUrl(SelectedUrl!.ShortCode!, inputUrl);

            return RedirectToPage("Index");
        }
    }
}