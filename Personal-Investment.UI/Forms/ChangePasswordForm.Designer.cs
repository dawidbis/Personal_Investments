namespace Personal_Investment.UI.Forms;
    partial class ChangePasswordForm
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox txtResetCode;
        private TextBox txtNewPassword;
        private TextBox txtConfirmPassword;
        private Button btnChangePassword;
        private Label lblResetCode;
        private Label lblNewPassword;
        private Label lblConfirmPassword;

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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangePasswordForm));
        txtResetCode = new TextBox();
        txtNewPassword = new TextBox();
        txtConfirmPassword = new TextBox();
        btnChangePassword = new Button();
        lblResetCode = new Label();
        lblNewPassword = new Label();
        lblConfirmPassword = new Label();
        SuspendLayout();
        // 
        // txtResetCode
        // 
        txtResetCode.BackColor = Color.FromArgb(30, 30, 30);
        txtResetCode.Font = new Font("Segoe UI", 10F);
        txtResetCode.ForeColor = Color.White;
        txtResetCode.Location = new Point(180, 20);
        txtResetCode.Name = "txtResetCode";
        txtResetCode.Size = new Size(200, 30);
        txtResetCode.TabIndex = 3;
        // 
        // txtNewPassword
        // 
        txtNewPassword.BackColor = Color.FromArgb(30, 30, 30);
        txtNewPassword.Font = new Font("Segoe UI", 10F);
        txtNewPassword.ForeColor = Color.White;
        txtNewPassword.Location = new Point(180, 60);
        txtNewPassword.Name = "txtNewPassword";
        txtNewPassword.Size = new Size(200, 30);
        txtNewPassword.TabIndex = 4;
        txtNewPassword.UseSystemPasswordChar = true;
        // 
        // txtConfirmPassword
        // 
        txtConfirmPassword.BackColor = Color.FromArgb(30, 30, 30);
        txtConfirmPassword.Font = new Font("Segoe UI", 10F);
        txtConfirmPassword.ForeColor = Color.White;
        txtConfirmPassword.Location = new Point(180, 100);
        txtConfirmPassword.Name = "txtConfirmPassword";
        txtConfirmPassword.Size = new Size(200, 30);
        txtConfirmPassword.TabIndex = 5;
        txtConfirmPassword.UseSystemPasswordChar = true;
        // 
        // btnChangePassword
        // 
        btnChangePassword.BackColor = Color.MediumPurple;
        btnChangePassword.FlatAppearance.BorderSize = 0;
        btnChangePassword.FlatStyle = FlatStyle.Flat;
        btnChangePassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnChangePassword.ForeColor = Color.White;
        btnChangePassword.Location = new Point(180, 140);
        btnChangePassword.Name = "btnChangePassword";
        btnChangePassword.Size = new Size(200, 40);
        btnChangePassword.TabIndex = 6;
        btnChangePassword.Text = "Zmień hasło";
        btnChangePassword.UseVisualStyleBackColor = false;
        btnChangePassword.Click += btnChangePassword_Click;
        // 
        // lblResetCode
        // 
        lblResetCode.AutoSize = true;
        lblResetCode.ForeColor = Color.White;
        lblResetCode.Location = new Point(60, 25);
        lblResetCode.Name = "lblResetCode";
        lblResetCode.Size = new Size(83, 20);
        lblResetCode.TabIndex = 0;
        lblResetCode.Text = "Kod resetu:";
        // 
        // lblNewPassword
        // 
        lblNewPassword.AutoSize = true;
        lblNewPassword.ForeColor = Color.White;
        lblNewPassword.Location = new Point(60, 65);
        lblNewPassword.Name = "lblNewPassword";
        lblNewPassword.Size = new Size(90, 20);
        lblNewPassword.TabIndex = 1;
        lblNewPassword.Text = "Nowe hasło:";
        // 
        // lblConfirmPassword
        // 
        lblConfirmPassword.AutoSize = true;
        lblConfirmPassword.ForeColor = Color.White;
        lblConfirmPassword.Location = new Point(60, 105);
        lblConfirmPassword.Name = "lblConfirmPassword";
        lblConfirmPassword.Size = new Size(116, 20);
        lblConfirmPassword.TabIndex = 2;
        lblConfirmPassword.Text = "Potwierdź hasło:";
        // 
        // ChangePasswordForm
        // 
        BackColor = Color.Black;
        ClientSize = new Size(420, 200);
        Controls.Add(lblResetCode);
        Controls.Add(lblNewPassword);
        Controls.Add(lblConfirmPassword);
        Controls.Add(txtResetCode);
        Controls.Add(txtNewPassword);
        Controls.Add(txtConfirmPassword);
        Controls.Add(btnChangePassword);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Icon = (Icon)resources.GetObject("$this.Icon");
        MaximizeBox = false;
        Name = "ChangePasswordForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Zmiana hasła";
        ResumeLayout(false);
        PerformLayout();
    }
}