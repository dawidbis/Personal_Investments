namespace Personal_Investment_App
{
    partial class ChangePasswordForm
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
            btnZmienHaslo = new Button();
            txtKodResetu = new TextBox();
            txtNoweHaslo = new TextBox();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // btnZmienHaslo
            // 
            btnZmienHaslo.Location = new Point(307, 293);
            btnZmienHaslo.Name = "btnZmienHaslo";
            btnZmienHaslo.Size = new Size(195, 29);
            btnZmienHaslo.TabIndex = 0;
            btnZmienHaslo.Text = "Zmien Haslo";
            btnZmienHaslo.UseVisualStyleBackColor = true;
            btnZmienHaslo.Click += btnZmienHaslo_Click;
            // 
            // txtKodResetu
            // 
            txtKodResetu.Location = new Point(307, 211);
            txtKodResetu.Name = "txtKodResetu";
            txtKodResetu.Size = new Size(195, 27);
            txtKodResetu.TabIndex = 1;
            txtKodResetu.TextChanged += btnKodResetu_TextChanged;
            // 
            // txtNoweHaslo
            // 
            txtNoweHaslo.Location = new Point(307, 244);
            txtNoweHaslo.Name = "txtNoweHaslo";
            txtNoweHaslo.PasswordChar = '*';
            txtNoweHaslo.Size = new Size(195, 27);
            txtNoweHaslo.TabIndex = 2;
            txtNoweHaslo.TextChanged += btnNoweHaslo_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(176, 211);
            label1.Name = "label1";
            label1.Size = new Size(119, 20);
            label1.TabIndex = 3;
            label1.Text = "Podaj kod resetu";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(176, 247);
            label2.Name = "label2";
            label2.Size = new Size(125, 20);
            label2.TabIndex = 4;
            label2.Text = "Podaj nowe hasło";
            // 
            // ChangePasswordForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtNoweHaslo);
            Controls.Add(txtKodResetu);
            Controls.Add(btnZmienHaslo);
            Name = "ChangePasswordForm";
            Text = "ChangePasswordForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnZmienHaslo;
        private TextBox txtKodResetu;
        private TextBox txtNoweHaslo;
        private Label label1;
        private Label label2;
    }
}