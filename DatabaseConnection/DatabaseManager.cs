using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Personal_Investment_App.DatabaseConnection;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using ProgramLogic;
using System;


namespace DatabaseConnection
{
    public class DatabaseManager : DbContext
    {
        public string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Personal;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public DbSet<User> Users { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<InvestmentType> InvestmentTypes { get; set; }
        public DbSet<InvestmentCategory> InvestmentCategories { get; set; }
        public DbSet<ReturnsHistory> ReturnsHistories { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportType> ReportTypes { get; set; }
        public DbSet<UserInvestment> UserInvestments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserInvestment>()
                .HasKey(ui => new { ui.UserId, ui.InvestmentId });

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
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.ConnectionString);
        }

        public bool DoesUserExist(string login, string email)
        {
            return this.Users.Any(u => u.Username == login && u.Email == email);
        }

        public void SaveResetCode(string login, string resetCode)
        {
            var user = this.Users.FirstOrDefault(u => u.Username == login);
            if (user != null)
            {
                user.ResetCode = resetCode;
                this.SaveChanges();
            }
        }

        public bool IsResetCodeValid(string login, string resetCode)
        {
            return this.Users.Any(u => u.Username == login && u.ResetCode == resetCode);
        }

        public void UpdatePassword(string login, string newPassword)
        {
            var user = this.Users.FirstOrDefault(u => u.Username == login);
            if (user != null)
            {
                user.PasswordHash = HashPassword(newPassword);
                user.ResetCode = null;
                this.SaveChanges();
            }
        }
        public bool UsunKonto(string username)
        {
            var user = this.Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                this.Users.Remove(user);
                this.SaveChanges();
                return true;
            }
            return false;
        }

        public LoginResult Login(string username, string password)
        {
            var result = new LoginResult();

            try
            {
                using (var context = new DatabaseManager())
                {
                    string hashedPassword = HashPassword(password);
                    var user = context.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == hashedPassword);

                    if (user != null)
                    {
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Invalid username or password.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = $"Error: {ex.Message}";
            }

            return result;
        }

        public RegisterResult Register(string username, string password, string email)
        {
            var result = new RegisterResult();

            try
            {
                using (var context = new DatabaseManager())
                {
                    bool userExists = context.Users.Any(u => u.Username == username || u.Email == email);

                    if (userExists)
                    {
                        result.Success = false;
                        result.Code = ErrorCode.UserAlreadyExists;
                        result.ErrorMessage = "Użytkownik o tej nazwie lub e-mailu już istnieje.";
                        return result;
                    }

                    string hashedPassword = HashPassword(password);

                    var user = new User
                    {
                        Username = username,
                        PasswordHash = hashedPassword,
                        Email = email,
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true,
                        ResetCode = string.Empty
                    };

                    context.Users.Add(user);
                    context.SaveChanges();

                    result.Success = true;
                    result.Code = ErrorCode.None;
                }
            }
            catch (DbUpdateException)
            {
                result.Success = false;
                result.Code = ErrorCode.DbUnavailable;
                result.ErrorMessage = "Błąd połączenia z bazą danych.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Code = ErrorCode.UnknownError;
                result.ErrorMessage = "Nieoczekiwany błąd: " + ex.Message;
            }

            return result;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public Investment AddInvestment(Investment investment)
        {
            this.Investments.Add(investment);
            this.SaveChanges();
            return investment;
        }

        public InvestmentCategory GetStockInvestmentCategory()
        {
            return this.InvestmentCategories
                       .FirstOrDefault(t => t.Name == "Akcje");
        }       

        public InvestmentType GetOrCreateStockInvestmentType()
        {
            // Znajdź lub utwórz kategorię "Akcje"
            var akcjeCategory = this.InvestmentCategories
                .FirstOrDefault(c => c.Name == "Akcje");

            if (akcjeCategory == null)
            {
                akcjeCategory = new InvestmentCategory
                {
                    Name = "Akcje",
                    Description = "Inwestycje w akcje notowane na giełdzie"
                };
                this.InvestmentCategories.Add(akcjeCategory);
                this.SaveChanges();
            }

            // Znajdź lub utwórz typ inwestycji "Akcje"
            var stockType = this.InvestmentTypes
                .FirstOrDefault(t => t.Name == "Akcje");

            if (stockType == null)
            {
                stockType = new InvestmentType
                {
                    Name = "Akcje",
                    RiskLevel = "Średni", // Możesz dopasować wg logiki aplikacji
                    CategoryId = akcjeCategory.Id
                };

                this.InvestmentTypes.Add(stockType);
                this.SaveChanges();
            }

            return stockType;
        }

        public int? GetUserIdByUsername(string username)
        {
            var user = this.Users.FirstOrDefault(u => u.Username == username);
            return user?.Id;
        }

        public bool SellInvestment(int investmentId, decimal marketPrice)
        {
            var investment = this.Investments.FirstOrDefault(i => i.Id == investmentId);
            if (investment == null) return false;

            decimal returnValue = marketPrice;

            var returnHistory = new ReturnsHistory
            {
                InvestmentId = investment.Id,
                Date = DateTime.Now,
                Value = returnValue
            };

            this.ReturnsHistories.Add(returnHistory);
            this.Investments.Remove(investment);
            this.SaveChanges();

            return true;
        }

    }
}
  