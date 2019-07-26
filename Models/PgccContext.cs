using Microsoft.EntityFrameworkCore;

namespace PgccApi.Models
{
    public class PgccContext : DbContext
    {
        public PgccContext(DbContextOptions<PgccContext> options)
            : base(options)
        {
        }

        public DbSet<NewsItem> NewsItems { get; set; }
    }
}