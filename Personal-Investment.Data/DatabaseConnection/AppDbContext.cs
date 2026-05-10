using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Personal_Investment.Core.Models;

namespace Personal_Investment.Data.DatabaseConnection;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
    {
        Database.EnsureCreated();
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
}