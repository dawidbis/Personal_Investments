namespace Personal_Investment.UI.Forms;
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
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
        txtLogin.BackColor = Color.FromArgb(30, 30, 30);
        txtLogin.BorderStyle = BorderStyle.FixedSingle;
        txtLogin.Font = new Font("Segoe UI", 10F);
        txtLogin.ForeColor = Color.White;
        txtLogin.Location = new Point(63, 40);
        txtLogin.Margin = new Padding(3, 4, 3, 4);
        txtLogin.Name = "txtLogin";
        txtLogin.Size = new Size(285, 30);
        txtLogin.TabIndex = 0;
        // 
        // txtPassword
        // 
        txtPassword.BackColor = Color.FromArgb(30, 30, 30);
        txtPassword.BorderStyle = BorderStyle.FixedSingle;
        txtPassword.Font = new Font("Segoe UI", 10F);
        txtPassword.ForeColor = Color.White;
        txtPassword.Location = new Point(63, 93);
        txtPassword.Margin = new Padding(3, 4, 3, 4);
        txtPassword.Name = "txtPassword";
        txtPassword.Size = new Size(285, 30);
        txtPassword.TabIndex = 1;
        txtPassword.UseSystemPasswordChar = true;
        // 
        // btnLogin
        // 
        btnLogin.BackColor = Color.MediumPurple;
        btnLogin.FlatAppearance.BorderSize = 0;
        btnLogin.FlatStyle = FlatStyle.Flat;
        btnLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnLogin.ForeColor = Color.White;
        btnLogin.Location = new Point(63, 160);
        btnLogin.Margin = new Padding(3, 4, 3, 4);
        btnLogin.Name = "btnLogin";
        btnLogin.Size = new Size(137, 53);
        btnLogin.TabIndex = 2;
        btnLogin.Text = "Zaloguj";
        btnLogin.UseVisualStyleBackColor = false;
        btnLogin.Click += btnLogin_Click;
        btnLogin.MouseEnter += btnLogin_MouseEnter;
        btnLogin.MouseLeave += btnLogin_MouseLeave;
        // 
        // btnRegister
        // 
        btnRegister.BackColor = Color.MediumPurple;
        btnRegister.FlatAppearance.BorderSize = 0;
        btnRegister.FlatStyle = FlatStyle.Flat;
        btnRegister.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnRegister.ForeColor = Color.White;
        btnRegister.Location = new Point(211, 160);
        btnRegister.Margin = new Padding(3, 4, 3, 4);
        btnRegister.Name = "btnRegister";
        btnRegister.Size = new Size(137, 53);
        btnRegister.TabIndex = 3;
        btnRegister.Text = "Zarejestruj";
        btnRegister.UseVisualStyleBackColor = false;
        btnRegister.Click += btnRegister_Click;
        btnRegister.MouseEnter += btnRegister_MouseEnter;
        btnRegister.MouseLeave += btnRegister_MouseLeave;
        // 
        // chckPassword
        // 
        chckPassword.AutoSize = true;
        chckPassword.Font = new Font("Segoe UI", 9F);
        chckPassword.ForeColor = Color.White;
        chckPassword.Location = new Point(63, 133);
        chckPassword.Margin = new Padding(3, 4, 3, 4);
        chckPassword.Name = "chckPassword";
        chckPassword.Size = new Size(108, 24);
        chckPassword.TabIndex = 4;
        chckPassword.Text = "Pokaż hasło";
        chckPassword.UseVisualStyleBackColor = true;
        chckPassword.CheckedChanged += chckPassword_CheckedChanged;
        // 
        // lnkPassword
        // 
        lnkPassword.AutoSize = true;
        lnkPassword.Font = new Font("Segoe UI", 9F);
        lnkPassword.LinkColor = Color.MediumPurple;
        lnkPassword.Location = new Point(211, 133);
        lnkPassword.Name = "lnkPassword";
        lnkPassword.Size = new Size(149, 20);
        lnkPassword.TabIndex = 5;
        lnkPassword.TabStop = true;
        lnkPassword.Text = "Nie pamiętasz hasła?";
        lnkPassword.LinkClicked += lnkPassword_LinkClicked;
        // 
        // LoginForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.Black;
        ClientSize = new Size(411, 267);
        Controls.Add(lnkPassword);
        Controls.Add(chckPassword);
        Controls.Add(btnRegister);
        Controls.Add(btnLogin);
        Controls.Add(txtPassword);
        Controls.Add(txtLogin);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Icon = (Icon)resources.GetObject("$this.Icon");
        Margin = new Padding(3, 4, 3, 4);
        MaximizeBox = false;
        Name = "LoginForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Logowanie";
        ResumeLayout(false);
        PerformLayout();
    }
}