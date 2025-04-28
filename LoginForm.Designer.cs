namespace Personal_Investment_App
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtLogin = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            btnRegister = new Button();
            chckPassword = new CheckBox();
            lnkPassword = new LinkLabel();
            SuspendLayout();
            // 
            // txtLogin
            // 
            txtLogin.BackColor = Color.Black;
            txtLogin.Font = new Font("Segoe UI", 24F);
            txtLogin.ForeColor = Color.FromArgb(224, 224, 224);
            txtLogin.Location = new Point(42, 94);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(424, 50);
            txtLogin.TabIndex = 0;
            txtLogin.Text = "login";
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.Black;
            txtPassword.Font = new Font("Segoe UI", 24F);
            txtPassword.ForeColor = Color.FromArgb(224, 224, 224);
            txtPassword.Location = new Point(42, 178);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(424, 50);
            txtPassword.TabIndex = 1;
            txtPassword.Text = "hasło";
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.Green;
            btnLogin.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            btnLogin.ForeColor = Color.FromArgb(224, 224, 224);
            btnLogin.Location = new Point(42, 284);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(200, 60);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "Zaloguj";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            btnLogin.MouseEnter += btnLogin_MouseEnter;
            btnLogin.MouseLeave += btnLogin_MouseLeave;
            // 
            // btnRegister
            // 
            btnRegister.BackColor = Color.Orange;
            btnRegister.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            btnRegister.ForeColor = Color.FromArgb(224, 224, 224);
            btnRegister.Location = new Point(248, 284);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(218, 60);
            btnRegister.TabIndex = 3;
            btnRegister.Text = "Nowe konto";
            btnRegister.UseVisualStyleBackColor = false;
            btnRegister.Click += btnRegister_Click;
            btnRegister.MouseEnter += btnRegister_MouseEnter;
            btnRegister.MouseLeave += btnRegister_MouseLeave;
            // 
            // chckPassword
            // 
            chckPassword.AutoSize = true;
            chckPassword.Font = new Font("Segoe UI", 14F);
            chckPassword.ForeColor = Color.FromArgb(224, 224, 224);
            chckPassword.Location = new Point(336, 234);
            chckPassword.Name = "chckPassword";
            chckPassword.Size = new Size(130, 29);
            chckPassword.TabIndex = 4;
            chckPassword.Text = "Pokaż hasło";
            chckPassword.UseVisualStyleBackColor = true;
            chckPassword.CheckedChanged += chckPassword_CheckedChanged;
            // 
            // lnkPassword
            // 
            lnkPassword.AutoSize = true;
            lnkPassword.Font = new Font("Segoe UI", 14F);
            lnkPassword.Location = new Point(288, 347);
            lnkPassword.Name = "lnkPassword";
            lnkPassword.Size = new Size(178, 25);
            lnkPassword.TabIndex = 5;
            lnkPassword.TabStop = true;
            lnkPassword.Text = "Zapomniałeś hasła?";
            lnkPassword.LinkClicked += lnkPassword_LinkClicked;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.MidnightBlue;
            ClientSize = new Size(521, 405);
            Controls.Add(lnkPassword);
            Controls.Add(chckPassword);
            Controls.Add(btnRegister);
            Controls.Add(btnLogin);
            Controls.Add(txtPassword);
            Controls.Add(txtLogin);
            ForeColor = Color.Black;
            Name = "LoginForm";
            Text = "LoginForm";
            TransparencyKey = Color.Transparent;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtLogin;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnRegister;
        private CheckBox chckPassword;
        private LinkLabel lnkPassword;
    }
}