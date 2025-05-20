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

            SuspendLayout();

            // 
            // Labels
            //
            lblName.AutoSize = true;
            lblName.ForeColor = Color.White;
            lblName.Location = new Point(40, 15);
            lblName.Name = "lblName";
            lblName.Size = new Size(90, 15);
            lblName.Text = "Nazwa akcji:";

            lblAmount.AutoSize = true;
            lblAmount.ForeColor = Color.White;
            lblAmount.Location = new Point(40, 50);
            lblAmount.Name = "lblAmount";
            lblAmount.Size = new Size(110, 15);
            lblAmount.Text = "Kwota zainwestowana:";

            lblExpectedReturn.AutoSize = true;
            lblExpectedReturn.ForeColor = Color.White;
            lblExpectedReturn.Location = new Point(40, 85);
            lblExpectedReturn.Name = "lblExpectedReturn";
            lblExpectedReturn.Size = new Size(130, 15);
            lblExpectedReturn.Text = "Oczekiwany zwrot (%):";

            lblDate.AutoSize = true;
            lblDate.ForeColor = Color.White;
            lblDate.Location = new Point(40, 120);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(110, 15);
            lblDate.Text = "Data inwestycji:";

            lblNotes.AutoSize = true;
            lblNotes.ForeColor = Color.White;
            lblNotes.Location = new Point(40, 155);
            lblNotes.Name = "lblNotes";
            lblNotes.Size = new Size(50, 15);
            lblNotes.Text = "Notatki:";

            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(169, 200);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(200, 36);
            buttonSave.TabIndex = 5;
            buttonSave.Text = "Zapisz";
            buttonSave.UseVisualStyleBackColor = false;
            buttonSave.BackColor = Color.MediumPurple;
            buttonSave.ForeColor = Color.White;
            buttonSave.FlatStyle = FlatStyle.Flat;
            buttonSave.FlatAppearance.BorderSize = 0;

            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(169, 12);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(200, 23);
            textBoxName.TabIndex = 0;
            textBoxName.BackColor = Color.FromArgb(30, 30, 30);
            textBoxName.ForeColor = Color.White;
            textBoxName.BorderStyle = BorderStyle.FixedSingle;

            // 
            // textBoxAmount
            // 
            textBoxAmount.Location = new Point(169, 47);
            textBoxAmount.Name = "textBoxAmount";
            textBoxAmount.Size = new Size(200, 23);
            textBoxAmount.TabIndex = 1;
            textBoxAmount.BackColor = Color.FromArgb(30, 30, 30);
            textBoxAmount.ForeColor = Color.White;
            textBoxAmount.BorderStyle = BorderStyle.FixedSingle;

            // 
            // textBoxExpectedReturn
            // 
            textBoxExpectedReturn.Location = new Point(169, 82);
            textBoxExpectedReturn.Name = "textBoxExpectedReturn";
            textBoxExpectedReturn.Size = new Size(200, 23);
            textBoxExpectedReturn.TabIndex = 2;
            textBoxExpectedReturn.BackColor = Color.FromArgb(30, 30, 30);
            textBoxExpectedReturn.ForeColor = Color.White;
            textBoxExpectedReturn.BorderStyle = BorderStyle.FixedSingle;

            // 
            // dateTimePicker
            // 
            dateTimePicker.Location = new Point(169, 117);
            dateTimePicker.Name = "dateTimePicker";
            dateTimePicker.Size = new Size(200, 23);
            dateTimePicker.TabIndex = 3;
            dateTimePicker.CalendarForeColor = Color.White;
            dateTimePicker.CalendarMonthBackground = Color.FromArgb(30, 30, 30);
            dateTimePicker.CalendarTitleBackColor = Color.MediumPurple;
            dateTimePicker.CalendarTitleForeColor = Color.White;
            dateTimePicker.CalendarTrailingForeColor = Color.Gray;
            dateTimePicker.BackColor = Color.FromArgb(30, 30, 30);
            dateTimePicker.ForeColor = Color.White;

            // 
            // textBoxNotes
            // 
            textBoxNotes.Location = new Point(169, 152);
            textBoxNotes.Name = "textBoxNotes";
            textBoxNotes.Size = new Size(200, 23);
            textBoxNotes.TabIndex = 4;
            textBoxNotes.BackColor = Color.FromArgb(30, 30, 30);
            textBoxNotes.ForeColor = Color.White;
            textBoxNotes.BorderStyle = BorderStyle.FixedSingle;

            // 
            // AddStockForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 260);
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

            Name = "AddStockForm";
            Text = "Dodaj akcję";
            BackColor = Color.Black;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;

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
    }
}