using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personal_Investment_App
{
    public partial class ReportForm : Form
    {
        public bool FiltrAktywne { get; private set; }
        public bool FiltrSprzedane { get; private set; }

        public bool FiltrTickerAktywny { get; private set; }
        public string Ticker { get; private set; }
        public bool FiltrWszystkieAkcje { get; private set; }

        public DateTime DataOd { get; private set; }
        public DateTime DataDo { get; private set; }

        public ReportForm()
        {
            InitializeComponent();

            chkWszystkieAkcje.Checked = true;

            btnZastosuj.Click += BtnZastosuj_Click;
            btnAnuluj.Click += BtnAnuluj_Click;

            // Aby checkboxy filtr tickera i wszystkie akcje były wykluczające się
            chkFiltrTicker.CheckedChanged += ChkFiltrTicker_CheckedChanged;
            chkWszystkieAkcje.CheckedChanged += ChkWszystkieAkcje_CheckedChanged;
        }

        private void ChkFiltrTicker_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFiltrTicker.Checked)
                chkWszystkieAkcje.Checked = false;
            else if (!chkWszystkieAkcje.Checked)
                chkWszystkieAkcje.Checked = true; // jeden zawsze musi być zaznaczony
        }

        private void ChkWszystkieAkcje_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWszystkieAkcje.Checked)
                chkFiltrTicker.Checked = false;
            else if (!chkFiltrTicker.Checked)
                chkFiltrTicker.Checked = true; // jeden zawsze musi być zaznaczony
        }

        private void BtnZastosuj_Click(object sender, EventArgs e)
        {
           
            if (!chkFiltrTicker.Checked && !chkWszystkieAkcje.Checked)
            {
                MessageBox.Show("Musisz wybrać filtr: konkretny ticker lub wszystkie akcje.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (chkFiltrTicker.Checked && string.IsNullOrWhiteSpace(txtTicker.Text))
            {
                MessageBox.Show("Musisz wpisać ticker akcji jeśli zaznaczyłeś filtr po tickerze.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpDataDo.Value.Date < dtpDataOd.Value.Date)
            {
                MessageBox.Show("Data 'Do' nie może być wcześniejsza niż data 'Od'.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FiltrTickerAktywny = chkFiltrTicker.Checked;
            Ticker = txtTicker.Text.Trim();
            FiltrWszystkieAkcje = chkWszystkieAkcje.Checked;
            DataOd = dtpDataOd.Value.Date;
            DataDo = dtpDataDo.Value.Date;

            // Zamknij formę z DialogResult OK
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnAnuluj_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
