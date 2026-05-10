using Microsoft.Extensions.DependencyInjection;
using Personal_Investment.Data.Services;

namespace Personal_Investment.UI.Forms;

public partial class ForgotPassword : Form
{
    private readonly AuthService _authService;

    public ForgotPassword()
    {
        InitializeComponent();
    }

    public ForgotPassword(AuthService authService) : this()
    {
        _authService = authService;
    }

    private async void btnWygenerujKod_Click(object sender, EventArgs e)
    {
        string login = txtLogin.Text.Trim();
        string email = txtEmail.Text.Trim();

        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(email))
        {
            MessageBox.Show("Wprowadź login i email.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            Cursor = Cursors.WaitCursor;

            // Logika przeniesiona do serwisu
            string? resetCode = await _authService.RequestPasswordResetAsync(login, email);

            if (resetCode == null)
            {
                MessageBox.Show("Nie znaleziono użytkownika z podanym loginem i emailem.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show($"Twój kod resetu to: {resetCode}", "Kod resetu", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Pobieramy ChangePasswordForm z DI
            var changePassForm = Program.ServiceProvider.GetRequiredService<ChangePasswordForm>();

            // Inicjalizujemy dane w nowym oknie (metoda, którą powinieneś mieć w ChangePasswordForm)
            changePassForm.SetUserContext(login);

            this.Hide();
            changePassForm.ShowDialog();
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Błąd: {ex.Message}");
        }
        finally
        {
            Cursor = Cursors.Default;
        }
    }

    private void btnAnuluj_Click(object sender, EventArgs e) => this.Close();
}