using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Investment.Data.DatabaseConnection
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public int UserId { get; set; }
    }

    public enum ErrorCode
    {
        None,
        UserNotFound,
        InvalidPassword,
        DbUnavailable,
        UserAlreadyExists,
        UnknownError
    }
}
