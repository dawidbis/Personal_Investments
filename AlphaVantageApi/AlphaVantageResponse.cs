using Newtonsoft.Json;
using System.Collections.Generic;

namespace Polygon_api
{
    public class AlphaVantageResponse
    {
        [JsonProperty("Time Series (1min)")]
        public Dictionary<string, AlphaVantageCandleDto> TimeSeriesDaily { get; set; }
    }
}
