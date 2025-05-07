using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLogic
{
    [Table("Reports")]
    public class Report
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ReportTypeId { get; set; }
        public string DataJson { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
        public ReportType ReportType { get; set; }
    }
}
