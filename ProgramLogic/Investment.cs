using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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
        public decimal AmountInvested { get; set; }
        public DateTime DateOfInvestment { get; set; }
        public decimal ExpectedReturn { get; set; }
        public string Notes { get; set; }

        public User User { get; set; }
        public InvestmentType Type { get; set; }
        public ICollection<UserInvestment> UserInvestments { get; set; }
        public ICollection<ReturnsHistory> ReturnsHistory { get; set; }
    }

}
