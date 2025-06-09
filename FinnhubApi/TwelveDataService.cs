using Personal_Investment_App.FinnhubApi;
using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public static class TwelveDataService
{
    private const string ApiKey = "715f59ed3a304b3eb3ffd5424ce9d127";
    private const string BaseUrl = "https://api.twelvedata.com/time_series";
    private static readonly HttpClient _httpClient = new HttpClient();

    private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

    public static async Task<decimal?> GetTodayClosePriceAsync(string symbol)
    {
        var today = DateTime.UtcNow.Date;
        var url = $"{BaseUrl}?symbol={symbol}/USD&interval=1day&start_date={today:yyyy-MM-dd}&end_date={today.AddDays(1):yyyy-MM-dd}&apikey={ApiKey}";

        var response = await _httpClient.GetStringAsync(url);

        var data = JsonSerializer.Deserialize<TwelveDataSimpleResponse>(response, _jsonOptions);
        return ParseClose(data);
    }

    public static async Task<decimal?> GetHistoricalClosePriceAsync(string symbol, DateTime date)
    {
        var url = $"{BaseUrl}?symbol={symbol}/USD&interval=1day&start_date={date:yyyy-MM-dd}&end_date={date.AddDays(1):yyyy-MM-dd}&apikey={ApiKey}";

        var response = await _httpClient.GetStringAsync(url);

        // Debug: Zapisz JSON do pliku
        string debugPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"TwelveData_HIST_{DateTime.Now:yyyyMMdd_HHmmss}.json");
        await File.WriteAllTextAsync(debugPath, response);

        var data = JsonSerializer.Deserialize<TwelveDataSimpleResponse>(response, _jsonOptions);
        return ParseClose(data);
    }

    private static decimal? ParseClose(TwelveDataSimpleResponse data)
    {
        if (data?.Values == null || data.Values.Count == 0)
            return null;

        var first = data.Values[0];

        if (decimal.TryParse(first.Close, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var close))
            return close;

        return null;
    }
}
