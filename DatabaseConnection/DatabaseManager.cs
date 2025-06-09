using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Personal_Investment_App.DatabaseConnection;
using Personal_Investment_App.FinnhubApi;
using Polygon_api;
using ProgramLogic;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;


namespace DatabaseConnection
{
    public class DatabaseManager : DbContext
    {
        public string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Personalia;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
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
            modelBuilder.Entity<ReturnsHistory>()
                .HasOne(r => r.Investment)
                .WithMany(i => i.ReturnsHistories)
                .HasForeignKey(r => r.InvestmentId)
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
                    CategoryId = akcjeCategory.Id
                };

                this.InvestmentTypes.Add(stockType);
                this.SaveChanges();
            }

            return stockType;
        }

        public InvestmentType GetOrCreateCryptoInvestmentType()
        {
            // Znajdź lub utwórz kategorię "Kryptowaluty"
            var cryptoCategory = this.InvestmentCategories
                .FirstOrDefault(c => c.Name == "Kryptowaluty");

            if (cryptoCategory == null)
            {
                cryptoCategory = new InvestmentCategory
                {
                    Name = "Kryptowaluty",
                    Description = "Inwestycje w aktywa cyfrowe, takie jak Bitcoin, Ethereum itp."
                };
                this.InvestmentCategories.Add(cryptoCategory);
                this.SaveChanges();
            }

            // Znajdź lub utwórz typ inwestycji "Kryptowaluty"
            var cryptoType = this.InvestmentTypes
                .FirstOrDefault(t => t.Name == "Kryptowaluty");

            if (cryptoType == null)
            {
                cryptoType = new InvestmentType
                {
                    Name = "Kryptowaluty",
                    CategoryId = cryptoCategory.Id
                };

                this.InvestmentTypes.Add(cryptoType);
                this.SaveChanges();
            }

            return cryptoType;
        }

        public int? GetUserIdByUsername(string username)
        {
            var user = this.Users.FirstOrDefault(u => u.Username == username);
            return user?.Id;
        }

        public bool SellInvestment(int investmentId, decimal marketPrice)
        {
            var investment = this.Investments.FirstOrDefault(i => i.Id == investmentId);
            if (investment == null || investment.IsSold)
                return false;

            var returnHistory = new ReturnsHistory
            {
                InvestmentId = investment.Id,
                Date = DateTime.Now,
                Value = marketPrice
            };

            this.ReturnsHistories.Add(returnHistory);

            investment.IsSold = true; // ✅ oznacz jako sprzedaną
            this.SaveChanges();       // ✅ zapis wszystkiego razem

            return true;
        }
        public async Task<List<string>> CheckInvestmentsAutomaticallyAsync(
    int userId,
    bool useMockOnFail = false,
    Dictionary<string, decimal>? testPricesFromUI = null)
        {
            var alerts = new List<string>();

            var investments = this.Investments
                .Include(i => i.Type).ThenInclude(t => t.Category)
                .Include(i => i.ReturnsHistories)
                .Where(i => i.UserId == userId && !i.IsSold)
                .ToList();

            foreach (var investment in investments)
            {
                if (string.IsNullOrWhiteSpace(investment.Name))
                {
                    alerts.Add($"[BŁĄD] Inwestycja bez symbolu (ID: {investment.Id}) nie może być przetworzona.");
                    continue;
                }

                try
                {
                    decimal? currentPrice = null;

                    if (useMockOnFail && testPricesFromUI != null &&
                        testPricesFromUI.TryGetValue(investment.Name, out decimal testPriceFromUI))
                    {
                        currentPrice = testPriceFromUI;
                        //alerts.Add($"[INFO] Użyto ceny testowej z UI dla {investment.Name}: {currentPrice.Value:F2}");
                    }
                    else
                    {

                        if (investment.Type?.Name == "Akcje")
                        {
                            currentPrice = await FinnhubService.GetCurrentQuoteAsync(investment.Name);
                        }
                        else if (investment.Type?.Name == "Kryptowaluty")
                        {
                            currentPrice = await FinnhubService.GetCurrentCryptoQuoteAsync(investment.Name);
                        }
                    }

                    if (!currentPrice.HasValue)
                    {
                        //alerts.Add($"[BŁĄD] Nie udało się pobrać aktualnej ceny dla {investment.Name}.");
                        continue;
                    }

                    decimal? buyPrice = investment.BuyPrice;

                    if (!buyPrice.HasValue || buyPrice == 0)
                    {
                        //alerts.Add($"[BŁĄD] Inwestycja {investment.Name} nie ma poprawnej ceny zakupu.");
                        continue;
                    }

                    decimal change = (currentPrice.Value - buyPrice.Value) / buyPrice.Value;
                    decimal percentChange = change * 100;

                    // Log ogólny (do celów debugowania lub dziennika)
                    //alerts.Add($"[INFO] {investment.Name}: zmiana {percentChange:F2}% (kupiono za {buyPrice.Value:F2}, obecnie {currentPrice.Value:F2})");

                    // Sprawdzenie warunków sprzedaży
                    bool shouldSell = false;
                    string sellReason = "";

                    if (investment.ExpectedReturnPercent > 0 && change >= investment.ExpectedReturnPercent)
                    {
                        shouldSell = true;
                        decimal przekroczenie = (change - investment.ExpectedReturnPercent)*100;
                        sellReason = $"[SPRZEDAŻ] {investment.Name}: przekroczono oczekiwany zwrot o {przekroczenie:F2}%.";
                    }
                    else if (investment.StopLossPercent < 0 && change <= investment.StopLossPercent)
                    {
                        shouldSell = true;
                        decimal przekroczenie = (investment.StopLossPercent - change)*100;
                        sellReason = $"[SPRZEDAŻ] {investment.Name}: przekroczono limit straty o {przekroczenie:F2}%.";
                    }

                    if (shouldSell)
                    {
                        investment.IsSold = true;

                        investment.ReturnsHistories.Add(new ReturnsHistory
                        {
                            Date = DateTime.Now,
                            Value = currentPrice.Value
                        });

                        alerts.Add(sellReason);
                    }
                }
                catch (Exception ex)
                {
                    alerts.Add($"[BŁĄD] {investment.Name}: {ex.Message}");
                }
            }

            await this.SaveChangesAsync();
            return alerts;
        }






    }
}
  