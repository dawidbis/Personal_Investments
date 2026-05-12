using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Personal_Investment.Core.Models;
using System.Collections.Generic;

namespace Personal_Investment.Data.DatabaseConnection;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();

        string sql = @"
        CREATE TABLE IF NOT EXISTS ""WatchlistItems"" (
            ""Id"" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
            ""Ticker"" TEXT NOT NULL,
            ""LastPrice"" TEXT NOT NULL DEFAULT '0'
        );";

        Database.ExecuteSqlRaw(sql);
    }

    // Tylko definicje tabel
    public DbSet<User> Users => Set<User>();
    public DbSet<Investment> Investments => Set<Investment>();
    public DbSet<InvestmentType> InvestmentTypes => Set<InvestmentType>();
    public DbSet<InvestmentCategory> InvestmentCategories => Set<InvestmentCategory>();
    public DbSet<ReturnsHistory> ReturnsHistories => Set<ReturnsHistory>();
    public DbSet<Report> Reports => Set<Report>();
    public DbSet<ReportType> ReportTypes => Set<ReportType>();
    public DbSet<UserInvestment> UserInvestments => Set<UserInvestment>();
    public DbSet<WatchlistItem> WatchlistItems => Set<WatchlistItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Klucz kompozytowy
        modelBuilder.Entity<UserInvestment>()
            .HasKey(ui => new { ui.UserId, ui.InvestmentId });

        // Relacje (Fluent API)
        modelBuilder.Entity<UserInvestment>()
            .HasOne(ui => ui.User)
            .WithMany(u => u.UserInvestments)
            .HasForeignKey(ui => ui.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserInvestment>()
            .HasOne(ui => ui.Investment)
            .WithMany(i => i.UserInvestments)
            .HasForeignKey(ui => ui.InvestmentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Dodaj indeksy dla wydajności - rekruterzy to uwielbiają!
        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
    }
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite("Data Source=investments.db");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}