using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Investment.Core.Models
{
    public class WatchlistItem
    {
        public int Id { get; set; }
        public string Ticker { get; set; }
        public decimal LastPrice { get; set; }
    }
}
