namespace Personal_Investment_App
{
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
            this.txtResetCode = new TextBox();
            this.txtNewPassword = new TextBox();
            this.txtConfirmPassword = new TextBox();
            this.btnChangePassword = new Button();
            this.lblResetCode = new Label();
            this.lblNewPassword = new Label();
            this.lblConfirmPassword = new Label();

            // txtResetCode
            this.txtResetCode.BackColor = Color.FromArgb(30, 30, 30);
            this.txtResetCode.ForeColor = Color.White;
            this.txtResetCode.Font = new Font("Segoe UI", 10F);
            this.txtResetCode.Location = new Point(180, 20);
            this.txtResetCode.Name = "txtResetCode";
            this.txtResetCode.Size = new Size(200, 25);

            // txtNewPassword
            this.txtNewPassword.BackColor = Color.FromArgb(30, 30, 30);
            this.txtNewPassword.ForeColor = Color.White;
            this.txtNewPassword.Font = new Font("Segoe UI", 10F);
            this.txtNewPassword.Location = new Point(180, 60);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Size = new Size(200, 25);
            this.txtNewPassword.UseSystemPasswordChar = true;

            // txtConfirmPassword
            this.txtConfirmPassword.BackColor = Color.FromArgb(30, 30, 30);
            this.txtConfirmPassword.ForeColor = Color.White;
            this.txtConfirmPassword.Font = new Font("Segoe UI", 10F);
            this.txtConfirmPassword.Location = new Point(180, 100);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.Size = new Size(200, 25);
            this.txtConfirmPassword.UseSystemPasswordChar = true;

            // btnChangePassword
            this.btnChangePassword.BackColor = Color.MediumPurple;
            this.btnChangePassword.FlatAppearance.BorderSize = 0;
            this.btnChangePassword.FlatStyle = FlatStyle.Flat;
            this.btnChangePassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnChangePassword.ForeColor = Color.White;
            this.btnChangePassword.Location = new Point(180, 140);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new Size(200, 40);
            this.btnChangePassword.Text = "Zmień hasło";
            this.btnChangePassword.UseVisualStyleBackColor = false;
            this.btnChangePassword.Click += new EventHandler(this.btnChangePassword_Click);

            // lblResetCode
            this.lblResetCode.AutoSize = true;
            this.lblResetCode.ForeColor = Color.White;
            this.lblResetCode.Location = new Point(60, 25);
            this.lblResetCode.Name = "lblResetCode";
            this.lblResetCode.Size = new Size(75, 15);
            this.lblResetCode.Text = "Kod resetu:";

            // lblNewPassword
            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.ForeColor = Color.White;
            this.lblNewPassword.Location = new Point(60, 65);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new Size(74, 15);
            this.lblNewPassword.Text = "Nowe hasło:";

            // lblConfirmPassword
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.ForeColor = Color.White;
            this.lblConfirmPassword.Location = new Point(60, 105);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new Size(114, 15);
            this.lblConfirmPassword.Text = "Potwierdź hasło:";

            // ChangePasswordForm
            this.BackColor = Color.Black;
            this.ClientSize = new Size(420, 200);
            this.Controls.Add(this.lblResetCode);
            this.Controls.Add(this.lblNewPassword);
            this.Controls.Add(this.lblConfirmPassword);
            this.Controls.Add(this.txtResetCode);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.btnChangePassword);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Zmiana hasła";
        }
    }
}