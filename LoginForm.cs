using Microsoft.VisualBasic.Devices;
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
    public partial class LoginForm : Form
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_MouseEnter(object sender, EventArgs e)
        {
            btnLogin.BackColor = Color.DarkGray;
            btnLogin.ForeColor = Color.FromArgb(250, 250, 250);
            btnLogin.Cursor = Cursors.Hand;
        }

        private void btnLogin_MouseLeave(object sender, EventArgs e)
        {
            btnLogin.BackColor = Color.DimGray;
            btnLogin.ForeColor = Color.FromArgb(224, 224, 224);
        }

        private void btnRegister_MouseEnter(object sender, EventArgs e)
        {
            btnRegister.BackColor = Color.DarkGray;
            btnRegister.ForeColor = Color.FromArgb(250, 250, 250);
            btnRegister.Cursor = Cursors.Hand;
        }

        private void btnRegister_MouseLeave(object sender, EventArgs e)
        {
            btnRegister.BackColor = Color.DimGray;
            btnRegister.ForeColor = Color.FromArgb(224, 224, 224);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Username = txtLogin.Text;
            Password = txtPassword.Text;
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Proszę wpisać nazwę użytkownika oraz hasło.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            

        }
    }
}
