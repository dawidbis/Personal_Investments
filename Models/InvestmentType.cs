using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLogic
{
    [Table("InvestmentTypes")]
    public class InvestmentType
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        //public string RiskLevel { get; set; }

        public InvestmentCategory Category { get; set; }
        public ICollection<Investment> Investments { get; set; }
    }
}
