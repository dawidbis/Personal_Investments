using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Personal_Investment_App.FinnhubApi
{
    public class TwelveDataMeta
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("interval")]
        public string Interval { get; set; }

        [JsonPropertyName("currency_base")]
        public string CurrencyBase { get; set; }

        [JsonPropertyName("currency_quote")]
        public string CurrencyQuote { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
