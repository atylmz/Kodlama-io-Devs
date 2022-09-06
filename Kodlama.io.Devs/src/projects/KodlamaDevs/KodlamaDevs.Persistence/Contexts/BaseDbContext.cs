using KodlamaDevs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodlamaDevs.Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }


        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgrammingLanguage>(a =>
            {
                a.ToTable("ProgrammingLanguages").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
                a.Property(p => p.IsActive).HasColumnName("IsActive");
                a.Property(p => p.CreatedDate).HasColumnName("CreatedDate");
                a.Property(p => p.ModifiedDate).HasColumnName("ModifiedDate");
            });

            ProgrammingLanguage[] pragrammingLanguageEntitySeed = { new(1, "C#", true, DateTime.Now, DateTime.Now),
                new(2, "Python", true, DateTime.Now, DateTime.Now) };
            modelBuilder.Entity<ProgrammingLanguage>().HasData(pragrammingLanguageEntitySeed);
        }
    }
}
