using Microsoft.Extensions.DependencyInjection;
using Personal_Investment.Core.Models;
using Personal_Investment.Data.Services;
using Personal_Investment.Core.Enums;
using Timer = System.Windows.Forms.Timer;

namespace Personal_Investment.UI.Forms;

public partial class MainWindow : Form
{
    private readonly InvestmentService _investmentService;
    private readonly AuthService _authService;
    private readonly DataExportService _exportService;
    private Timer _autoCheckTimer;
    private List<Investment> _investments = new();
    private Timer portfolioTimer;

    private int _currentUserId;
    private string _zalogowanyUzytkownik;
    public bool Wylogowano { get; private set; } = false;

    // Flaga stanu widoku - pomaga przyciskiowi "Odśwież" wiedzieć co przeładować
    private bool _isShowingHistory = false;

    public MainWindow(
        InvestmentService investmentService,
        AuthService authService,
        DataExportService exportService)
    {
        InitializeComponent();
        // Ustawienia padding i kolory dla elementów menu i podmenu
        foreach (ToolStripMenuItem parent in menuStrip2.Items.OfType<ToolStripMenuItem>())
        {
            parent.Padding = new Padding(15, 10, 15, 10);
            parent.ForeColor = Color.White;
            parent.Margin = new Padding(10, 0, 0, 0); // przesunięcie w dół'

            foreach (ToolStripItem subItem in parent.DropDownItems)
            {
                subItem.BackColor = Color.FromArgb(25, 25, 35);
                subItem.ForeColor = Color.White;
                subItem.Padding = new Padding(6, 7, 6, 7); // większy padding góra-dół
                subItem.Height = 25;
                subItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
                subItem.MouseEnter += (s, e) => { menuStrip2.Cursor = Cursors.Hand; };
                subItem.MouseLeave += (s, e) => { menuStrip2.Cursor = Cursors.Default; };
            }
        }
        _autoCheckTimer = new Timer();
        _autoCheckTimer.Interval = 10 * 60 * 1000; // 10 minut
        _autoCheckTimer.Tick += async (s, e) => {
            var alerts = await _investmentService.RunAutomaticCheckAsync(_currentUserId);
            if (alerts.Any())
            {
                MessageBox.Show(string.Join(Environment.NewLine, alerts), "Automatyczna Sprzedaż");
                RefreshData(); // Odśwież widok, bo statusy IsSold mogły się zmienić
            }
        };
        portfolioTimer = new System.Windows.Forms.Timer();
        portfolioTimer.Interval = 300000; // 5 minut (300 000 ms)
        portfolioTimer.Tick += async (s, e) => await RefreshPortfolioPricesAsync();
        portfolioTimer.Start();
        _autoCheckTimer.Start();
        _investmentService = investmentService;
        _authService = authService;
        _exportService = exportService;
    }

    public void SetUser(string username, int userId)
    {
        _zalogowanyUzytkownik = username;
        _currentUserId = userId;
        labelWelcome.Text = $"Cześć, {username}!";

        // Tryb testowy dostępny tylko dla admina (zgodnie z Twoim Designerem)
        checkBoxTrybTestowy.Visible = username.ToLower() == "admin";

        RefreshData();
    }

    // --- LOGIKA WIDOKÓW (ListView) ---

    private void SetupActiveColumns()
    {
        listView1.Columns.Clear();
        listView1.Columns.Add("Nazwa", 150);
        listView1.Columns.Add("Ilość", 80);
        listView1.Columns.Add("Cena Zakupu", 100);
        listView1.Columns.Add("Data", 100);
        listView1.Columns.Add("Cel", 80);
        listView1.Columns.Add("Stop Loss", 80);
        listView1.Columns.Add("Typ", 100);
        listView1.Columns.Add("Zysk/Strata", 130);
    }

    private void SetupHistoryColumns()
    {
        listView1.Columns.Clear();
        listView1.Columns.Add("Nazwa", 150);
        listView1.Columns.Add("Ilość", 80);
        listView1.Columns.Add("Cena Zakupu", 100);
        listView1.Columns.Add("Data Zakupu", 100);
        listView1.Columns.Add("Data Sprzedaży", 100);
        listView1.Columns.Add("Cena Sprzedaży", 100);
        listView1.Columns.Add("Zysk/Strata", 120);
    }

