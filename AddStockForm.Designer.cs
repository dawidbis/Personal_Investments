namespace Personal_Investment_App
{
    partial class AddStockForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            buttonSave = new Button();
            textBoxName = new TextBox();
            textBoxAmount = new TextBox();
            textBoxExpectedReturn = new TextBox();
            dateTimePicker = new DateTimePicker();
            textBoxNotes = new TextBox();
            lblName = new Label();
            lblAmount = new Label();
            lblExpectedReturn = new Label();
            lblDate = new Label();
            lblNotes = new Label();
            txtStopLoss = new TextBox();
            label1 = new Label();
            btnCenaAkcji = new Button();
            textBoxMockPrice = new TextBox();
            labelMockPrice = new Label();
            SuspendLayout();
            // 
            // buttonSave
            // 
            buttonSave.BackColor = Color.MediumPurple;
            buttonSave.FlatAppearance.BorderSize = 0;
            buttonSave.FlatStyle = FlatStyle.Flat;
            buttonSave.ForeColor = Color.White;
            buttonSave.Location = new Point(169, 221);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(200, 36);
            buttonSave.TabIndex = 5;
            buttonSave.Text = "Zapisz";
            buttonSave.UseVisualStyleBackColor = false;
            // 
            // textBoxName
            // 
            textBoxName.BackColor = Color.FromArgb(30, 30, 30);
            textBoxName.BorderStyle = BorderStyle.FixedSingle;
            textBoxName.ForeColor = Color.White;
            textBoxName.Location = new Point(169, 12);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(200, 23);
            textBoxName.TabIndex = 0;
            // 
            // textBoxAmount
            // 
            textBoxAmount.BackColor = Color.FromArgb(30, 30, 30);
            textBoxAmount.BorderStyle = BorderStyle.FixedSingle;
            textBoxAmount.ForeColor = Color.White;
            textBoxAmount.Location = new Point(169, 47);
            textBoxAmount.Name = "textBoxAmount";
            textBoxAmount.Size = new Size(200, 23);
            textBoxAmount.TabIndex = 1;
            // 
            // textBoxExpectedReturn
            // 
            textBoxExpectedReturn.BackColor = Color.FromArgb(30, 30, 30);
            textBoxExpectedReturn.BorderStyle = BorderStyle.FixedSingle;
            textBoxExpectedReturn.ForeColor = Color.White;
            textBoxExpectedReturn.Location = new Point(169, 82);
            textBoxExpectedReturn.Name = "textBoxExpectedReturn";
            textBoxExpectedReturn.Size = new Size(200, 23);
            textBoxExpectedReturn.TabIndex = 2;
            // 
            // dateTimePicker
            // 
            dateTimePicker.BackColor = Color.FromArgb(30, 30, 30);
            dateTimePicker.CalendarForeColor = Color.White;
            dateTimePicker.CalendarMonthBackground = Color.FromArgb(30, 30, 30);
            dateTimePicker.CalendarTitleBackColor = Color.MediumPurple;
            dateTimePicker.CalendarTitleForeColor = Color.White;
            dateTimePicker.CalendarTrailingForeColor = Color.Gray;
            dateTimePicker.ForeColor = Color.White;
            dateTimePicker.Location = new Point(169, 154);
            dateTimePicker.Name = "dateTimePicker";
            dateTimePicker.Size = new Size(200, 23);
            dateTimePicker.TabIndex = 3;
            // 
            // textBoxNotes
            // 
            textBoxNotes.BackColor = Color.FromArgb(30, 30, 30);
            textBoxNotes.BorderStyle = BorderStyle.FixedSingle;
            textBoxNotes.ForeColor = Color.White;
            textBoxNotes.Location = new Point(169, 192);
            textBoxNotes.Name = "textBoxNotes";
            textBoxNotes.Size = new Size(200, 23);
            textBoxNotes.TabIndex = 4;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.ForeColor = Color.White;
            lblName.Location = new Point(40, 12);
            lblName.Name = "lblName";
            lblName.Size = new Size(72, 15);
            lblName.TabIndex = 0;
            lblName.Text = "Nazwa akcji:";
            // 
            // lblAmount
            // 
            lblAmount.AutoSize = true;
            lblAmount.ForeColor = Color.White;
            lblAmount.Location = new Point(40, 47);
            lblAmount.Name = "lblAmount";
            lblAmount.Size = new Size(70, 15);
            lblAmount.TabIndex = 1;
            lblAmount.Text = "Liczba akcji:";
            // 
            // lblExpectedReturn
            // 
            lblExpectedReturn.AutoSize = true;
            lblExpectedReturn.ForeColor = Color.White;
            lblExpectedReturn.Location = new Point(40, 84);
            lblExpectedReturn.Name = "lblExpectedReturn";
            lblExpectedReturn.Size = new Size(126, 15);
            lblExpectedReturn.TabIndex = 2;
            lblExpectedReturn.Text = "Oczekiwany zwrot (%):";
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.ForeColor = Color.White;
            lblDate.Location = new Point(40, 154);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(89, 15);
            lblDate.TabIndex = 3;
            lblDate.Text = "Data inwestycji:";
            // 
            // lblNotes
            // 
            lblNotes.AutoSize = true;
            lblNotes.ForeColor = Color.White;
            lblNotes.Location = new Point(40, 192);
            lblNotes.Name = "lblNotes";
            lblNotes.Size = new Size(49, 15);
            lblNotes.TabIndex = 4;
            lblNotes.Text = "Notatki:";
            // 
            // txtStopLoss
            // 
            txtStopLoss.BackColor = Color.FromArgb(30, 30, 30);
            txtStopLoss.BorderStyle = BorderStyle.FixedSingle;
            txtStopLoss.ForeColor = Color.White;
            txtStopLoss.Location = new Point(169, 117);
            txtStopLoss.Name = "txtStopLoss";
            txtStopLoss.Size = new Size(200, 23);
            txtStopLoss.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.Window;
            label1.Location = new Point(40, 117);
            label1.Name = "label1";
            label1.Size = new Size(78, 15);
            label1.TabIndex = 7;
            label1.Text = "Stop Loss (%)";
            // 
            // btnCenaAkcji
            // 
            btnCenaAkcji.BackColor = Color.MediumPurple;
            btnCenaAkcji.FlatAppearance.BorderSize = 0;
            btnCenaAkcji.FlatStyle = FlatStyle.Flat;
            btnCenaAkcji.ForeColor = Color.White;
            btnCenaAkcji.Location = new Point(40, 221);
            btnCenaAkcji.Name = "btnCenaAkcji";
            btnCenaAkcji.Size = new Size(123, 36);
            btnCenaAkcji.TabIndex = 5;
            btnCenaAkcji.Text = "Sprawdź Cenę Akcji";
            btnCenaAkcji.UseVisualStyleBackColor = false;
            btnCenaAkcji.Click += btnCenaAkcji_ClickAsync;
            //
            // labelMockPrice
            // 
            labelMockPrice.AutoSize = true;
            labelMockPrice.ForeColor = SystemColors.Window;
            labelMockPrice.Location = new Point(40, 270);
            labelMockPrice.Name = "labelMockPrice";
            labelMockPrice.Size = new Size(90, 15);
            labelMockPrice.Text = "Cena testowa:";
            // 
            // textBoxMockPrice
            // 
            textBoxMockPrice.BackColor = Color.FromArgb(30, 30, 30);
            textBoxMockPrice.BorderStyle = BorderStyle.FixedSingle;
            textBoxMockPrice.ForeColor = Color.White;
            textBoxMockPrice.Location = new Point(169, 270);
            textBoxMockPrice.Name = "textBoxMockPrice";
            textBoxMockPrice.Size = new Size(200, 23);
            // 
            // AddStockForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(420, 320);
            Controls.Add(btnCenaAkcji);
            Controls.Add(label1);
            Controls.Add(txtStopLoss);
            Controls.Add(lblName);
            Controls.Add(lblAmount);
            Controls.Add(lblExpectedReturn);
            Controls.Add(lblDate);
            Controls.Add(lblNotes);
            Controls.Add(textBoxNotes);
            Controls.Add(dateTimePicker);
            Controls.Add(textBoxExpectedReturn);
            Controls.Add(textBoxAmount);
            Controls.Add(textBoxName);
            Controls.Add(buttonSave);
            Controls.Add(labelMockPrice);
            Controls.Add(textBoxMockPrice);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "AddStockForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Dodaj akcję";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonSave;
        private TextBox textBoxName;
        private TextBox textBoxAmount;
        private TextBox textBoxExpectedReturn;
        private DateTimePicker dateTimePicker;
        private TextBox textBoxNotes;

        private Label lblName;
        private Label lblAmount;
        private Label lblExpectedReturn;
        private Label lblDate;
        private Label lblNotes;
        private TextBox txtStopLoss;
        private Label label1;
        private Button btnCenaAkcji;
        private TextBox textBoxMockPrice;
        private Label labelMockPrice;

    }
}