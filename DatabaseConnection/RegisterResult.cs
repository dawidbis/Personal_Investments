using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Investment_App.DatabaseConnection
{
    public class RegisterResult
    {
        public bool Success { get; set; }
        public ErrorCode Code { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
