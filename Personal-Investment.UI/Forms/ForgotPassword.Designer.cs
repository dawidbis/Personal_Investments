
namespace Personal_Investment.UI.Forms;
   partial class ForgotPassword
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtLogin;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnReset;
        private Label lblLogin;
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ForgotPassword));
        txtLogin = new TextBox();
        txtEmail = new TextBox();
        btnReset = new Button();
        lblLogin = new Label();
        lblEmail = new Label();
        SuspendLayout();
        // 
        // txtLogin
        // 
        txtLogin.BackColor = Color.FromArgb(30, 30, 30);
        txtLogin.BorderStyle = BorderStyle.FixedSingle;
        txtLogin.Font = new Font("Segoe UI", 10F);
        txtLogin.ForeColor = Color.White;
        txtLogin.Location = new Point(160, 25);
        txtLogin.Name = "txtLogin";
        txtLogin.Size = new Size(200, 30);
        txtLogin.TabIndex = 0;
        // 
        // txtEmail
        // 
        txtEmail.BackColor = Color.FromArgb(30, 30, 30);
        txtEmail.BorderStyle = BorderStyle.FixedSingle;
        txtEmail.Font = new Font("Segoe UI", 10F);
        txtEmail.ForeColor = Color.White;
        txtEmail.Location = new Point(160, 65);
        txtEmail.Name = "txtEmail";
        txtEmail.Size = new Size(200, 30);
        txtEmail.TabIndex = 1;
        // 
        // btnReset
        // 
        btnReset.BackColor = Color.MediumPurple;
        btnReset.FlatAppearance.BorderSize = 0;
        btnReset.FlatStyle = FlatStyle.Flat;
        btnReset.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnReset.ForeColor = Color.White;
        btnReset.Location = new Point(160, 105);
        btnReset.Name = "btnReset";
        btnReset.Size = new Size(200, 40);
        btnReset.TabIndex = 2;
        btnReset.Text = "Resetuj hasło";
        btnReset.UseVisualStyleBackColor = false;
        btnReset.Click += btnWygenerujKod_Click;
        // 
        // lblLogin
        // 
        lblLogin.AutoSize = true;
        lblLogin.ForeColor = Color.White;
        lblLogin.Location = new Point(50, 30);
        lblLogin.Name = "lblLogin";
        lblLogin.Size = new Size(49, 20);
        lblLogin.TabIndex = 3;
        lblLogin.Text = "Login:";
        // 
        // lblEmail
        // 
        lblEmail.AutoSize = true;
        lblEmail.ForeColor = Color.White;
        lblEmail.Location = new Point(50, 70);
        lblEmail.Name = "lblEmail";
        lblEmail.Size = new Size(49, 20);
        lblEmail.TabIndex = 4;
        lblEmail.Text = "Email:";
        // 
        // ForgotPassword
        // 
        BackColor = Color.Black;
        ClientSize = new Size(400, 170);
        Controls.Add(lblEmail);
        Controls.Add(lblLogin);
        Controls.Add(btnReset);
        Controls.Add(txtEmail);
        Controls.Add(txtLogin);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Icon = (Icon)resources.GetObject("$this.Icon");
        MaximizeBox = false;
        Name = "ForgotPassword";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Resetowanie hasła";
        ResumeLayout(false);
        PerformLayout();
    }
}