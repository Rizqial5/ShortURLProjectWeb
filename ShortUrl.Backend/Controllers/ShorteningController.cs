using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShortUrl.Backend.Models;

namespace ShortUrl.Backend.Controllers
{
    [Route("")]
    [ApiController]
    public class ShorteningController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public ShorteningController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var datas = await _context.ShortenDatas!.ToListAsync();

            return Ok(datas);
            
        }

        [HttpGet("{shortUrl}")]
        public async Task<ActionResult> GetByShort(string shortUrl)
        {
            var selectedData = await _context.ShortenDatas!.FirstOrDefaultAsync(u=> u.ShortCode == shortUrl);

            if(selectedData == null) return NotFound("Url not found");

            return Ok(selectedData);
        }

        [HttpPost("Shorten")]
        public async Task<IActionResult> GetShortUrl([FromBody] UrlDTO urlDTO)
        {
            if(urlDTO == null ) return BadRequest("Please input full URL");

            var urlData = new ShortenData
            {
                Url = urlDTO.UrlText,
                ShortCode = "Short",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
                
            };

            _context.ShortenDatas!.Add(urlData);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByShort), new {shortUrl = urlData.ShortCode}, urlData);
        }
    }

    public class UrlDTO
    {
        public string? UrlText {get;set;}
    }
}
