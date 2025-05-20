#pragma warning disable CA1416 
using DatabaseConnection;
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

        private DatabaseManager dbManager;

        public LoginForm(DatabaseManager dbManager)
        {
            InitializeComponent();
            this.dbManager = dbManager;
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            this.BackColor = Color.Black;

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = Color.MediumPurple;
                    btn.ForeColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    btn.Size = new Size(120, 40); // dopasuj w razie potrzeby
                }
                else if (ctrl is TextBox txt)
                {
                    txt.BackColor = Color.FromArgb(30, 30, 30);
                    txt.ForeColor = Color.White;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (ctrl is Label lbl)
                {
                    lbl.ForeColor = Color.White;
                }
                else if (ctrl is LinkLabel link)
                {
                    link.LinkColor = Color.MediumPurple;
                    link.ActiveLinkColor = Color.DarkViolet;
                }
                else if (ctrl is CheckBox chk)
                {
                    chk.ForeColor = Color.White;
                }
            }
        }
        private void btnLogin_MouseEnter(object sender, EventArgs e)
        {

            btnLogin.BackColor = Color.DarkViolet;
            btnLogin.ForeColor = Color.White;
            btnLogin.Cursor = Cursors.Hand;
        }

        private void btnLogin_MouseLeave(object sender, EventArgs e)
        {
            btnLogin.BackColor = Color.MediumPurple;
            btnLogin.ForeColor = Color.White;
        }

        private void btnRegister_MouseEnter(object sender, EventArgs e)
        {
            btnRegister.BackColor = Color.DarkViolet;
            btnRegister.ForeColor = Color.White;
            btnRegister.Cursor = Cursors.Hand;
        }

        private void btnRegister_MouseLeave(object sender, EventArgs e)
        {
            btnRegister.BackColor = Color.MediumPurple;
            btnRegister.ForeColor = Color.White;
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

            var result = dbManager.Login(txtLogin.Text, txtPassword.Text);

            if (result.Success)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(result.ErrorMessage, "Błąd logowania",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chckPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chckPassword.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
            }
        }
        
        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm okno = new RegisterForm(dbManager);
            okno.ShowDialog();
        }

        private void lnkPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ForgotPassword oknoResetowanie = new ForgotPassword(dbManager);
            oknoResetowanie.ShowDialog();
        }
    }
}
