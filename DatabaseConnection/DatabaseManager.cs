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
    public class DatabaseManager: DbContext
    {
        public string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PersonalInvestments;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
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
                        Token = string.Empty
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
    }
}
  