using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personal_Investment_App
{
    public partial class RegisterForm : Form
    {
        private DatabaseManager dbManager;

        public RegisterForm(DatabaseManager dbManager)
        {
            InitializeComponent();
            this.dbManager = dbManager;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            string email = txtEmail.Text;

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(email) ||
                password != confirmPassword ||
                !IsValidEmail(email))
            {
                MessageBox.Show("Proszę podać poprawne dane i upewnić się, że hasła są zgodne.",
                                "Błąd rejestracji", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = dbManager.Register(username, password, email);

            if (result.Success)
            {
                MessageBox.Show("Rejestracja zakończona sukcesem!", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(result.ErrorMessage, "Błąd rejestracji", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }
}
