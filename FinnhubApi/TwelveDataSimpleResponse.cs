using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Personal_Investment_App.FinnhubApi
{
    public class TwelveDataSimpleResponse
    {
        [JsonPropertyName("meta")]
        public TwelveDataMeta Meta { get; set; }

        [JsonPropertyName("values")]
        public List<TwelveDataValue> Values { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }  // Na wypadek błędu
    }
}
