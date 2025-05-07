
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgramLogic
{
    [Table("Users")]
    public class User
    {
            public int Id { get; set; }
            public string Username { get; set; }
            public string PasswordHash { get; set; }
            public string Token { get; set; }
            public string Email { get; set; }
            public DateTime CreatedAt { get; set; }
            public bool IsActive { get; set; }

            public ICollection<UserInvestment> UserInvestments { get; set; }
            public ICollection<Report> Reports { get; set; }
            public ICollection<Investment> Investments { get; set; }
    }
}
