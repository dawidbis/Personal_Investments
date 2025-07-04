using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Investment_App.FinnhubApi
{
    public class PolygonAggsResponse
    {
        public string status { get; set; }
        public List<PolygonAggsResult> results { get; set; }
    }

    public class PolygonAggsResult
    {
        public decimal o { get; set; }  // open
        public decimal h { get; set; }  // high
        public decimal l { get; set; }  // low
        public decimal c { get; set; }  // close
        public decimal v { get; set; }  // volume
        public long t { get; set; }     // timestamp (ms)
    }
}
