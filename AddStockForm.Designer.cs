namespace Personal_Investment_App
{
    partial class AddStockForm
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
            buttonSave = new Button();
            textBoxName = new TextBox();
            textBoxAmount = new TextBox();
            textBoxExpectedReturn = new TextBox();
            dateTimePicker = new DateTimePicker();
            textBoxNotes = new TextBox();
            SuspendLayout();
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(169, 200);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(200, 36);
            buttonSave.TabIndex = 0;
            buttonSave.Text = "Zapisz";
            buttonSave.UseVisualStyleBackColor = true;
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(169, 12);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(200, 23);
            textBoxName.TabIndex = 1;
            textBoxName.Text = "Nazwa akcji";
            // 
            // textBoxAmount
            // 
            textBoxAmount.Location = new Point(169, 41);
            textBoxAmount.Name = "textBoxAmount";
            textBoxAmount.Size = new Size(200, 23);
            textBoxAmount.TabIndex = 2;
            textBoxAmount.Text = "wartość zainwestowana";
            textBoxAmount.TextChanged += textBoxAmount_TextChanged;
            // 
            // textBoxExpectedReturn
            // 
            textBoxExpectedReturn.Location = new Point(169, 70);
            textBoxExpectedReturn.Name = "textBoxExpectedReturn";
            textBoxExpectedReturn.Size = new Size(200, 23);
            textBoxExpectedReturn.TabIndex = 3;
            textBoxExpectedReturn.Text = "wartość oczekiwana";
            textBoxExpectedReturn.TextChanged += textBoxExpectedReturn_TextChanged;
            // 
            // dateTimePicker
            // 
            dateTimePicker.Location = new Point(169, 99);
            dateTimePicker.Name = "dateTimePicker";
            dateTimePicker.Size = new Size(200, 23);
            dateTimePicker.TabIndex = 4;
            // 
            // textBoxNotes
            // 
            textBoxNotes.Location = new Point(169, 128);
            textBoxNotes.Name = "textBoxNotes";
            textBoxNotes.Size = new Size(200, 23);
            textBoxNotes.TabIndex = 5;
            textBoxNotes.Text = "notatki";
            // 
            // AddStockForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(536, 271);
            Controls.Add(textBoxNotes);
            Controls.Add(dateTimePicker);
            Controls.Add(textBoxExpectedReturn);
            Controls.Add(textBoxAmount);
            Controls.Add(textBoxName);
            Controls.Add(buttonSave);
            Name = "AddStockForm";
            Text = "AddStockForm";
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
    }
}