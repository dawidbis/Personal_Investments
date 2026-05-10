using System;
using System.Windows.Forms;

namespace Personal_Investment.UI.Forms;

public partial class ReportForm : Form
{
    // Właściwości publiczne, do których MainWindow uzyska dostęp po zamknięciu okna
    public bool FiltrTickerAktywny { get; private set; }
    public string Ticker { get; private set; } = string.Empty;
    public bool FiltrWszystkieAkcje { get; private set; }
    public DateTime DataOd { get; private set; }
    public DateTime DataDo { get; private set; }

    // Dodajemy właściwość dla wygody w MainWindow (z Twojego starego kodu wynikało, że raporty są dla sprzedanych)
    public bool FiltrSprzedane => true;

    public ReportForm()
    {
        InitializeComponent();
        InitializeCustomLogic();
    }

    private void InitializeCustomLogic()
    {
        // Ustawienia domyślne UI
        chkWszystkieAkcje.Checked = true;
        dtpDataOd.Value = DateTime.Now.AddMonths(-1); // Domyślnie ostatni miesiąc
        dtpDataDo.Value = DateTime.Now;

        // Podpięcie zdarzeń (jeśli nie zrobiłeś tego w Designerze)
        btnZastosuj.Click += BtnZastosuj_Click;
        btnAnuluj.Click += BtnAnuluj_Click;

        chkFiltrTicker.CheckedChanged += ChkFiltrTicker_CheckedChanged;
        chkWszystkieAkcje.CheckedChanged += ChkWszystkieAkcje_CheckedChanged;
    }

    private void ChkFiltrTicker_CheckedChanged(object sender, EventArgs e)
    {
        if (chkFiltrTicker.Checked)
            chkWszystkieAkcje.Checked = false;
        else if (!chkWszystkieAkcje.Checked)
            chkWszystkieAkcje.Checked = true;
    }

    private void ChkWszystkieAkcje_CheckedChanged(object sender, EventArgs e)
    {
        if (chkWszystkieAkcje.Checked)
            chkFiltrTicker.Checked = false;
        else if (!chkFiltrTicker.Checked)
            chkFiltrTicker.Checked = true;
    }

    private void BtnZastosuj_Click(object sender, EventArgs e)
    {
        if (!ValidateInputs()) return;

        // Przypisanie wartości do właściwości
        FiltrTickerAktywny = chkFiltrTicker.Checked;
        Ticker = txtTicker.Text.Trim();
        FiltrWszystkieAkcje = chkWszystkieAkcje.Checked;
        DataOd = dtpDataOd.Value.Date;
        DataDo = dtpDataDo.Value.Date;

        this.DialogResult = DialogResult.OK;
        this.Close();
    }

    private bool ValidateInputs()
    {
        if (chkFiltrTicker.Checked && string.IsNullOrWhiteSpace(txtTicker.Text))
        {
            MessageBox.Show("Musisz wpisać ticker akcji.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        if (dtpDataDo.Value.Date < dtpDataOd.Value.Date)
        {
            MessageBox.Show("Data końcowa nie może być wcześniejsza niż początkowa.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        return true;
    }

    private void BtnAnuluj_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.Cancel;
        this.Close();
    }
}