    private async void RefreshData()
    {
        try
        {
            if (_isShowingHistory)
            {
                var soldData = await _investmentService.GetSoldInvestmentsAsync(_currentUserId);
                PopulateHistoryListView(soldData);
            }
            else
            {
                // 1. Pobieramy listę aktywnych inwestycji
                var investments = await _investmentService.GetActiveInvestmentsAsync(_currentUserId);

                // 2. Tworzymy słownik ID -> CenaAktualna, aby serwis wiedział co liczyć
                // Dzięki temu podsumowanie użyje tych samych cen, które widzi użytkownik
                var currentPrices = investments.ToDictionary(i => i.Id, i => i.CurrentPrice);

                // 3. Pobieramy podsumowanie, przekazując słownik cen
                var summary = await _investmentService.GetAccountSummaryAsync(_currentUserId, currentPrices);

                // 4. Aktualizujemy UI
                PopulateListView(investments);
                UpdateSummaryLabels(summary);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Błąd odświeżania: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void PopulateListView(List<Investment> investments)
    {
        listView1.BeginUpdate();
        SetupActiveColumns();
        listView1.Items.Clear();

        for (int i = 0; i < investments.Count; i++)
        {
            var inv = investments[i];
            var item = new ListViewItem(inv.Name);
            item.SubItems.Add($"{inv.NumberOfShares} szt");
            item.SubItems.Add($"{inv.BuyPrice:F2} USD");
            item.SubItems.Add(inv.DateOfInvestment.ToShortDateString());
            item.SubItems.Add(inv.ExpectedReturnPercent.ToString("P0"));
            item.SubItems.Add(inv.StopLossPercent.ToString("P0"));
            item.SubItems.Add(inv.Type?.Name ?? "Brak");

            decimal profitLoss = inv.ProfitLoss;
            string profitText = (profitLoss >= 0 ? "+" : "") + profitLoss.ToString("F2") + " USD";
            var subItem = item.SubItems.Add(profitText);

            item.UseItemStyleForSubItems = false;
            subItem.ForeColor = profitLoss >= 0 ? Color.LimeGreen : Color.Red;

            // Ikony z ImageList (zgodnie z Twoim Designerem)
            item.ImageIndex = inv.Type?.Category?.Name switch
            {
                "Kryptowaluty" => 0,
                "Akcje" => 1,
                "Surowce" => 3,
                _ => 2
            };

            item.BackColor = (i % 2 == 0) ? Color.Black : Color.FromArgb(60, 0, 90);
            item.ForeColor = Color.White;
            item.Tag = inv.Id;
            listView1.Items.Add(item);
        }
        listView1.EndUpdate();
    }

    private void PopulateHistoryListView(List<Investment> soldInvestments)
    {
        listView1.BeginUpdate();
        SetupHistoryColumns();
        listView1.Items.Clear();

        for (int i = 0; i < soldInvestments.Count; i++)
        {
            var inv = soldInvestments[i];
            var sale = inv.ReturnsHistories.OrderByDescending(r => r.Date).FirstOrDefault();
            decimal profit = (sale?.Value ?? 0) * inv.NumberOfShares - (inv.BuyPrice * inv.NumberOfShares);

            var item = new ListViewItem(inv.Name);
            item.SubItems.Add($"{inv.NumberOfShares}");
            item.SubItems.Add($"{inv.BuyPrice:F2}");
            item.SubItems.Add(inv.DateOfInvestment.ToShortDateString());
            item.SubItems.Add(sale?.Date.ToShortDateString() ?? "-");
            item.SubItems.Add($"{sale?.Value:F2}");
            item.SubItems.Add($"{profit:F2} USD");

            item.BackColor = (i % 2 == 0) ? Color.Black : Color.FromArgb(60, 0, 90);
            item.ForeColor = profit >= 0 ? Color.LightGreen : Color.Red;
            item.Tag = inv.Id;
            listView1.Items.Add(item);
        }
        listView1.EndUpdate();
    }

    private void UpdateSummaryLabels(AccountSummary summary)
    {
        // Bilans Ogólny (TotalChange)
        string totalSign = summary.TotalChange >= 0 ? "+" : "";
        labelBilans.Text = $"Bilans konta: {totalSign}{summary.TotalChange:F2} USD";
        labelBilans.ForeColor = summary.TotalChange > 0 ? Color.LightGreen :
                               summary.TotalChange < 0 ? Color.Red : Color.Gold;

        // Bilans Aktualny (CurrentBalance)
        string currentSign = summary.CurrentBalance >= 0 ? "+" : "";
        labelBilansAktualny.Text = $"Bilans aktualny: {currentSign}{summary.CurrentBalance:F2} USD";
        labelBilansAktualny.ForeColor = summary.CurrentBalance > 0 ? Color.LightGreen :
                                       summary.CurrentBalance < 0 ? Color.Red : Color.Gold;
    }

    // --- OBSŁUGA ZDARZEŃ (Events) ---

    private void btnAktualne_Click(object sender, EventArgs e)
    {
        _isShowingHistory = false;
        RefreshData();
    }

    private void btnHistoria_Click(object sender, EventArgs e)
    {
        _isShowingHistory = true;
        RefreshData();
    }

    private async void btnOdswiez_Click(object sender, EventArgs e)
    {
        Cursor = Cursors.WaitCursor;
        // Jeśli tryb testowy jest włączony, można tu dodać przekazywanie ceny z textBoxAktualnaCenaTest
        await _investmentService.RunAutomaticCheckAsync(_currentUserId);
        RefreshData();
        Cursor = Cursors.Default;
    }

    private void checkBoxTrybTestowy_CheckedChanged(object sender, EventArgs e)
    {
        bool isChecked = checkBoxTrybTestowy.Checked;
        labelTestPrice.Visible = isChecked;
        textBoxAktualnaCenaTest.Visible = isChecked;
    }

    private async void sprzedajToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (listView1.SelectedItems.Count == 0 || _isShowingHistory) return;

        int invId = (int)listView1.SelectedItems[0].Tag;
        if (MessageBox.Show("Sprzedać wybraną inwestycję?", "Potwierdzenie", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
            await _investmentService.SellInvestmentAsync(invId);
            RefreshData();
        }
    }

    private async void UsunKontoToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (MessageBox.Show("Czy na pewno chcesz USUNĄĆ konto?", "OSTRZEŻENIE", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        {
            await _authService.DeleteAccountAsync(_currentUserId);
            wylogujToolStripMenuItem1_Click(sender, e);
        }
    }

    private async void generujRaportToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var form = new ReportForm();
        if (form.ShowDialog() == DialogResult.OK)
        {
            var result = await _investmentService.GenerateReportAsync(
                _currentUserId,
                form.DataOd,
                form.DataDo,
                form.FiltrTickerAktywny ? form.Ticker : null
            );

            // Wyświetlanie w labelRaport (który masz w Designerze)
            labelRaport.Text = $"Raport: {form.DataOd:d} - {form.DataDo:d}\n" +
                              $"Zainwestowano: {result.TotalInvested:F2} USD\n" +
                              $"Uzyskano: {result.TotalEarned:F2} USD\n" +
                              $"Zysk/Strata: {result.Profit:F2} USD";
            labelRaport.ForeColor = result.Profit >= 0 ? Color.LightGreen : Color.Red;
            labelRaport.Visible = true;
        }
    }

    private async void importujDaneToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var ofd = new OpenFileDialog { Filter = "JSON|*.json" };
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            var success = await _exportService.ImportUserDataAsync(_currentUserId, ofd.FileName);
            if (success)
            {
                MessageBox.Show("Dane zaimportowane pomyślnie!");
                RefreshData();
            }
        }
    }

    private async void eksportujDaneToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var sfd = new SaveFileDialog { Filter = "JSON|*.json" };
        if (sfd.ShowDialog() == DialogResult.OK)
        {
            await _exportService.ExportUserDataAsync(_currentUserId, sfd.FileName);
            MessageBox.Show("Eksport zakończony!");
        }
    }

