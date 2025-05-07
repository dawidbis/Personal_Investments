using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLogic
{
    [Table("ReportTypes")]
    public class ReportType
    {
        public int Id { get; set; }
        public ICollection<Report> Reports { get; set; }
    }
}
