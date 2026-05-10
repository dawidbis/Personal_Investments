using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace Personal_Investment.Core.Api.Polygon;

// USUNIĘTO: static
public class PolygonService
{
    private readonly HttpClient _client;
    private readonly string _apiKey;

    // Konstruktor przyjmuje zależności
    public PolygonService(IConfiguration config, HttpClient client)
    {
        _client = client;
        _apiKey = config["ApiKeys:Polygon"]; // Pobieranie klucza z appsettings.json
    }

    // Metoda pomocnicza może zostać static, jeśli nie używa pól klasy, 
    // ale dla spójności zrobimy ją instancyjną.
    public DateTime GetLastTradingDay(DateTime date)
    {
        while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
        {
            date = date.AddDays(-1);
        }
        return date;
    }

    // USUNIĘTO: static ze wszystkich metod API
    public async Task<decimal?> GetHistoricalClosePriceAsync(string ticker, DateTime date)
    {
        try
        {
            string formattedDate = date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string url = $"https://api.polygon.io/v1/open-close/{ticker}/{formattedDate}?adjusted=true&apiKey={_apiKey}";

            return await FetchAndParseAsync<PolygonDailyResponse>(url, r => r.status == "OK" ? r.close : null);
        }
        catch (Exception ex)
        {
            throw new Exception($"Błąd Polygon API (Stock): {ticker}", ex);
        }
    }

    public async Task<decimal?> GetHistoricalCryptoClosePriceAsync(string fromSymbol, DateTime date)
    {
        try
        {
            string formattedDate = date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string ticker = $"X:{fromSymbol.ToUpper()}USD";
            string url = $"https://api.polygon.io/v2/aggs/ticker/{ticker}/range/1/day/{formattedDate}/{formattedDate}?adjusted=true&apiKey={_apiKey}";

            return await FetchAndParseAsync<PolygonAggsResponse>(url, r =>
            {
                if (r.status == "OK" && r.results?.Count > 0)
                    return r.results[0].c;
                return null;
            });
        }
        catch (Exception ex)
        {
            throw new Exception($"Błąd Polygon API (Crypto): {fromSymbol}", ex);
        }
    }

    // Prywatna metoda generyczna - teraz korzysta z instancyjnego _client
    private async Task<decimal?> FetchAndParseAsync<T>(string url, Func<T, decimal?> selector)
    {
        var response = await _client.GetAsync(url);
        if (!response.IsSuccessStatusCode) return null;

        string json = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<T>(json);

        return data != null ? selector(data) : null;
    }
}