using Microsoft.AspNetCore.Identity;

namespace Personal_Investment.Core.Security;

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
}

public class PasswordHasher : IPasswordHasher
{
    // Korzystamy z PasswordHasher od Microsoftu - to bezpieczniejsze niż własne implementacje
    private readonly PasswordHasher<object> _hasher = new();

    public string HashPassword(string password) =>
        _hasher.HashPassword(new object(), password);

    public bool VerifyPassword(string password, string hashedPassword) =>
    _hasher.VerifyHashedPassword(new object(), hashedPassword, password)
    != PasswordVerificationResult.Failed;
}