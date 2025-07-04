using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Investment_App.AlphaVantageApi
{
    public class FinnHubQuote
    {

        public decimal? c { get; set; } // close
        public decimal? d { get; set; } // change
        public decimal? dp { get; set; } // percent change
        public decimal? h { get; set; } // high
        public decimal? l { get; set; } // low
        public decimal? o { get; set; } // open
        public decimal? pc { get; set; } // previous close
        public long t { get; set; } // timestamp

        // Dodatkowe właściwości używane w MapToCandles
        public string Ticker { get; set; } // symbol
        public DateTime Date { get; set; } // konwertowana data
        public decimal V { get; set; } // volume
    }
}
