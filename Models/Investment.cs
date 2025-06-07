using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLogic
{
    [Table("Investments")]
    public class Investment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public int NumberOfShares { get; set; }
        public DateTime DateOfInvestment { get; set; }
        [Range(0.01, 100)]
        [Column(TypeName = "decimal(5,2)")]
        public decimal ExpectedReturnPercent { get; set; }
        [Range(-100, -0.01)]
        [Column(TypeName = "decimal(5,2)")]
        public decimal StopLossPercent { get; set; }
        public decimal BuyPrice { get; set; }
        public string Notes { get; set; }
        public bool IsSold { get; set; }

        public User User { get; set; }
        public InvestmentType Type { get; set; }
        public ICollection<UserInvestment> UserInvestments { get; set; }
        public ICollection<ReturnsHistory> ReturnsHistories { get; set; }
    }

}
