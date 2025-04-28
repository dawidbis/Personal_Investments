using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnection
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }   
        public ErrorCode Code { get; set; }    
    }

    public enum ErrorCode
    {
        None,
        UserNotFound,
        InvalidPassword,
        DbUnavailable,
        UnknownError
    }
}
