namespace Personal_Investment.UI.Forms;
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddStockForm));
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
        SuspendLayout();
        // 
        // buttonSave
        // 
        buttonSave.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        buttonSave.BackColor = Color.MediumPurple;
        buttonSave.FlatAppearance.BorderSize = 0;
        buttonSave.FlatStyle = FlatStyle.Flat;
        buttonSave.ForeColor = Color.White;
        buttonSave.Location = new Point(229, 295);
        buttonSave.Margin = new Padding(3, 4, 3, 4);
        buttonSave.Name = "buttonSave";
        buttonSave.Size = new Size(309, 48);
        buttonSave.TabIndex = 5;
        buttonSave.Text = "Zapisz";
        buttonSave.UseVisualStyleBackColor = false;
        buttonSave.Click += buttonSave_Click;
        // 
        // textBoxName
        // 
        textBoxName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        textBoxName.BackColor = Color.FromArgb(30, 30, 30);
        textBoxName.BorderStyle = BorderStyle.FixedSingle;
        textBoxName.ForeColor = Color.White;
        textBoxName.Location = new Point(229, 16);
        textBoxName.Margin = new Padding(3, 4, 3, 4);
        textBoxName.Name = "textBoxName";
        textBoxName.Size = new Size(309, 27);
        textBoxName.TabIndex = 0;
        // 
        // textBoxAmount
        // 
        textBoxAmount.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        textBoxAmount.BackColor = Color.FromArgb(30, 30, 30);
        textBoxAmount.BorderStyle = BorderStyle.FixedSingle;
        textBoxAmount.ForeColor = Color.White;
        textBoxAmount.Location = new Point(229, 63);
        textBoxAmount.Margin = new Padding(3, 4, 3, 4);
        textBoxAmount.Name = "textBoxAmount";
        textBoxAmount.Size = new Size(309, 27);
        textBoxAmount.TabIndex = 1;
        // 
        // textBoxExpectedReturn
        // 
        textBoxExpectedReturn.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        textBoxExpectedReturn.BackColor = Color.FromArgb(30, 30, 30);
        textBoxExpectedReturn.BorderStyle = BorderStyle.FixedSingle;
        textBoxExpectedReturn.ForeColor = Color.White;
        textBoxExpectedReturn.Location = new Point(229, 110);
        textBoxExpectedReturn.Margin = new Padding(3, 4, 3, 4);
        textBoxExpectedReturn.Name = "textBoxExpectedReturn";
        textBoxExpectedReturn.Size = new Size(309, 27);
        textBoxExpectedReturn.TabIndex = 2;
        // 
        // dateTimePicker
        // 
        dateTimePicker.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        dateTimePicker.BackColor = Color.FromArgb(30, 30, 30);
        dateTimePicker.CalendarForeColor = Color.White;
        dateTimePicker.CalendarMonthBackground = Color.FromArgb(30, 30, 30);
        dateTimePicker.CalendarTitleBackColor = Color.MediumPurple;
        dateTimePicker.CalendarTitleForeColor = Color.White;
        dateTimePicker.CalendarTrailingForeColor = Color.Gray;
        dateTimePicker.ForeColor = Color.White;
        dateTimePicker.Location = new Point(229, 205);
        dateTimePicker.Margin = new Padding(3, 4, 3, 4);
        dateTimePicker.Name = "dateTimePicker";
        dateTimePicker.Size = new Size(309, 27);
        dateTimePicker.TabIndex = 3;
        // 
        // textBoxNotes
        // 
        textBoxNotes.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        textBoxNotes.BackColor = Color.FromArgb(30, 30, 30);
        textBoxNotes.BorderStyle = BorderStyle.FixedSingle;
        textBoxNotes.ForeColor = Color.White;
        textBoxNotes.Location = new Point(229, 249);
        textBoxNotes.Margin = new Padding(3, 4, 3, 4);
        textBoxNotes.Name = "textBoxNotes";
        textBoxNotes.Size = new Size(309, 27);
        textBoxNotes.TabIndex = 4;
        // 
        // lblName
        // 
        lblName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        lblName.AutoSize = true;
        lblName.ForeColor = Color.White;
        lblName.Location = new Point(66, 16);
        lblName.Name = "lblName";
        lblName.Size = new Size(91, 20);
        lblName.TabIndex = 0;
        lblName.Text = "Nazwa akcji:";
        // 
        // lblAmount
        // 
        lblAmount.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        lblAmount.AutoSize = true;
        lblAmount.ForeColor = Color.White;
        lblAmount.Location = new Point(66, 63);
        lblAmount.Name = "lblAmount";
        lblAmount.Size = new Size(88, 20);
        lblAmount.TabIndex = 1;
        lblAmount.Text = "Liczba akcji:";
        // 
        // lblExpectedReturn
        // 
        lblExpectedReturn.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        lblExpectedReturn.AutoSize = true;
        lblExpectedReturn.ForeColor = Color.White;
        lblExpectedReturn.Location = new Point(66, 112);
        lblExpectedReturn.Name = "lblExpectedReturn";
        lblExpectedReturn.Size = new Size(157, 20);
        lblExpectedReturn.TabIndex = 2;
        lblExpectedReturn.Text = "Oczekiwany zwrot (%):";
        // 
        // lblDate
        // 
        lblDate.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        lblDate.AutoSize = true;
        lblDate.ForeColor = Color.White;
        lblDate.Location = new Point(66, 205);
        lblDate.Name = "lblDate";
        lblDate.Size = new Size(112, 20);
        lblDate.TabIndex = 3;
        lblDate.Text = "Data inwestycji:";
        // 
        // lblNotes
        // 
        lblNotes.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        lblNotes.AutoSize = true;
        lblNotes.ForeColor = Color.White;
        lblNotes.Location = new Point(66, 256);
        lblNotes.Name = "lblNotes";
        lblNotes.Size = new Size(61, 20);
        lblNotes.TabIndex = 4;
        lblNotes.Text = "Notatki:";
        // 
        // txtStopLoss
        // 
        txtStopLoss.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtStopLoss.BackColor = Color.FromArgb(30, 30, 30);
        txtStopLoss.BorderStyle = BorderStyle.FixedSingle;
        txtStopLoss.ForeColor = Color.White;
        txtStopLoss.Location = new Point(229, 156);
        txtStopLoss.Margin = new Padding(3, 4, 3, 4);
        txtStopLoss.Name = "txtStopLoss";
        txtStopLoss.Size = new Size(309, 27);
        txtStopLoss.TabIndex = 6;
        // 
        // label1
        // 
        label1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        label1.AutoSize = true;
        label1.ForeColor = SystemColors.Window;
        label1.Location = new Point(66, 156);
        label1.Name = "label1";
        label1.Size = new Size(98, 20);
        label1.TabIndex = 7;
        label1.Text = "Stop Loss (%)";
        // 
        // btnCenaAkcji
        // 
        btnCenaAkcji.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        btnCenaAkcji.BackColor = Color.MediumPurple;
        btnCenaAkcji.FlatAppearance.BorderSize = 0;
        btnCenaAkcji.FlatStyle = FlatStyle.Flat;
        btnCenaAkcji.ForeColor = Color.White;
        btnCenaAkcji.Location = new Point(66, 295);
        btnCenaAkcji.Margin = new Padding(3, 4, 3, 4);
        btnCenaAkcji.Name = "btnCenaAkcji";
        btnCenaAkcji.Size = new Size(157, 48);
        btnCenaAkcji.TabIndex = 5;
        btnCenaAkcji.Text = "Sprawdź Cenę Akcji";
        btnCenaAkcji.UseVisualStyleBackColor = false;
        btnCenaAkcji.Click += btnCenaAkcji_ClickAsync;
        // 
        // AddStockForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.Black;
        ClientSize = new Size(604, 353);
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
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Icon = (Icon)resources.GetObject("$this.Icon");
        Margin = new Padding(3, 4, 3, 4);
        MaximizeBox = false;
        Name = "AddStockForm";
        Padding = new Padding(20);
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

    }