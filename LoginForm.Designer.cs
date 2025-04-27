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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            txtLogin = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            btnRegister = new Button();
            SuspendLayout();
            // 
            // txtLogin
            // 
            txtLogin.BackColor = Color.Black;
            txtLogin.Font = new Font("Segoe UI", 24F);
            txtLogin.ForeColor = Color.FromArgb(224, 224, 224);
            txtLogin.Location = new Point(445, 160);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(340, 50);
            txtLogin.TabIndex = 0;
            txtLogin.Text = "login";
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.Black;
            txtPassword.Font = new Font("Segoe UI", 24F);
            txtPassword.ForeColor = Color.FromArgb(224, 224, 224);
            txtPassword.Location = new Point(445, 297);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(340, 50);
            txtPassword.TabIndex = 1;
            txtPassword.Text = "hasło";
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.DimGray;
            btnLogin.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            btnLogin.ForeColor = Color.FromArgb(224, 224, 224);
            btnLogin.Location = new Point(445, 409);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(340, 60);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "Zaloguj";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            btnLogin.MouseEnter += btnLogin_MouseEnter;
            btnLogin.MouseLeave += btnLogin_MouseLeave;
            // 
            // btnRegister
            // 
            btnRegister.BackColor = Color.DimGray;
            btnRegister.Font = new Font("Segoe UI", 24F, FontStyle.Italic);
            btnRegister.ForeColor = Color.FromArgb(224, 224, 224);
            btnRegister.Location = new Point(445, 499);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(340, 60);
            btnRegister.TabIndex = 3;
            btnRegister.Text = "Nowe konto";
            btnRegister.UseVisualStyleBackColor = false;
            btnRegister.MouseEnter += btnRegister_MouseEnter;
            btnRegister.MouseLeave += btnRegister_MouseLeave;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1264, 681);
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
    }
}