using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLogic
{
    public enum InvestmentType
    {
        Stock,
        Bond,
        MutualFund,
        Cryptocurrency
    }

    public abstract class InvestmentBase
    {
        public int ID { get; set; }
        public int InvestmentID { get; set; }
        public string? Name { get; set; }
        public decimal AmountInvested { get; set; }
        public DateTime DateOfInvestment { get; set; }

        public abstract decimal Profit { get; }
        public decimal ProfitPercentage => AmountInvested != 0 ? (Profit / AmountInvested) * 100 : 0;
    }

    public class Investment : InvestmentBase
    {
        public DateTime DateClosed { get; set; }
        public decimal FinalProfit { get; set; }

        public override decimal Profit => FinalProfit;
    }

    public class ActiveInvestment : InvestmentBase
    {
        public InvestmentType Type { get; set; }
        public decimal CurrentValue { get; set; }
        public decimal? StopLoss { get; set; }
        public decimal? TakeProfit { get; set; }

        public override decimal Profit => CurrentValue - AmountInvested;
    }
}
