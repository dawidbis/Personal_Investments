using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Investment_App.AlphaVantageApi
{
    public class FinnHubQuote
    {
        public decimal c { get; set; }  // current
        public decimal d { get; set; }  // change
        public decimal dp { get; set; } // change %
        public decimal h { get; set; }
        public decimal l { get; set; }
        public decimal o { get; set; }
        public decimal pc { get; set; }
        public long t { get; set; }
    }
}
