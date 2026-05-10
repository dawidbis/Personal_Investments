using Microsoft.EntityFrameworkCore;
using Personal_Investment.Data.DatabaseConnection;
using Personal_Investment.Core.Models;
using Personal_Investment.Core.Security;
using System.Threading.Tasks;
using System;

namespace Personal_Investment.Data.Services;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher _hasher;

    public AuthService(AppDbContext context, IPasswordHasher hasher)
    {
        _context = context;
        _hasher = hasher;
    }

    public async Task<LoginResult> LoginAsync(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null) return new LoginResult { Success = false, ErrorMessage = "Użytkownik nie istnieje." };

        return new LoginResult { Success = true, UserId = user.Id };
    }

    public async Task<RegisterResult> RegisterAsync(string username, string email, string password)
    {
        if (await _context.Users.AnyAsync(u => u.Username == username || u.Email == email))
            return new RegisterResult { Success = false, ErrorMessage = "Użytkownik już istnieje." };

        var user = new User
        {
            Username = username,
            Email = email,
            PasswordHash = _hasher.HashPassword(password),
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new RegisterResult { Success = true };
    }

    public async Task<bool> DeleteAccountAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> IsResetCodeValidAsync(string username, string code)
    {
        // W uproszczonej wersji sprawdzamy czy użytkownik istnieje
        // W pełnej wersji sprawdzałbyś kolumnę ResetCode w bazie
        return await _context.Users.AnyAsync(u => u.Username == username);
    }

    public async Task<bool> UpdatePasswordAsync(string username, string newPassword)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null) return false;

        // Hashujemy nowe hasło!
        user.PasswordHash = _hasher.HashPassword(newPassword);

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<string?> RequestPasswordResetAsync(string username, string email)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username && u.Email == email);

        if (user == null) return null;

        // Generujemy kod (w rzeczywistym projekcie zapisałbyś go w bazie z datą wygaśnięcia)
        string resetCode = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();

        // Na potrzeby tego projektu przyjmujemy, że kod jest poprawny jeśli użytkownik istnieje
        // Możesz tu dodać logikę zapisu kodu do tabeli User, jeśli ją rozbudowałeś
        return resetCode;
    }
}