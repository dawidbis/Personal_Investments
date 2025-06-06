using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;

namespace Polygon_api
{
    public static class AlphaVantageService
    {
        private static readonly HttpClient _client = new HttpClient();
        private static readonly string _apiKey = "6A5KD56KS742XIUZ";

        public static async Task<List<AlphaVantage>> GetDailyCandlesAsync(string ticker)
        {
            var url = $"https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol={ticker}&interval=1min&outputsize=compact&apikey={_apiKey}";

            var response = await _client.GetStringAsync(url);

            var fullResponse = JsonConvert.DeserializeObject<AlphaVantageResponse>(response);

            if (fullResponse?.TimeSeriesDaily == null)
                return new List<AlphaVantage>();

            return MapToCandles(ticker, fullResponse);
        }

        private static List<AlphaVantage> MapToCandles(string ticker, AlphaVantageResponse response)
        {
            return response.TimeSeriesDaily.Select(kvp => new AlphaVantage
            {
                Ticker = ticker,
                Date = DateTime.Parse(kvp.Key),
                O = decimal.Parse(kvp.Value.Open, CultureInfo.InvariantCulture),
                H = decimal.Parse(kvp.Value.High, CultureInfo.InvariantCulture),
                L = decimal.Parse(kvp.Value.Low, CultureInfo.InvariantCulture),
                C = decimal.Parse(kvp.Value.Close, CultureInfo.InvariantCulture),
                V = long.Parse(kvp.Value.Volume, CultureInfo.InvariantCulture)
            }).ToList();
        }
        public static async Task<decimal?> GetLatestClosePriceAsync(string ticker)
        {
            try
            {
                var candles = await GetDailyCandlesAsync(ticker);
                var latest = candles.OrderByDescending(c => c.Date).FirstOrDefault();
                return latest?.C;
            }
            catch
            {
                return null;
            }
        }
    }
}
