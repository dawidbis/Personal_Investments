namespace Personal_Investment_App
{
    partial class RegisterForm
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
            btnOk = new Button();
            btnAnuluj = new Button();
            txtLogin = new TextBox();
            txtHasło = new TextBox();
            txtEmail = new TextBox();
            groupBox1 = new GroupBox();
            lblEmail = new Label();
            lblHasło = new Label();
            lblLogin = new Label();
            groupBox2 = new GroupBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // btnOk
            // 
            btnOk.Location = new Point(6, 20);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(75, 23);
            btnOk.TabIndex = 0;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnAnuluj
            // 
            btnAnuluj.Location = new Point(87, 20);
            btnAnuluj.Name = "btnAnuluj";
            btnAnuluj.Size = new Size(75, 23);
            btnAnuluj.TabIndex = 1;
            btnAnuluj.Text = "Anuluj";
            btnAnuluj.UseVisualStyleBackColor = true;
            btnAnuluj.Click += btnAnuluj_Click;
            // 
            // txtLogin
            // 
            txtLogin.Location = new Point(49, 16);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(100, 23);
            txtLogin.TabIndex = 2;
            // 
            // txtHasło
            // 
            txtHasło.Location = new Point(49, 43);
            txtHasło.Name = "txtHasło";
            txtHasło.PasswordChar = '*';
            txtHasło.Size = new Size(100, 23);
            txtHasło.TabIndex = 3;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(49, 71);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(100, 23);
            txtEmail.TabIndex = 4;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lblEmail);
            groupBox1.Controls.Add(lblHasło);
            groupBox1.Controls.Add(lblLogin);
            groupBox1.Controls.Add(txtLogin);
            groupBox1.Controls.Add(txtEmail);
            groupBox1.Controls.Add(txtHasło);
            groupBox1.Location = new Point(36, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(177, 103);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(6, 74);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(36, 15);
            lblEmail.TabIndex = 7;
            lblEmail.Text = "Email";
            // 
            // lblHasło
            // 
            lblHasło.AutoSize = true;
            lblHasło.Location = new Point(6, 46);
            lblHasło.Name = "lblHasło";
            lblHasło.Size = new Size(37, 15);
            lblHasło.TabIndex = 6;
            lblHasło.Text = "Hasło";
            // 
            // lblLogin
            // 
            lblLogin.AutoSize = true;
            lblLogin.Location = new Point(6, 19);
            lblLogin.Name = "lblLogin";
            lblLogin.Size = new Size(40, 15);
            lblLogin.TabIndex = 5;
            lblLogin.Text = "Login:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnOk);
            groupBox2.Controls.Add(btnAnuluj);
            groupBox2.Location = new Point(36, 121);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(177, 49);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(244, 188);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "RegisterForm";
            Text = "RegisterForm";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button btnOk;
        private Button btnAnuluj;
        private TextBox txtLogin;
        private TextBox txtHasło;
        private TextBox txtEmail;
        private GroupBox groupBox1;
        private Label lblEmail;
        private Label lblHasło;
        private Label lblLogin;
        private GroupBox groupBox2;
    }
}