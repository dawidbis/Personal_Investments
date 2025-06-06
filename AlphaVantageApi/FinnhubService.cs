using Newtonsoft.Json;
using Personal_Investment_App.AlphaVantageApi;

namespace Polygon_api
{
    public static class FinnhubService
    {
        private static readonly HttpClient _client = new HttpClient();
        private static readonly string _apiKey = "d11k9ihr01qjtpe7e510d11k9ihr01qjtpe7e51g";

        public static async Task<List<AlphaVantage>> GetMinuteCandlesAsync(string ticker)
        {
            // Czas w Unix time (ostatnie 60 minut)
            var to = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var from = to - 60 * 60; // ostatnia godzina

            var url = $"https://finnhub.io/api/v1/stock/candle?symbol={ticker}&resolution=1&from={from}&to={to}&token={_apiKey}";

            var response = await _client.GetStringAsync(url);
            var candleResponse = JsonConvert.DeserializeObject<FinnhubCandleResponse>(response);

            if (candleResponse?.c == null || candleResponse.s != "ok")
                return new List<AlphaVantage>();

            return MapToCandles(ticker, candleResponse);
        }

        private static List<AlphaVantage> MapToCandles(string ticker, FinnhubCandleResponse response)
        {
            var candles = new List<AlphaVantage>();

            for (int i = 0; i < response.t.Length; i++)
            {
                candles.Add(new AlphaVantage
                {
                    Ticker = ticker,
                    Date = DateTimeOffset.FromUnixTimeSeconds(response.t[i]).UtcDateTime,
                    O = response.o[i],
                    H = response.h[i],
                    L = response.l[i],
                    C = response.c[i],
                    V = response.v[i]
                });
            }

            return candles;
        }

        public static async Task<decimal?> GetCurrentQuoteAsync(string symbol)
        {
            var url = $"https://finnhub.io/api/v1/quote?symbol={symbol}&token={_apiKey}";
            var json = await _client.GetStringAsync(url);
            var quote = JsonConvert.DeserializeObject<FinnHubQuote>(json);

            return quote?.c;
        }
    }
}
