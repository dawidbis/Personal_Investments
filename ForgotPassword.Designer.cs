namespace Personal_Investment_App
{
    partial class ForgotPassword
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
            txtEmail = new TextBox();
            btnZatwierdź = new Button();
            btnAnuluj = new Button();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // txtLogin
            // 
            txtLogin.Location = new Point(371, 174);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(125, 27);
            txtLogin.TabIndex = 0;
            txtLogin.TextChanged += txtLogin_TextChanged;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(371, 138);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(125, 27);
            txtEmail.TabIndex = 1;
            txtEmail.TextChanged += txtEmail_TextChanged;
            // 
            // btnZatwierdź
            // 
            btnZatwierdź.Location = new Point(251, 241);
            btnZatwierdź.Name = "btnZatwierdź";
            btnZatwierdź.Size = new Size(132, 29);
            btnZatwierdź.TabIndex = 2;
            btnZatwierdź.Text = "Wygeneruj Kod";
            btnZatwierdź.UseVisualStyleBackColor = true;
            btnZatwierdź.Click += btnWygenerujKod_Click;
            // 
            // btnAnuluj
            // 
            btnAnuluj.Location = new Point(402, 241);
            btnAnuluj.Name = "btnAnuluj";
            btnAnuluj.Size = new Size(94, 29);
            btnAnuluj.TabIndex = 3;
            btnAnuluj.Text = "Anuluj";
            btnAnuluj.UseVisualStyleBackColor = true;
            btnAnuluj.Click += btnAnuluj_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(238, 138);
            label1.Name = "label1";
            label1.Size = new Size(127, 20);
            label1.TabIndex = 4;
            label1.Text = "Podaj adres email";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(251, 181);
            label2.Name = "label2";
            label2.Size = new Size(84, 20);
            label2.TabIndex = 5;
            label2.Text = "Podaj login";
            label2.Click += label2_Click;
            // 
            // ForgotPassword
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnAnuluj);
            Controls.Add(btnZatwierdź);
            Controls.Add(txtEmail);
            Controls.Add(txtLogin);
            Name = "ForgotPassword";
            Text = "ForgotPassword";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtLogin;
        private TextBox txtEmail;
        private Button btnZatwierdź;
        private Button btnAnuluj;
        private Label label1;
        private Label label2;
    }
}