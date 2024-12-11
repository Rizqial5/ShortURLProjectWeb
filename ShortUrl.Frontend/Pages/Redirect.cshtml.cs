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
    public class RedirectModel : PageModel
    {
        private readonly ILogger<RedirectModel> _logger;
        private readonly IApiClient _apiClient;

        [BindProperty] public ShowUrl ShowUrl{get;set;}
        [BindProperty(SupportsGet = true)] public string redirectString{get;set;}
        

        public RedirectModel(ILogger<RedirectModel> logger, IApiClient apiClient)
        {
            _logger = logger;
            _apiClient = apiClient;
        }

        public async Task<IActionResult> OnGet(string redirectString)
        {
            var sessions = await _apiClient.GetByShortAsync(redirectString);

            ShowUrl = sessions;

            

            

            return Redirect(ShowUrl.Url);
        }
    }
}