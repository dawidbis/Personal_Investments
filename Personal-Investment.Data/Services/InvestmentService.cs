using Microsoft.EntityFrameworkCore;
using Personal_Investment.Core.Models;
using Personal_Investment.Data.DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Personal_Investment.Core.Enums;
using Personal_Investment.Core.Api.Finnhub;
using Personal_Investment.Core.Api.Polygon;
using Personal_Investment.Core.Api.TwelveData;

namespace Personal_Investment.Data.Services;

public class InvestmentService
{
    private readonly AppDbContext _context;
    private readonly FinnhubService _finnhub;
    private readonly PolygonService _polygon;
    private readonly TwelveDataService _twelveData;

    public InvestmentService(
        AppDbContext context,
        FinnhubService finnhub,
        PolygonService polygon,
        TwelveDataService twelveData)
    {
        _context = context;
        _finnhub = finnhub;
        _polygon = polygon;
        _twelveData = twelveData;
    }

    // 1. Pobieranie aktywnych inwestycji
    public async Task<List<Investment>> GetActiveInvestmentsAsync(int userId)
    {
        return await _context.Investments
            .Include(i => i.Type)
                .ThenInclude(t => t.Category)
            .Where(i => i.UserId == userId && !i.IsSold)
            .ToListAsync();
    }

    // 2. Pobieranie sprzedanych inwestycji (Historia)
    public async Task<List<Investment>> GetSoldInvestmentsAsync(int userId)
    {
        return await _context.Investments
            .Include(i => i.Type)
                .ThenInclude(t => t.Category)
            .Include(i => i.ReturnsHistories)
            .Where(i => i.UserId == userId && i.IsSold)
            .OrderByDescending(i => i.DateOfInvestment)
            .ToListAsync();
    }

    // 3. Automatyczne sprawdzanie cen i alerty sprzedaży
    public async Task<List<string>> RunAutomaticCheckAsync(int userId)
    {
        var alerts = new List<string>();
        var investments = await GetActiveInvestmentsAsync(userId);

        foreach (var inv in investments)
        {
            // ZMIANA: Używamy wstrzykniętych serwisów zamiast statycznych metod
            decimal? currentPrice = inv.Type?.Name switch
            {
                "Akcje" => await _finnhub.GetCurrentQuoteAsync(inv.Name),
                "Kryptowaluty" => await _finnhub.GetCurrentCryptoQuoteAsync(inv.Name),
                "Surowce" => await _twelveData.GetTodayClosePriceAsync(inv.Name),
                _ => null
            };

            if (currentPrice.HasValue && ShouldSell(inv, currentPrice.Value, out string reason))
            {
                inv.IsSold = true;
                inv.ReturnsHistories.Add(new ReturnsHistory { Date = DateTime.Now, Value = currentPrice.Value });
                alerts.Add(reason);
            }
        }

        await _context.SaveChangesAsync();
        return alerts;
    }

    private bool ShouldSell(Investment inv, decimal currentPrice, out string reason)
    {
        reason = string.Empty;
        if (inv.BuyPrice == 0) return false;

        decimal change = (currentPrice - inv.BuyPrice) / inv.BuyPrice;

        if (inv.ExpectedReturnPercent > 0 && change >= inv.ExpectedReturnPercent)
        {
            reason = $"[PROFIT] {inv.Name}: Osiągnięto cel ({change:P})";
            return true;
        }

        if (inv.StopLossPercent < 0 && change <= inv.StopLossPercent)
        {
            reason = $"[STOPLOSS] {inv.Name}: Przekroczono limit straty ({change:P})";
            return true;
        }

        return false;
    }

