using Microsoft.EntityFrameworkCore;
using TimeCalculator.Domain.Entities;

namespace TimeCalculator.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<TimeEntry> TimeEntries { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=TimeCalculator.db");
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TimeEntry>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Date).IsRequired();
            entity.Property(e => e.MorningStart).IsRequired();
            entity.Property(e => e.MorningEnd).IsRequired();
            entity.Property(e => e.AfternoonStart).IsRequired();
            entity.Property(e => e.AfternoonEnd).IsRequired();
            entity.Property(e => e.MinimumLunchBreak).IsRequired();

            // Index sur la date pour les recherches rapides
            entity.HasIndex(e => e.Date).IsUnique();
        });
    }
}