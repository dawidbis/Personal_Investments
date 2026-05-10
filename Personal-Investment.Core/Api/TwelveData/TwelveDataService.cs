using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Personal_Investment.Core.Api.TwelveData;

// USUNIĘTO: static
public class TwelveDataService
{
    private readonly string _apiKey;
    private const string BaseUrl = "https://api.twelvedata.com/time_series";
    private readonly HttpClient _httpClient;

    private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

    // Konstruktor przyjmuje zależności z DI
    public TwelveDataService(IConfiguration config, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _apiKey = config["ApiKeys:TwelveData"]; // Klucz z appsettings.json
    }

    // USUNIĘTO: static
    public async Task<decimal?> GetTodayClosePriceAsync(string symbol)
    {
        var today = DateTime.UtcNow.Date;
        // Używamy _apiKey zamiast stałej
        var url = $"{BaseUrl}?symbol={symbol}/USD&interval=1day&start_date={today:yyyy-MM-dd}&end_date={today.AddDays(1):yyyy-MM-dd}&apikey={_apiKey}";

        var response = await _httpClient.GetStringAsync(url);

        var data = JsonSerializer.Deserialize<TwelveDataSimpleResponse>(response, _jsonOptions);
        return ParseClose(data);
    }

    // USUNIĘTO: static
    public async Task<decimal?> GetHistoricalClosePriceAsync(string symbol, DateTime date)
    {
        var url = $"{BaseUrl}?symbol={symbol}/USD&interval=1day&start_date={date:yyyy-MM-dd}&end_date={date.AddDays(1):yyyy-MM-dd}&apikey={_apiKey}";

        var response = await _httpClient.GetStringAsync(url);

        // Debug można zostawić lub przenieść do loggera
        var data = JsonSerializer.Deserialize<TwelveDataSimpleResponse>(response, _jsonOptions);
        return ParseClose(data);
    }

    // Metoda pomocnicza też już nie jest static
    private decimal? ParseClose(TwelveDataSimpleResponse data)
    {
        if (data?.Values == null || data.Values.Count == 0)
            return null;

        var first = data.Values[0];

        if (decimal.TryParse(first.Close, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var close))
            return close;

        return null;
    }
}