    // 4. Podsumowanie portfela
    public async Task<AccountSummary> GetAccountSummaryAsync(int userId, Dictionary<int, decimal>? pricesFromUI = null)
    {
        var summary = new AccountSummary();
        decimal totalInvestedCost = 0;
        decimal totalCurrentValue = 0;
        decimal totalProfitFromSold = 0;

        var investments = await _context.Investments
            .Include(i => i.ReturnsHistories)
            .Where(i => i.UserId == userId)
            .ToListAsync();

        foreach (var inv in investments)
        {
            decimal cost = inv.BuyPrice * inv.NumberOfShares;

            if (!inv.IsSold)
            {
                totalInvestedCost += cost;

                // ZMIANA: Używamy _finnhub zamiast FinnhubService
                decimal? currentPrice = (pricesFromUI != null && pricesFromUI.TryGetValue(inv.Id, out decimal p))
                                         ? p : await _finnhub.GetCurrentQuoteAsync(inv.Name);

                decimal actualPriceToCalculate = currentPrice ?? inv.BuyPrice;
                totalCurrentValue += actualPriceToCalculate * inv.NumberOfShares;
            }
            else
            {
                var sale = inv.ReturnsHistories.OrderByDescending(r => r.Date).FirstOrDefault();
                if (sale != null)
                {
                    totalProfitFromSold += (sale.Value * inv.NumberOfShares) - cost;
                }
            }
        }

        summary.CurrentBalance = totalCurrentValue - totalInvestedCost;
        summary.TotalChange = summary.CurrentBalance + totalProfitFromSold;

        return summary;
    }

    // 5. Logika Sprzedaży manualnej
    public async Task<bool> SellInvestmentAsync(int investmentId)
    {
        var inv = await _context.Investments.Include(i => i.Type).FirstOrDefaultAsync(i => i.Id == investmentId);
        if (inv == null || inv.IsSold) return false;

        // ZMIANA: Używamy wstrzykniętych serwisów
        decimal? price = inv.Type?.Name switch
        {
            "Akcje" => await _finnhub.GetCurrentQuoteAsync(inv.Name),
            "Kryptowaluty" => await _finnhub.GetCurrentCryptoQuoteAsync(inv.Name),
            "Surowce" => await _twelveData.GetTodayClosePriceAsync(inv.Name),
            _ => null
        };

        if (!price.HasValue) return false;

        inv.IsSold = true;
        inv.ReturnsHistories.Add(new ReturnsHistory { Date = DateTime.Now, Value = price.Value });

        await _context.SaveChangesAsync();
        return true;
    }

    // 6. Generowanie raportu
    public async Task<InvestmentReport> GenerateReportAsync(int userId, DateTime from, DateTime to, string ticker = null)
    {
        var query = _context.Investments
            .Include(i => i.ReturnsHistories)
            .Where(inv => inv.UserId == userId && inv.IsSold)
            .Where(inv => inv.DateOfInvestment >= from && inv.DateOfInvestment <= to);

        if (!string.IsNullOrEmpty(ticker))
            query = query.Where(inv => inv.Name.ToUpper() == ticker.ToUpper());

        var investments = await query.ToListAsync();

        decimal invested = investments.Sum(inv => inv.BuyPrice * inv.NumberOfShares);
        decimal earned = investments.Sum(inv => {
            var sale = inv.ReturnsHistories.OrderByDescending(r => r.Date).FirstOrDefault();
            return (sale?.Value ?? inv.BuyPrice) * inv.NumberOfShares;
        });

        return new InvestmentReport
        {
            TotalInvested = invested,
            TotalEarned = earned,
            Profit = earned - invested,
            InvestmentCount = investments.Count
        };
    }

    public async Task<Investment> AddInvestmentAsync(Investment inv, InvestmentKind kind)
    {
        var typeName = kind switch
        {
            InvestmentKind.Akcja => "Akcje",
            InvestmentKind.Kryptowaluta => "Kryptowaluty",
            InvestmentKind.Surowiec => "Surowce",
            _ => "Inne"
        };

        var type = await _context.InvestmentTypes
            .Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.Name == typeName);

        if (type == null)
        {
            var category = await _context.InvestmentCategories
                .FirstOrDefaultAsync(c => c.Name == typeName)
                ?? new InvestmentCategory { Name = typeName, Description = "Automatyczna" };

            type = new InvestmentType { Name = typeName, Category = category };
            _context.InvestmentTypes.Add(type);
            await _context.SaveChangesAsync();
        }

        inv.TypeId = type.Id;
        _context.Investments.Add(inv);
        await _context.SaveChangesAsync();

        return inv;
    }

    public async Task<decimal?> GetLatestPriceAsync(string symbol, string typeName)
    {
        // ZMIANA: Używamy wstrzykniętych serwisów
        return typeName switch
        {
            "Akcje" => await _finnhub.GetCurrentQuoteAsync(symbol),
            "Kryptowaluta" => await _finnhub.GetCurrentCryptoQuoteAsync(symbol),
            "Surowce" => await _twelveData.GetTodayClosePriceAsync(symbol),
            _ => await _finnhub.GetCurrentQuoteAsync(symbol)
        };
    }
}