using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Polygon_api
{
    public class AlphaVantageService
    {
        private readonly HttpClient _client;
        private readonly string _apiKey = "JP2T1FCU998BJUCK";

        public AlphaVantageService()
        {
            _client = new HttpClient();
        }

        public async Task<List<AlphaVantage>> GetDailyCandlesAsync(string ticker)
        {
            var url = $"https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol={ticker}&interval=1min&outputsize=compact&apikey={_apiKey}";
            var response = await _client.GetStringAsync(url);

            var fullResponse = JsonConvert.DeserializeObject<AlphaVantageResponse>(response);

            if (fullResponse?.TimeSeriesDaily == null)
                return new List<AlphaVantage>();

            return MapToCandles(ticker, fullResponse);
        }

        private List<AlphaVantage> MapToCandles(string ticker, AlphaVantageResponse response)
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
    }
}
