using Personal_Investment.Data.DatabaseConnection;
using Personal_Investment.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Personal_Investment.UI.Forms;

public partial class ChangePasswordForm : Form
{
    private readonly AuthService _authService;
    private readonly string _login;
    private string _currentLogin;

    // Konstruktor dla Designera
    public ChangePasswordForm()
    {
        InitializeComponent();
    }

    // Konstruktor dla Dependency Injection
    public ChangePasswordForm(AuthService authService, string login) : this()
    {
        _authService = authService;
        _login = login;
    }

    public void SetUserContext(string login)
    {
        _currentLogin = login;
    }

    private async void btnChangePassword_Click(object sender, EventArgs e)
    {
        string code = txtResetCode.Text.Trim();
        string newPassword = txtNewPassword.Text;
        string confirmPassword = txtConfirmPassword.Text;

        // 1. Walidacja UI
        if (string.IsNullOrWhiteSpace(code) || newPassword != confirmPassword || string.IsNullOrEmpty(newPassword))
        {
            MessageBox.Show("Sprawdź poprawność danych i zgodność haseł.", "Błąd");
            return;
        }

        try
        {
            Cursor = Cursors.WaitCursor;

            // 2. Sprawdzenie kodu przez serwis
            bool isValid = await _authService.IsResetCodeValidAsync(_login, code);
            if (!isValid)
            {
                MessageBox.Show("Nieprawidłowy kod resetu.", "Błąd");
                return;
            }

            // 3. Aktualizacja hasła
            bool success = await _authService.UpdatePasswordAsync(_login, newPassword);
            if (success)
            {
                MessageBox.Show("Hasło zostało zmienione.", "Sukces");
                this.Close();
            }
        }
        finally
        {
            Cursor = Cursors.Default;
        }
    }
}
