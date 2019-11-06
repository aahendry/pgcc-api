using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PgccApi.Entities;
using System;

namespace PgccApi.Models
{
    public class PgccContext : DbContext
    {
        public PgccContext(DbContextOptions<PgccContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<NewsItem> NewsItems { get; set; }
        public DbSet<Rink> Rinks { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Fixture> Fixtures { get; set; }
        public DbSet<Enquiry> Enquiries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(bool))
                    {
                        property.SetValueConverter(new BoolToIntConverter());
                    }
                }
            }
        }
    }

    public class BoolToIntConverter : ValueConverter<bool, int>
    {
        public BoolToIntConverter(ConverterMappingHints mappingHints = null)
            : base(
                  v => Convert.ToInt32(v),
                  v => Convert.ToBoolean(v),
                  mappingHints)
        {
        }

        public static ValueConverterInfo DefaultInfo { get; }
            = new ValueConverterInfo(typeof(bool), typeof(int), i => new BoolToIntConverter(i.MappingHints));
    }
}