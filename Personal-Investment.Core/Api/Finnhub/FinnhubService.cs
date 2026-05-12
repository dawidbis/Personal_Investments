using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Personal_Investment.Core.Models;

namespace Personal_Investment.Core.Api.Finnhub
{
    // USUNIĘTO: static
    public class FinnhubService
    {
        private readonly HttpClient _client;
        private readonly string _apiKey;

        // Konstruktor przyjmuje konfigurację i HttpClient z DI
        public FinnhubService(IConfiguration config, HttpClient client)
        {
            _client = client;
            _apiKey = config["ApiKeys:Finnhub"]; // Pobieranie klucza z JSONa
        }

        // USUNIĘTO: static ze wszystkich metod poniżej
        public async Task<List<FinnHubQuote>> GetMinuteCandlesAsync(string ticker)
        {
            var to = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var from = to - 60 * 60;

            var url = $"https://finnhub.io/api/v1/stock/candle?symbol={ticker}&resolution=1&from={from}&to={to}&token={_apiKey}";

            var response = await _client.GetStringAsync(url);
            var candleResponse = JsonConvert.DeserializeObject<FinnhubCandleResponse>(response);

            if (candleResponse?.c == null || candleResponse.s != "ok")
                return new List<FinnHubQuote>();

            return MapToCandles(ticker, candleResponse);
        }

        private List<FinnHubQuote> MapToCandles(string ticker, FinnhubCandleResponse response)
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

        private async Task<decimal?> FetchQuoteAsync(string url)
        {
            try
            {
                var json = await _client.GetStringAsync(url);
                var quote = JsonConvert.DeserializeObject<FinnHubQuote>(json);

                if (quote?.c == null || quote.c == 0)
                    return null;

                return quote.c;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Błąd połączenia z serwerem dostawcy danych.", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception("Błąd podczas przetwarzania danych z API.", ex);
            }
        }

        public async Task<decimal?> GetCurrentQuoteAsync(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol)) return null;
            string url = $"https://finnhub.io/api/v1/quote?symbol={symbol.ToUpper().Trim()}&token={_apiKey}";
            return await FetchQuoteAsync(url);
        }

        public async Task<decimal?> GetCurrentCryptoQuoteAsync(string userSymbol)
        {
            if (string.IsNullOrWhiteSpace(userSymbol)) return null;
            string cleanSymbol = userSymbol.Trim().ToUpper();
            string url = $"https://finnhub.io/api/v1/quote?symbol=BINANCE:{cleanSymbol}USDT&token={_apiKey}";
            return await FetchQuoteAsync(url);
        }

        // Nowa metoda pomocnicza zwracająca pełny obiekt
        private async Task<MarketData?> FetchMarketDataAsync(string url)
        {
            try
            {
                var json = await _client.GetStringAsync(url);
                var quote = JsonConvert.DeserializeObject<FinnHubQuote>(json);

                if (quote == null || quote.c == 0) return null;

                // FinnhubService.cs ok. linii 105
                return new MarketData
                {
                    // Używamy ?? 0, aby w razie błędu API przypisać zero zamiast błędu
                    Price = quote.c ?? 0,
                    PercentChange = quote.dp ?? 0
                };
            }
            catch
            {
                return null;
            }
        }

        // Publiczna metoda dla akcji
        public async Task<MarketData?> GetFullQuoteAsync(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol)) return null;
            string url = $"https://finnhub.io/api/v1/quote?symbol={symbol.ToUpper().Trim()}&token={_apiKey}";
            return await FetchMarketDataAsync(url);
        }

        // Publiczna metoda dla krypto
        public async Task<MarketData?> GetFullCryptoQuoteAsync(string userSymbol)
        {
            if (string.IsNullOrWhiteSpace(userSymbol)) return null;
            string cleanSymbol = userSymbol.Trim().ToUpper();
            string url = $"https://finnhub.io/api/v1/quote?symbol=BINANCE:{cleanSymbol}USDT&token={_apiKey}";
            return await FetchMarketDataAsync(url);
        }
    }
}