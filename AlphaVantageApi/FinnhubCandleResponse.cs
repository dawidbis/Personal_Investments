using Newtonsoft.Json;

namespace Polygon_api
{
    public class FinnhubCandleResponse
    {
        [JsonProperty("c")]
        public decimal[] c { get; set; } // close

        [JsonProperty("h")]
        public decimal[] h { get; set; } // high

        [JsonProperty("l")]
        public decimal[] l { get; set; } // low

        [JsonProperty("o")]
        public decimal[] o { get; set; } // open

        [JsonProperty("t")]
        public long[] t { get; set; } // timestamps

        [JsonProperty("v")]
        public long[] v { get; set; } // volume

        [JsonProperty("s")]
        public string s { get; set; } // status: "ok" or "no_data"
    }
}