    private void akcjaToolStripMenuItem_Click(object sender, EventArgs e) => OpenAddForm(InvestmentKind.Akcja);
    private void kryptowalutaToolStripMenuItem_Click(object sender, EventArgs e) => OpenAddForm(InvestmentKind.Kryptowaluta);
    private void surowiecToolStripMenuItem_Click(object sender, EventArgs e) => OpenAddForm(InvestmentKind.Surowiec);

    private void OpenAddForm(InvestmentKind kind)
    {
        // Pobieramy form z DI, aby miał wstrzyknięte serwisy
        var form = Program.ServiceProvider.GetRequiredService<AddStockForm>();
        form.SetMode(kind, _currentUserId); // Ustawiamy parametry
        if (form.ShowDialog() == DialogResult.OK)
        {
            RefreshData();
        }
    }

    private async Task RefreshPortfolioPricesAsync()
    {
        // Pobieramy aktualną listę inwestycji z ListView lub z bazy
        // Zakładając, że masz listę załadowanych inwestycji w polu _investments
        if (_investments == null || !_investments.Any()) return;

        try
        {
            // Zmieniamy kursor na klepsydrę
            this.UseWaitCursor = true;

            foreach (var inv in _investments.Where(i => !i.IsSold))
            {
                // Pobieramy nową cenę z serwisu (wykorzystując logikę API)
                // Używamy Ticker (inv.Name) i Typu (inv.Type.Name)
                var price = await _investmentService.GetLatestPriceAsync(inv.Name, inv.Type?.Name ?? "Akcja");

                if (price.HasValue)
                {
                    inv.CurrentPrice = price.Value;
                }

                // Małe opóźnienie, aby nie przekroczyć limitu 8 zapytań/min w darmowym API
                await Task.Delay(500);
            }

            // Odświeżamy widok listy
            RefreshListView(_investments);

            // Aktualizujemy etykiety bilansu
            RefreshData();
        }
        catch (Exception ex)
        {
            // Logujemy błąd po cichu, żeby timer nie wywalił aplikacji
            Console.WriteLine($"Błąd auto-odświeżania: {ex.Message}");
        }
        finally
        {
            this.UseWaitCursor = false;
        }
    }

