using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLogic
{
    [Table("ReturnsHistories")]
    public class ReturnsHistory
    {
        public int Id { get; set; }
        [ForeignKey("Investment")]
        public int InvestmentId { get; set; }
        public DateTime Date { get; set; }
        [Precision(18,6)]
        public decimal Value { get; set; }

        public Investment Investment { get; set; }
    }
}
