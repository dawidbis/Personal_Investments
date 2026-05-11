namespace Personal_Investment.UI.Forms;
    partial class RegisterForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.Label lblEmail;

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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterForm));
        txtUsername = new TextBox();
        txtPassword = new TextBox();
        txtConfirmPassword = new TextBox();
        txtEmail = new TextBox();
        btnRegister = new Button();
        lblUsername = new Label();
        lblPassword = new Label();
        lblConfirmPassword = new Label();
        lblEmail = new Label();
        SuspendLayout();
        // 
        // txtUsername
        // 
        txtUsername.BackColor = Color.FromArgb(30, 30, 30);
        txtUsername.BorderStyle = BorderStyle.FixedSingle;
        txtUsername.Font = new Font("Segoe UI", 10F);
        txtUsername.ForeColor = Color.White;
        txtUsername.Location = new Point(198, 31);
        txtUsername.Name = "txtUsername";
        txtUsername.Size = new Size(190, 30);
        txtUsername.TabIndex = 0;
        // 
        // txtPassword
        // 
        txtPassword.BackColor = Color.FromArgb(30, 30, 30);
        txtPassword.BorderStyle = BorderStyle.FixedSingle;
        txtPassword.Font = new Font("Segoe UI", 10F);
        txtPassword.ForeColor = Color.White;
        txtPassword.Location = new Point(198, 67);
        txtPassword.Name = "txtPassword";
        txtPassword.Size = new Size(190, 30);
        txtPassword.TabIndex = 1;
        txtPassword.UseSystemPasswordChar = true;
        // 
        // txtConfirmPassword
        // 
        txtConfirmPassword.BackColor = Color.FromArgb(30, 30, 30);
        txtConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
        txtConfirmPassword.Font = new Font("Segoe UI", 10F);
        txtConfirmPassword.ForeColor = Color.White;
        txtConfirmPassword.Location = new Point(198, 105);
        txtConfirmPassword.Name = "txtConfirmPassword";
        txtConfirmPassword.Size = new Size(190, 30);
        txtConfirmPassword.TabIndex = 2;
        txtConfirmPassword.UseSystemPasswordChar = true;
        // 
        // txtEmail
        // 
        txtEmail.BackColor = Color.FromArgb(30, 30, 30);
        txtEmail.BorderStyle = BorderStyle.FixedSingle;
        txtEmail.Font = new Font("Segoe UI", 10F);
        txtEmail.ForeColor = Color.White;
        txtEmail.Location = new Point(198, 151);
        txtEmail.Name = "txtEmail";
        txtEmail.Size = new Size(190, 30);
        txtEmail.TabIndex = 2;
        // 
        // btnRegister
        // 
        btnRegister.BackColor = Color.MediumPurple;
        btnRegister.FlatAppearance.BorderSize = 0;
        btnRegister.FlatStyle = FlatStyle.Flat;
        btnRegister.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnRegister.ForeColor = Color.White;
        btnRegister.Location = new Point(198, 197);
        btnRegister.Name = "btnRegister";
        btnRegister.Size = new Size(190, 40);
        btnRegister.TabIndex = 3;
        btnRegister.Text = "Zarejestruj";
        btnRegister.UseVisualStyleBackColor = false;
        btnRegister.Click += btnRegister_Click;
        // 
        // lblUsername
        // 
        lblUsername.AutoSize = true;
        lblUsername.ForeColor = Color.White;
        lblUsername.Location = new Point(50, 35);
        lblUsername.Name = "lblUsername";
        lblUsername.Size = new Size(142, 20);
        lblUsername.TabIndex = 4;
        lblUsername.Text = "Nazwa użytkownika:";
        // 
        // lblPassword
        // 
        lblPassword.AutoSize = true;
        lblPassword.ForeColor = Color.White;
        lblPassword.Location = new Point(50, 75);
        lblPassword.Name = "lblPassword";
        lblPassword.Size = new Size(50, 20);
        lblPassword.TabIndex = 5;
        lblPassword.Text = "Hasło:";
        // 
        // lblConfirmPassword
        // 
        lblConfirmPassword.AutoSize = true;
        lblConfirmPassword.ForeColor = Color.White;
        lblConfirmPassword.Location = new Point(50, 115);
        lblConfirmPassword.Name = "lblConfirmPassword";
        lblConfirmPassword.Size = new Size(116, 20);
        lblConfirmPassword.TabIndex = 6;
        lblConfirmPassword.Text = "Potwierdź hasło:";
        // 
        // lblEmail
        // 
        lblEmail.AutoSize = true;
        lblEmail.ForeColor = Color.White;
        lblEmail.Location = new Point(50, 155);
        lblEmail.Name = "lblEmail";
        lblEmail.Size = new Size(49, 20);
        lblEmail.TabIndex = 7;
        lblEmail.Text = "Email:";
        // 
        // RegisterForm
        // 
        BackColor = Color.Black;
        ClientSize = new Size(439, 300);
        Controls.Add(lblConfirmPassword);
        Controls.Add(lblPassword);
        Controls.Add(lblUsername);
        Controls.Add(lblEmail);
        Controls.Add(btnRegister);
        Controls.Add(txtConfirmPassword);
        Controls.Add(txtPassword);
        Controls.Add(txtUsername);
        Controls.Add(txtEmail);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Icon = (Icon)resources.GetObject("$this.Icon");
        MaximizeBox = false;
        Name = "RegisterForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Rejestracja";
        ResumeLayout(false);
        PerformLayout();
    }
}