namespace Personal_Investment_App
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.CheckBox chckPassword;
        private System.Windows.Forms.LinkLabel lnkPassword;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.chckPassword = new System.Windows.Forms.CheckBox();
            this.lnkPassword = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();

            // 
            // txtLogin
            // 
            this.txtLogin.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.txtLogin.ForeColor = System.Drawing.Color.White;
            this.txtLogin.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLogin.Location = new System.Drawing.Point(55, 30);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(250, 25);
            this.txtLogin.TabIndex = 0;
            this.txtLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.txtPassword.ForeColor = System.Drawing.Color.White;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPassword.Location = new System.Drawing.Point(55, 70);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(250, 25);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.MediumPurple;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(55, 120);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(120, 40);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Zaloguj";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            this.btnLogin.MouseEnter += new System.EventHandler(this.btnLogin_MouseEnter);
            this.btnLogin.MouseLeave += new System.EventHandler(this.btnLogin_MouseLeave);

            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.MediumPurple;
            this.btnRegister.FlatAppearance.BorderSize = 0;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Location = new System.Drawing.Point(185, 120);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(120, 40);
            this.btnRegister.TabIndex = 3;
            this.btnRegister.Text = "Zarejestruj";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            this.btnRegister.MouseEnter += new System.EventHandler(this.btnRegister_MouseEnter);
            this.btnRegister.MouseLeave += new System.EventHandler(this.btnRegister_MouseLeave);

            // 
            // chckPassword
            // 
            this.chckPassword.AutoSize = true;
            this.chckPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chckPassword.ForeColor = System.Drawing.Color.White;
            this.chckPassword.Location = new System.Drawing.Point(55, 100);
            this.chckPassword.Name = "chckPassword";
            this.chckPassword.Size = new System.Drawing.Size(96, 19);
            this.chckPassword.TabIndex = 4;
            this.chckPassword.Text = "Pokaż hasło";
            this.chckPassword.UseVisualStyleBackColor = true;
            this.chckPassword.CheckedChanged += new System.EventHandler(this.chckPassword_CheckedChanged);

            // 
            // lnkPassword
            // 
            this.lnkPassword.AutoSize = true;
            this.lnkPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lnkPassword.LinkColor = System.Drawing.Color.MediumPurple;
            this.lnkPassword.Location = new System.Drawing.Point(185, 100);
            this.lnkPassword.Name = "lnkPassword";
            this.lnkPassword.Size = new System.Drawing.Size(120, 15);
            this.lnkPassword.TabIndex = 5;
            this.lnkPassword.TabStop = true;
            this.lnkPassword.Text = "Nie pamiętasz hasła?";
            this.lnkPassword.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkPassword_LinkClicked);

            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(360, 200);
            this.Controls.Add(this.lnkPassword);
            this.Controls.Add(this.chckPassword);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Logowanie";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}