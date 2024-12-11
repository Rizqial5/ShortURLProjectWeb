using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ShortUrl.Frontend.Pages
{
    public class Update : PageModel
    {
        private readonly ILogger<Update> _logger;

        public Update(ILogger<Update> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}