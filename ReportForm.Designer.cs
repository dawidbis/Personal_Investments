namespace Personal_Investment_App
{
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
            this.components = new System.ComponentModel.Container();

            this.lblOpisFiltrTicker = new Label();
            this.chkFiltrTicker = new CheckBox();
            this.txtTicker = new TextBox();

            this.lblOpisFiltrWszystkieAkcje = new Label();
            this.chkWszystkieAkcje = new CheckBox();

            this.lblOpisZakresCzasu = new Label();
            this.dtpDataOd = new DateTimePicker();
            this.dtpDataDo = new DateTimePicker();

            this.btnZastosuj = new Button();
            this.btnAnuluj = new Button();

            // 
            // Form settings
            // 
            this.ClientSize = new Size(450, 230);
            this.BackColor = Color.Black;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Opcje Raportu";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            //
            // lblOpisFiltrTicker
            //
            this.lblOpisFiltrTicker.AutoSize = true;
            this.lblOpisFiltrTicker.ForeColor = Color.White;
            this.lblOpisFiltrTicker.Location = new Point(20, 20);
            this.lblOpisFiltrTicker.Text = "Filtruj według konkretnego tickera:";

            //
            // chkFiltrTicker
            //
            this.chkFiltrTicker.ForeColor = Color.White;
            this.chkFiltrTicker.Location = new Point(340, 18);
            this.chkFiltrTicker.Size = new Size(18, 24);
            this.chkFiltrTicker.UseVisualStyleBackColor = true;

            //
            // txtTicker
            //
            this.txtTicker.BackColor = Color.FromArgb(30, 30, 30);
            this.txtTicker.BorderStyle = BorderStyle.FixedSingle;
            this.txtTicker.ForeColor = Color.White;
            this.txtTicker.Size = new Size(140, 23);
            this.txtTicker.Location = new Point(20, 45);
            this.txtTicker.Name = "txtTicker";

            //
            // lblOpisFiltrWszystkieAkcje
            //
            this.lblOpisFiltrWszystkieAkcje.AutoSize = true;
            this.lblOpisFiltrWszystkieAkcje.ForeColor = Color.White;
            this.lblOpisFiltrWszystkieAkcje.Location = new Point(20, 80);
            this.lblOpisFiltrWszystkieAkcje.Text = "Lub pokaż wszystkie inwestycje:";

            //
            // chkWszystkieAkcje
            //
            this.chkWszystkieAkcje.ForeColor = Color.White;
            this.chkWszystkieAkcje.Location = new Point(340, 78);
            this.chkWszystkieAkcje.Size = new Size(18, 24);
            this.chkWszystkieAkcje.UseVisualStyleBackColor = true;

            //
            // lblOpisZakresCzasu
            //
            this.lblOpisZakresCzasu.AutoSize = true;
            this.lblOpisZakresCzasu.ForeColor = Color.White;
            this.lblOpisZakresCzasu.Location = new Point(20, 115);
            this.lblOpisZakresCzasu.Text = "Zakres czasu:";

            //
            // dtpDataOd
            //
            this.dtpDataOd.Format = DateTimePickerFormat.Short;
            this.dtpDataOd.Location = new Point(150, 110);
            this.dtpDataOd.Size = new Size(120, 22);

            //
            // dtpDataDo
            //
            this.dtpDataDo.Format = DateTimePickerFormat.Short;
            this.dtpDataDo.Location = new Point(280, 110);
            this.dtpDataDo.Size = new Size(120, 22);

            //
            // btnZastosuj
            //
            this.btnZastosuj.Location = new Point(50, 170);
            this.btnZastosuj.Text = "OK";
            this.btnZastosuj.UseVisualStyleBackColor = false;
            this.btnZastosuj.BackColor = Color.MediumPurple;
            this.btnZastosuj.FlatAppearance.BorderSize = 0;
            this.btnZastosuj.FlatStyle = FlatStyle.Flat;
            this.btnZastosuj.ForeColor = Color.White;
            this.btnZastosuj.Size = new Size(150, 36);

            //
            // btnAnuluj
            //
            this.btnAnuluj.Location = new Point(210, 170);
            this.btnAnuluj.Text = "Anuluj";
            this.btnAnuluj.UseVisualStyleBackColor = false;
            this.btnAnuluj.BackColor = Color.MediumPurple;
            this.btnAnuluj.FlatAppearance.BorderSize = 0;
            this.btnAnuluj.FlatStyle = FlatStyle.Flat;
            this.btnAnuluj.ForeColor = Color.White;
            this.btnAnuluj.Size = new Size(150, 36);

            //
            // Add controls
            //
            this.Controls.Add(this.lblOpisFiltrTicker);
            this.Controls.Add(this.chkFiltrTicker);
            this.Controls.Add(this.txtTicker);

            this.Controls.Add(this.lblOpisFiltrWszystkieAkcje);
            this.Controls.Add(this.chkWszystkieAkcje);

            this.Controls.Add(this.lblOpisZakresCzasu);
            this.Controls.Add(this.dtpDataOd);
            this.Controls.Add(this.dtpDataDo);

            this.Controls.Add(this.btnZastosuj);
            this.Controls.Add(this.btnAnuluj);
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
}