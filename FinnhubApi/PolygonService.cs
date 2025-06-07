using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Investment_App.FinnhubApi
{
    public static class PolygonService
    {
        private static readonly HttpClient _client = new HttpClient();
        private static readonly string _apiKey = "B37GC7G0jp7U4jBcubVJiGNBcrjncWr3";

        public static DateTime GetLastTradingDay(DateTime date)
        {
            while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                date = date.AddDays(-1);
            }
            return date;
        }

        public static async Task<decimal?> GetHistoricalClosePriceAsync(string ticker, DateTime date)
        {
            try
            {
                string formattedDate = date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                string url = $"https://api.polygon.io/v1/open-close/{ticker}/{formattedDate}?adjusted=true&apiKey={_apiKey}";
                string json = await _client.GetStringAsync(url);

                var result = JsonConvert.DeserializeObject<PolygonDailyResponse>(json);

                if (result.status == "OK")
                    return result.close;

                return null;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Błąd HTTP podczas pobierania danych z Polygon.io: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Nieoczekiwany błąd: {ex.Message}");
                return null;
            }
        }
    }
}
