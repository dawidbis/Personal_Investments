namespace Personal_Investment.UI.Forms;
    partial class ReportForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblOpisFiltrTicker;
        private System.Windows.Forms.CheckBox chkFiltrTicker;
        private System.Windows.Forms.TextBox txtTicker;

        private System.Windows.Forms.Label lblOpisFiltrWszystkieAkcje;
        private System.Windows.Forms.CheckBox chkWszystkieAkcje;

        private System.Windows.Forms.Label lblOpisZakresCzasu;
        private System.Windows.Forms.DateTimePicker dtpDataOd;
        private System.Windows.Forms.DateTimePicker dtpDataDo;

        private System.Windows.Forms.Button btnZastosuj;
        private System.Windows.Forms.Button btnAnuluj;

    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportForm));
        lblOpisFiltrTicker = new Label();
        chkFiltrTicker = new CheckBox();
        txtTicker = new TextBox();
        lblOpisFiltrWszystkieAkcje = new Label();
        chkWszystkieAkcje = new CheckBox();
        lblOpisZakresCzasu = new Label();
        dtpDataOd = new DateTimePicker();
        dtpDataDo = new DateTimePicker();
        btnZastosuj = new Button();
        btnAnuluj = new Button();
        SuspendLayout();
        // 
        // lblOpisFiltrTicker
        // 
        lblOpisFiltrTicker.AutoSize = true;
        lblOpisFiltrTicker.ForeColor = Color.White;
        lblOpisFiltrTicker.Location = new Point(20, 20);
        lblOpisFiltrTicker.Name = "lblOpisFiltrTicker";
        lblOpisFiltrTicker.Size = new Size(237, 20);
        lblOpisFiltrTicker.TabIndex = 0;
        lblOpisFiltrTicker.Text = "Filtruj według konkretnego tickera:";
        // 
        // chkFiltrTicker
        // 
        chkFiltrTicker.ForeColor = Color.White;
        chkFiltrTicker.Location = new Point(340, 18);
        chkFiltrTicker.Name = "chkFiltrTicker";
        chkFiltrTicker.Size = new Size(18, 24);
        chkFiltrTicker.TabIndex = 1;
        chkFiltrTicker.UseVisualStyleBackColor = true;
        // 
        // txtTicker
        // 
        txtTicker.BackColor = Color.FromArgb(30, 30, 30);
        txtTicker.BorderStyle = BorderStyle.FixedSingle;
        txtTicker.ForeColor = Color.White;
        txtTicker.Location = new Point(20, 45);
        txtTicker.Name = "txtTicker";
        txtTicker.Size = new Size(140, 27);
        txtTicker.TabIndex = 2;
        // 
        // lblOpisFiltrWszystkieAkcje
        // 
        lblOpisFiltrWszystkieAkcje.AutoSize = true;
        lblOpisFiltrWszystkieAkcje.ForeColor = Color.White;
        lblOpisFiltrWszystkieAkcje.Location = new Point(20, 80);
        lblOpisFiltrWszystkieAkcje.Name = "lblOpisFiltrWszystkieAkcje";
        lblOpisFiltrWszystkieAkcje.Size = new Size(217, 20);
        lblOpisFiltrWszystkieAkcje.TabIndex = 3;
        lblOpisFiltrWszystkieAkcje.Text = "Lub pokaż wszystkie inwestycje:";
        // 
        // chkWszystkieAkcje
        // 
        chkWszystkieAkcje.ForeColor = Color.White;
        chkWszystkieAkcje.Location = new Point(340, 78);
        chkWszystkieAkcje.Name = "chkWszystkieAkcje";
        chkWszystkieAkcje.Size = new Size(18, 24);
        chkWszystkieAkcje.TabIndex = 4;
        chkWszystkieAkcje.UseVisualStyleBackColor = true;
        // 
        // lblOpisZakresCzasu
        // 
        lblOpisZakresCzasu.AutoSize = true;
        lblOpisZakresCzasu.ForeColor = Color.White;
        lblOpisZakresCzasu.Location = new Point(20, 115);
        lblOpisZakresCzasu.Name = "lblOpisZakresCzasu";
        lblOpisZakresCzasu.Size = new Size(95, 20);
        lblOpisZakresCzasu.TabIndex = 5;
        lblOpisZakresCzasu.Text = "Zakres czasu:";
        // 
        // dtpDataOd
        // 
        dtpDataOd.Format = DateTimePickerFormat.Short;
        dtpDataOd.Location = new Point(150, 110);
        dtpDataOd.Name = "dtpDataOd";
        dtpDataOd.Size = new Size(120, 27);
        dtpDataOd.TabIndex = 6;
        // 
        // dtpDataDo
        // 
        dtpDataDo.Format = DateTimePickerFormat.Short;
        dtpDataDo.Location = new Point(280, 110);
        dtpDataDo.Name = "dtpDataDo";
        dtpDataDo.Size = new Size(120, 27);
        dtpDataDo.TabIndex = 7;
        // 
        // btnZastosuj
        // 
        btnZastosuj.BackColor = Color.MediumPurple;
        btnZastosuj.FlatAppearance.BorderSize = 0;
        btnZastosuj.FlatStyle = FlatStyle.Flat;
        btnZastosuj.ForeColor = Color.White;
        btnZastosuj.Location = new Point(50, 170);
        btnZastosuj.Name = "btnZastosuj";
        btnZastosuj.Size = new Size(150, 36);
        btnZastosuj.TabIndex = 8;
        btnZastosuj.Text = "OK";
        btnZastosuj.UseVisualStyleBackColor = false;
        // 
        // btnAnuluj
        // 
        btnAnuluj.BackColor = Color.MediumPurple;
        btnAnuluj.FlatAppearance.BorderSize = 0;
        btnAnuluj.FlatStyle = FlatStyle.Flat;
        btnAnuluj.ForeColor = Color.White;
        btnAnuluj.Location = new Point(210, 170);
        btnAnuluj.Name = "btnAnuluj";
        btnAnuluj.Size = new Size(150, 36);
        btnAnuluj.TabIndex = 9;
        btnAnuluj.Text = "Anuluj";
        btnAnuluj.UseVisualStyleBackColor = false;
        // 
        // ReportForm
        // 
        BackColor = Color.Black;
        ClientSize = new Size(450, 230);
        Controls.Add(lblOpisFiltrTicker);
        Controls.Add(chkFiltrTicker);
        Controls.Add(txtTicker);
        Controls.Add(lblOpisFiltrWszystkieAkcje);
        Controls.Add(chkWszystkieAkcje);
        Controls.Add(lblOpisZakresCzasu);
        Controls.Add(dtpDataOd);
        Controls.Add(dtpDataDo);
        Controls.Add(btnZastosuj);
        Controls.Add(btnAnuluj);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Icon = (Icon)resources.GetObject("$this.Icon");
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "ReportForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Opcje Raportu";
        ResumeLayout(false);
        PerformLayout();
    }

    /// <summary>
    /// Zwolnienie zasobów
    /// </summary>
    /// <param name="disposing"></param>
    protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }