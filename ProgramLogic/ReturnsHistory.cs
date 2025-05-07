using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLogic
{
    public class ReturnsHistory
    {
        public int Id { get; set; }
        public int InvestmentId { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }

        public Investment Investment { get; set; }
    }
}
