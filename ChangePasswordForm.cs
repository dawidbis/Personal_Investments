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
        private string login;
        private DatabaseManager dbManager;
        public ChangePasswordForm(DatabaseManager dbManager, string login)
        {
            InitializeComponent();
            this.dbManager = dbManager;
            this.login = login;
        }

        private void btnKodResetu_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnNoweHaslo_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnZmienHaslo_Click(object sender, EventArgs e)
        {
            string code = txtKodResetu.Text;
            string newPassword = txtNoweHaslo.Text;

            if (!dbManager.IsResetCodeValid(login, code))
            {
                MessageBox.Show("Nieprawidłowy kod.");
                return;
            }

            dbManager.UpdatePassword(login, newPassword);
            MessageBox.Show("Hasło zmienione.");
            this.Close();
        }
    }
}
