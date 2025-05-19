using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygon_api
{
    public class dbManager : DbContext
    {
        public string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AlphaVantage;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public DbSet<AlphaVantage> AlphaVantageCandles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlphaVantage>()
                .HasIndex(c => new { c.Ticker, c.Date })  // przydatny indeks
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.ConnectionString);
        }
    }
}
