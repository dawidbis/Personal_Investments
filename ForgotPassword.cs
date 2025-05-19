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

namespace Personal_Investment_App
{
    public partial class ForgotPassword : Form
    {
        private DatabaseManager dbManager;
        public ForgotPassword(DatabaseManager dbManager)
        {
            InitializeComponent();
            this.dbManager = dbManager;
        }

        private void btnWygenerujKod_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text;
            string email = txtEmail.Text;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Wprowadź login i email.");
                return;
            }

            if (!dbManager.DoesUserExist(login, email))
            {
                MessageBox.Show("Nie znaleziono użytkownika.");
                return;
            }

            string resetCode = Guid.NewGuid().ToString().Substring(0, 6); // np. 6-cyfrowy kod
            dbManager.SaveResetCode(login, resetCode);

            MessageBox.Show($"Twój kod resetu to: {resetCode}", "Kod resetu");

            var changePasswordForm = new ChangePasswordForm(dbManager, login);
            this.Hide();
            changePasswordForm.ShowDialog();
            this.Close(); // wracamy do LoginForm po zmianie hasła
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAnuluj_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtLogin_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
