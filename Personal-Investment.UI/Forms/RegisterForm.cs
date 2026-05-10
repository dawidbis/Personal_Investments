using Personal_Investment.Core.Models;   // Tutaj powinien być RegisterResult
using Personal_Investment.Data.Services; // Poprawiony using
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Personal_Investment.UI.Forms;

public partial class RegisterForm : Form
{
    // 1. Wstrzykujemy AuthService zamiast AppDbContext
    private readonly AuthService _authService;

    // Konstruktor dla Designera
    public RegisterForm()
    {
        InitializeComponent();
    }

    // 2. Konstruktor dla Dependency Injection
    public RegisterForm(AuthService authService) : this()
    {
        _authService = authService;
    }

    private async void btnRegister_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text;
        string password = txtPassword.Text;
        string confirmPassword = txtConfirmPassword.Text;
        string email = txtEmail.Text;

        // --- Walidacja UI ---
        if (!ValidateInputs(username, password, confirmPassword, email))
        {
            return;
        }

        try
        {
            // Zmiana kursora na czas pracy (dobra praktyka w CV)
            Cursor = Cursors.WaitCursor;

            // 3. Wywołanie asynchroniczne logiki z serwisu
            var result = await _authService.RegisterAsync(username, email, password);

            if (result.Success)
            {
                MessageBox.Show("Rejestracja zakończona sukcesem!", "Sukces",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(result.ErrorMessage, "Błąd rejestracji",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Wystąpił nieoczekiwany błąd: {ex.Message}", "Błąd",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            Cursor = Cursors.Default;
        }
    }

    private bool ValidateInputs(string user, string pass, string confirm, string email)
    {
        if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass) || string.IsNullOrWhiteSpace(email))
        {
            MessageBox.Show("Wszystkie pola są wymagane.");
            return false;
        }

        if (pass != confirm)
        {
            MessageBox.Show("Hasła nie są identyczne.");
            return false;
        }

        if (!IsValidEmail(email))
        {
            MessageBox.Show("Podaj poprawny adres e-mail.");
            return false;
        }

        return true;
    }

    private void btnCancel_Click(object sender, EventArgs e) => this.Close();

    private bool IsValidEmail(string email)
    {
        // Uproszczony Regex, wystarczający na potrzeby CV
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}