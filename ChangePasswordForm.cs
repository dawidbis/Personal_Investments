using DatabaseConnection;
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

namespace Personal_Investment_App
{
    public partial class ChangePasswordForm : Form
    {
        private readonly DatabaseManager dbManager;
        private readonly string login;

        public ChangePasswordForm(DatabaseManager dbManager, string login)
        {
            InitializeComponent();
            this.dbManager = dbManager;
            this.login = login;
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            string code = txtResetCode.Text.Trim();
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show("Wprowadź kod resetu.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Wprowadź i potwierdź nowe hasło.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Hasła nie są takie same.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!dbManager.IsResetCodeValid(login, code))
            {
                MessageBox.Show("Nieprawidłowy kod resetu.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dbManager.UpdatePassword(login, newPassword);
            MessageBox.Show("Hasło zostało zmienione.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
