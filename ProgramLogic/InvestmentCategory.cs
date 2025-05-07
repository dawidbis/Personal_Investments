using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLogic
{
    [Table("InvestmentCategories")]
    public class InvestmentCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<InvestmentType> InvestmentTypes { get; set; }
    }
}
