using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Personal_Investment_App.FinnhubApi
{
    public class TwelveDataValue
    {
        [JsonPropertyName("datetime")]
        public string Datetime { get; set; }

        [JsonPropertyName("open")]
        public string Open { get; set; }

        [JsonPropertyName("high")]
        public string High { get; set; }

        [JsonPropertyName("low")]
        public string Low { get; set; }

        [JsonPropertyName("close")]
        public string Close { get; set; }
    }
}
