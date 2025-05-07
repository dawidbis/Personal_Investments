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

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string username = txtLogin.Text;
            string password = txtHasło.Text;
            string email = txtEmail.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || !IsValidEmail(email))
            {
                MessageBox.Show("Proszę podać poprawne dane.", "Błąd rejestracji", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnAnuluj_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
