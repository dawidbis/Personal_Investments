using Newtonsoft.Json;
using Personal_Investment_App.AlphaVantageApi;

namespace Polygon_api
{
    public static class FinnhubService
    {
        private static readonly HttpClient _client = new HttpClient();
        private static readonly string _apiKey = "d11k9ihr01qjtpe7e510d11k9ihr01qjtpe7e51g";

        public static async Task<List<FinnHubQuote>> GetMinuteCandlesAsync(string ticker)
        {
            // Czas w Unix time (ostatnie 60 minut)
            var to = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var from = to - 60 * 60; // ostatnia godzina

            var url = $"https://finnhub.io/api/v1/stock/candle?symbol={ticker}&resolution=1&from={from}&to={to}&token={_apiKey}";

            var response = await _client.GetStringAsync(url);
            var candleResponse = JsonConvert.DeserializeObject<FinnhubCandleResponse>(response);

            if (candleResponse?.c == null || candleResponse.s != "ok")
                return new List<FinnHubQuote>();

            return MapToCandles(ticker, candleResponse);
        }

        private static List<FinnHubQuote> MapToCandles(string ticker, FinnhubCandleResponse response)
        {
            var candles = new List<FinnHubQuote>();

            for (int i = 0; i < response.t.Length; i++)
            {
                candles.Add(new FinnHubQuote
                {
                    Ticker = ticker,
                    Date = DateTimeOffset.FromUnixTimeSeconds(response.t[i]).UtcDateTime,
                    o = response.o[i],
                    h = response.h[i],
                    l = response.l[i],
                    c = response.c[i],
                    V = response.v[i]
                });
            }

            return candles;
        }

        public static async Task<decimal?> GetCurrentQuoteAsync(string symbol)
        {
            try
            {
                var url = $"https://finnhub.io/api/v1/quote?symbol={symbol}&token={_apiKey}";
                var json = await _client.GetStringAsync(url);
                var quote = JsonConvert.DeserializeObject<FinnHubQuote>(json);

                // Walidacja: brak danych = ticker nie istnieje
                if (quote == null || quote.c == null || quote.c == 0 || quote.t == 0)
                {
                    return null;
                }

                return quote.c;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił nieoczekiwany błąd{ex.Message}");
                return null;
            }
        }

        public static async Task<decimal?> GetCurrentCryptoQuoteAsync(string cryptoSymbol)
        {
            try
            {
                // cryptoSymbol np. "BINANCE:BTCUSDT"
                var url = $"https://finnhub.io/api/v1/quote?symbol={cryptoSymbol}&token={_apiKey}";
                var json = await _client.GetStringAsync(url);
                var quote = JsonConvert.DeserializeObject<FinnHubQuote>(json);

                if (quote == null || quote.c == null || quote.c == 0 || quote.t == 0)
                    return null;

                return quote.c;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd pobierania ceny kryptowaluty: {ex.Message}");
                return null;
            }
        }

    }
}