    private void RefreshListView(List<Investment> investments)
    {
        listView1.Items.Clear();

        foreach (var inv in investments)
        {
            ListViewItem item = new ListViewItem(inv.Name);
            item.SubItems.Add(inv.NumberOfShares.ToString());
            item.SubItems.Add(inv.BuyPrice.ToString("N2") + " USD");
            item.SubItems.Add(inv.DateOfInvestment.ToShortDateString());
            item.SubItems.Add((inv.ExpectedReturnPercent * 100).ToString("N0") + "%");
            item.SubItems.Add((inv.StopLossPercent * 100).ToString("N0") + "%");
            item.SubItems.Add(inv.Type?.Name ?? "Akcja");

            // Obliczanie Zysku/Straty
            // ProfitLoss to właściwość [NotMapped], którą dodałeś do modelu Investment
            decimal profitLoss = inv.ProfitLoss;
            string profitText = (profitLoss >= 0 ? "+" : "") + profitLoss.ToString("N2") + " USD";

            var subItem = item.SubItems.Add(profitText);

            // Kolorowanie: Zielony dla zysku, Czerwony dla straty
            item.UseItemStyleForSubItems = false; // Pozwala na kolorowanie pojedynczych komórek
            if (profitLoss > 0) subItem.ForeColor = Color.LimeGreen;
            else if (profitLoss < 0) subItem.ForeColor = Color.Red;

            listView1.Items.Add(item);
        }
    }

    private void wylogujToolStripMenuItem1_Click(object sender, EventArgs e)
    {
        Wylogowano = true;
        this.Close();
    }
}
