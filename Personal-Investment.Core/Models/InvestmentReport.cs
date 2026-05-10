using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Investment.Core.Models
{
    public class InvestmentReport
    {
        public decimal TotalInvested { get; set; }
        public decimal TotalEarned { get; set; }
        public decimal Profit { get; set; }
        public int InvestmentCount { get; set; }
    }
}
