using Microsoft.EntityFrameworkCore;
using PgccApi.Entities;

namespace PgccApi.Models
{
    public class PgccContext : DbContext
    {
        public PgccContext(DbContextOptions<PgccContext> options)
            : base(options)
        {
        }

        public DbSet<NewsItem> NewsItems { get; set; }
        public DbSet<Rink> Rinks { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Competition> Competitions { get; set; }
    }
}