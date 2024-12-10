using Microsoft.EntityFrameworkCore;

namespace ShortUrl.Backend.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ShortenData>? ShortenDatas{get;set;}
    }
}