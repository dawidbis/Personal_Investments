using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Personal_Investment.Data.DatabaseConnection;
using Personal_Investment.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Personal_Investment.Data.Services
{
    public class DataExportService
    {
        private readonly AppDbContext _context;

        public DataExportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ImportUserDataAsync(int userId, string filePath)
        {
            try
            {
                string json = await File.ReadAllTextAsync(filePath);
                using JsonDocument doc = JsonDocument.Parse(json);
                JsonElement root = doc.RootElement;

                // Importujemy obie sekcje: aktywne i sprzedane
                await ImportInvestmentsAsync(root, "ActiveInvestments", false, userId);
                await ImportInvestmentsAsync(root, "SoldInvestments", true, userId);

                return true;
            }
            catch (Exception)
            {
                // Tutaj można dodać logowanie błędów
                return false;
            }
        }

        public async Task ExportUserDataAsync(int userId, string filePath)
        {
            var investments = await _context.Investments
                .Include(i => i.Type).ThenInclude(t => t.Category)
                .Include(i => i.ReturnsHistories)
                .Where(i => i.UserId == userId)
                .ToListAsync();

            var exportData = new
            {
                ExportDate = DateTime.Now,
                UserId = userId,
                ActiveInvestments = investments.Where(i => !i.IsSold).Select(i => MapToExport(i)),
                SoldInvestments = investments.Where(i => i.IsSold).Select(i => MapToExport(i))
            };

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(exportData, options);
            await File.WriteAllTextAsync(filePath, json);
        }

        // Pomocnicza metoda do mapowania obiektu na format eksportu
        private object MapToExport(Investment i) => new
        {
            i.Name,
            Type = i.Type?.Name,
            Category = i.Type?.Category?.Name,
            i.NumberOfShares,
            i.BuyPrice,
            i.DateOfInvestment,
            i.ExpectedReturnPercent,
            i.StopLossPercent,
            ReturnsHistory = i.ReturnsHistories.Select(rh => new { rh.Date, rh.Value })
        };

        public async Task<bool> ImportInvestmentsAsync(JsonElement root, string propertyName, bool isSold, int userId)
        {
            if (!root.TryGetProperty(propertyName, out JsonElement investments))
                return false;

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                foreach (JsonElement inv in investments.EnumerateArray())
                {
                    string categoryName = inv.TryGetProperty("Category", out var cat) ? cat.GetString()! : "Inne";
                    string typeName = inv.TryGetProperty("Type", out var t) ? t.GetString()! : "Inne";

                    // Logika Get or Create dla Kategorii
                    var category = await _context.InvestmentCategories.FirstOrDefaultAsync(c => c.Name == categoryName)
                                   ?? new InvestmentCategory { Name = categoryName, Description = "Zaimportowano" };

                    if (_context.Entry(category).State == EntityState.Detached)
                        _context.InvestmentCategories.Add(category);

                    await _context.SaveChangesAsync();

                    // Logika Get or Create dla Typu
                    var type = await _context.InvestmentTypes.FirstOrDefaultAsync(tp => tp.Name == typeName && tp.CategoryId == category.Id)
                               ?? new InvestmentType { Name = typeName, CategoryId = category.Id };

                    if (_context.Entry(type).State == EntityState.Detached)
                        _context.InvestmentTypes.Add(type);

                    await _context.SaveChangesAsync();

                    // Tworzenie inwestycji
                    var investment = new Investment
                    {
                        Name = inv.GetProperty("Name").GetString(),
                        TypeId = type.Id,
                        UserId = userId,
                        NumberOfShares = inv.GetProperty("NumberOfShares").GetDecimal(),
                        DateOfInvestment = inv.GetProperty("DateOfInvestment").GetDateTime(),
                        ExpectedReturnPercent = inv.GetProperty("ExpectedReturnPercent").GetDecimal(),
                        StopLossPercent = inv.GetProperty("StopLossPercent").GetDecimal(),
                        BuyPrice = inv.GetProperty("BuyPrice").GetDecimal(),
                        IsSold = isSold
                    };

                    _context.Investments.Add(investment);
                    await _context.SaveChangesAsync();

                    // Import historii
                    if (inv.TryGetProperty("ReturnsHistory", out JsonElement history))
                    {
                        foreach (JsonElement h in history.EnumerateArray())
                        {
                            _context.ReturnsHistories.Add(new ReturnsHistory
                            {
                                InvestmentId = investment.Id,
                                Date = h.GetProperty("Date").GetDateTime(),
                                Value = h.GetProperty("Value").GetDecimal()
                            });
                        }
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}