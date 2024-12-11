using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using ShortUrl.Backend.Models;
using System.Text;
using Base62;

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

        [HttpGet("lists")]
        public async Task<ActionResult> GetAllAsync()
        {
            var datas = await _context.ShortenDatas!.ToListAsync();

            return Ok(datas);
            
        }

        [HttpGet("{shortUrl}")]
        public async Task<ActionResult> GetByShort(string shortUrl)
        {
            var selectedData = await _context.ShortenDatas!.FirstOrDefaultAsync(u => u.ShortCode == shortUrl);

            if (selectedData == null) return NotFound("Url not found");
            ShowUrl showUrl = CreateShowUrlData(selectedData);

            selectedData.AccessCount += 1;

            await _context.SaveChangesAsync();

            return Ok(showUrl);
        }


        [HttpGet("{shortUrl}/stats")]
        public async Task<ActionResult> GetShortStats(string shortUrl)
        {
            var selectedData = await _context.ShortenDatas!.FirstOrDefaultAsync(u=> u.ShortCode == shortUrl);

            if(selectedData == null) return NotFound("Url not found");

            return Ok(selectedData);
        }

        [HttpPost("shorten")]
        public async Task<IActionResult> GetShortUrl([FromBody] UrlDTO urlDTO)
        {

            if(!ValidateUrl(urlDTO)) return BadRequest("Please input valid URL");


            var shortUrl = ShorteningAlgorithm(urlDTO.UrlText!);

            var urlData = new ShortenData
            {
                Url = urlDTO.UrlText,
                ShortCode = shortUrl,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                AccessCount = 0

            };

            _context.ShortenDatas!.Add(urlData);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByShort), new { shortUrl = urlData.ShortCode }, urlData);
        }

        private static bool ValidateUrl(UrlDTO urlDTO)
        {
            var success = Uri.IsWellFormedUriString(urlDTO.UrlText, UriKind.Absolute);

            return success;
        }

        private string ShorteningAlgorithm(string urlInput)
        {
            MD5 md5Hasher = MD5.Create();

            byte[] urlHash = md5Hasher.ComputeHash(Encoding.Default.GetBytes(urlInput));

            StringBuilder stringBuilder = new StringBuilder();
            var base62Converter = new Base62Converter();

            for (var i = 0; i <= 2; i++)
            {
                stringBuilder.Append(urlHash[i].ToString("x2"));
            }

            var urlDecimal = Int64.Parse(stringBuilder.ToString(), System.Globalization.NumberStyles.HexNumber);

            var encodeUrl = base62Converter.Encode(urlDecimal.ToString());

            var shortUrl = new StringBuilder();

            for (var i = 0; i <= 6; i++)
            {
                shortUrl.Append(encodeUrl[i]);
            }

            return shortUrl.ToString();
        }

        [HttpDelete("{shortUrl}")]
        public async Task<IActionResult> DeleteAsync(string shortUrl)
        {
            var selectedData = await _context.ShortenDatas!.FirstOrDefaultAsync(u=> u.ShortCode == shortUrl);

            if(selectedData == null) return NotFound();

            _context.ShortenDatas!.Remove(selectedData);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{shortUrl}")]
        public async Task<IActionResult> UpdateShortUrl(string shortUrl, [FromBody] UrlDTO inputUrl)
        {

            if(!ValidateUrl(inputUrl)) return BadRequest("Please input valid Url");

            var selectedData = await _context.ShortenDatas!.FirstOrDefaultAsync(u=> u.ShortCode == shortUrl);

            if(selectedData == null) return NotFound();

            selectedData.Url = inputUrl.UrlText;
            selectedData.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            var showUrl = CreateShowUrlData(selectedData);

            

            return Ok(showUrl);
        }

        private static ShowUrl CreateShowUrlData(ShortenData? selectedData)
        {
            return new ShowUrl
            {
                Id = selectedData.Id,
                Url = selectedData.Url,
                ShortCode = selectedData.ShortCode,
                CreatedAt = selectedData.CreatedAt,
                UpdatedAt = selectedData.UpdatedAt

            };
        }
    }
}
