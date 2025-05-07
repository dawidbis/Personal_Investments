using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLogic
{
    public class UserInvestment
    {
        public int UserId { get; set; }
        public int InvestmentId { get; set; }

        public User User { get; set; }
        public Investment Investment { get; set; }
    }
}
