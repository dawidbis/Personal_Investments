#pragma warning disable CA1416 
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.Devices;
using Personal_Investment.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Personal_Investment.Data.Services;

namespace Personal_Investment.UI.Forms;
    public partial class LoginForm : Form
    {
        // 1. Zmieniamy pole dbManager na AuthService
        private readonly AuthService _authService;
        public string Username { get; private set; }
        public int ZalogowanyUserId { get; private set; }

    // Konstruktor dla Designera (musi być pusty)
    public LoginForm()
        {
            InitializeComponent();
            ApplyTheme();
        }

        // 2. Konstruktor dla DI (wstrzykujemy AuthService)
        public LoginForm(AuthService authService) : this()
        {
            _authService = authService;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            Username = txtLogin.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Proszę wpisać nazwę użytkownika oraz hasło.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Używamy async/await i wywołujemy metodę z AuthService
            var result = await _authService.LoginAsync(Username, password);

             if (result.Success)
             {
                ZalogowanyUserId = result.UserId; // Zapamiętujemy ID do przekazania do MainWindow
                this.DialogResult = DialogResult.OK;
                this.Close();
             }
    }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // 4. Formularze też pobieramy z DI przez Program.ServiceProvider
            // To pokaże rekruterowi, że rozumiesz jak działa kontener
            var registerForm = Program.ServiceProvider.GetRequiredService<RegisterForm>();
            registerForm.ShowDialog();
        }

        private void lnkPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var forgotPasswordForm = Program.ServiceProvider.GetRequiredService<ForgotPassword>();
            forgotPasswordForm.ShowDialog();
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

        private void chckPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chckPassword.Checked;
        }
    }
