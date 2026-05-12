using Microsoft.Extensions.DependencyInjection;
using Personal_Investment.Core.Enums;
using Personal_Investment.Core.Models;
using Personal_Investment.Data.Services;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Personal_Investment.UI.Forms;

public partial class MainWindow : Form
{
    private readonly InvestmentService _investmentService;
    private readonly AuthService _authService;
    private readonly DataExportService _exportService;

    private Timer _autoCheckTimer;
    private int _currentUserId;
    private string _zalogowanyUzytkownik;
    public bool Wylogowano { get; private set; } = false;

    // Flaga stanu widoku
    private bool _isShowingHistory = false;

    // Zmienne do kontroli limitów API
    private DateTime _lastRefreshTime = DateTime.MinValue;
    private const int RefreshCooldownSeconds = 30;

    public MainWindow(
        InvestmentService investmentService,
        AuthService authService,
        DataExportService exportService)
    {
        InitializeComponent();
        // Pod InitializeComponent()
        akcjaToolStripMenuItem.Image = imageList1.Images[1];

        // 2. Kryptowaluta
        kryptowalutaToolStripMenuItem.Image = imageList1.Images[0];

        // 3. Surowiec
        surowiecToolStripMenuItem.Image = imageList1.Images[2];

        // Opcjonalnie: upewnij się, że ikony wyświetlają się obok tekstu
        akcjaToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
        kryptowalutaToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
        surowiecToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
        listView1.DrawColumnHeader += ListView1_DrawColumnHeader;
        listView1.DrawItem += ListView1_DrawItem;
        listView1.DrawSubItem += ListView1_DrawSubItem;
        this.Resize += MainWindow_Resize;
        this.Load += async (s, e) => await InitialRefreshAsync();
        _investmentService = investmentService;
        _authService = authService;
        _exportService = exportService;

        AdjustMenuSpacing();
        SetupMenuStyles();
        SetupTimers();
    }

    private async Task InitialRefreshAsync()
    {
        try
        {
            // Opcjonalnie: pokaż kursor oczekiwania
            Cursor = Cursors.WaitCursor;

            // Pobierz dane dla portfela i listy obserwowanych
            RefreshData();
            await UpdateWatchlist();
        }
        catch (Exception ex)
        {
            // Logowanie błędu, jeśli np. brak internetu przy starcie
            Console.WriteLine($"Błąd inicjalizacji danych: {ex.Message}");
        }
        finally
        {
            Cursor = Cursors.Default;
        }
    }
    private void AdjustMenuSpacing()
    {
        if (menuStrip2 == null || spacerLeft == null || spacerRight == null) return;

        // 1. Szerokość całego paska menu
        int menuWidth = menuStrip2.DisplayRectangle.Width;

        // 2. Sumujemy szerokości poszczególnych grup (bez rozpórek)
        int leftGroupWidth = inwestycjePersonalneToolStripMenuItem.Width + sprzedajToolStripMenuItem.Width;
        int centerGroupWidth = generujRaportToolStripMenuItem.Width + eksportujDaneToolStripMenuItem.Width + importujDaneToolStripMenuItem.Width;
        int rightGroupWidth = wylogujToolStripMenuItem1.Width + UsunKontoToolStripMenuItem.Width;

        // Dodajemy marginesy (WinForms dodaje standardowo kilka pikseli między elementami)
        int totalItemsWidth = leftGroupWidth + centerGroupWidth + rightGroupWidth + 40;

        // 3. Obliczamy wolne miejsce
        int freeSpace = menuWidth - totalItemsWidth;

        if (freeSpace > 0)
        {
            // Wyłączamy AutoSize, żebyśmy mogli sami ustawić Width
            spacerLeft.AutoSize = false;
            spacerRight.AutoSize = false;

            // Klucz do sukcesu:
            // spacerLeft musi odsunąć grupę środkową na sam środek okna.
            // Połowa okna minus połowa szerokości środkowej grupy minus szerokość lewej grupy.
            int leftSpacerWidth = (menuWidth / 2) - (centerGroupWidth / 2) - leftGroupWidth;

            // spacerRight wypełnia resztę, wypychając ostatnią grupę do prawej.
            int rightSpacerWidth = menuWidth - leftSpacerWidth - leftGroupWidth - centerGroupWidth - rightGroupWidth - 20;

            spacerLeft.Width = Math.Max(10, leftSpacerWidth);
            spacerRight.Width = Math.Max(10, rightSpacerWidth);
        }
    }

    private void ListView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
    {
        // Rysujemy ciemne tło nagłówka
        using (var backBrush = new SolidBrush(Color.FromArgb(15, 15, 15))) // Bardzo ciemny fiolet/czarny
        {
            e.Graphics.FillRectangle(backBrush, e.Bounds);
        }

        // Rysujemy biały tekst nagłówka (wyśrodkowany)
        TextRenderer.DrawText(e.Graphics, e.Header.Text, e.Font, e.Bounds, Color.White, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
    }

    private void ListView1_DrawItem(object sender, DrawListViewItemEventArgs e)
    {
        // To musi być puste lub obsługiwać selekcję, ale domyślnie zostawiamy e.DrawDefault = true;
        e.DrawDefault = true;
    }

    private void ListView1_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
    {
        // To również domyślnie, aby zachować kolory ForeColor (zielony/czerwony)
        e.DrawDefault = true;
    }

    private void SetupMenuStyles()
    {
        foreach (ToolStripMenuItem parent in menuStrip2.Items.OfType<ToolStripMenuItem>())
        {
            parent.Padding = new Padding(15, 10, 15, 10);
            parent.ForeColor = Color.White;
            parent.Margin = new Padding(10, 0, 0, 0);

            foreach (ToolStripItem subItem in parent.DropDownItems)
            {
                subItem.BackColor = Color.FromArgb(25, 25, 35);
                subItem.ForeColor = Color.White;
                subItem.Padding = new Padding(6, 7, 6, 7);
                subItem.MouseEnter += (s, e) => { menuStrip2.Cursor = Cursors.Hand; };
                subItem.MouseLeave += (s, e) => { menuStrip2.Cursor = Cursors.Default; };
            }
        }
    }

    private void SetupTimers()
    {
        // Jeden timer do automatycznego sprawdzania cen (raz na 10 minut)
        _autoCheckTimer = new Timer();
        _autoCheckTimer.Interval = 10 * 60 * 1000;
        _autoCheckTimer.Tick += async (s, e) =>
        {
            var alerts = await _investmentService.RunAutomaticCheckAsync(_currentUserId);
            if (alerts.Any())
            {
                MessageBox.Show(string.Join(Environment.NewLine, alerts), "Automatyczna Sprzedaż");
            }
            RefreshData();
        };
        _autoCheckTimer.Start();
    }

    public void SetUser(string username, int userId)
    {
        _zalogowanyUzytkownik = username;
        _currentUserId = userId;
        labelWelcome.Text = $"Cześć, {username}!";
        checkBoxTrybTestowy.Visible = username.ToLower() == "admin";

        RefreshData();
    }

    // --- LOGIKA ODŚWIEŻANIA DANYCH ---

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
                var investments = await _investmentService.GetActiveInvestmentsAsync(_currentUserId);
                var currentPrices = new Dictionary<int, decimal>();

                foreach (var inv in investments)
                {
                    decimal? price;

                    // LOGIKA TRYBU TESTOWEGO:
                    if (checkBoxTrybTestowy.Checked && !string.IsNullOrWhiteSpace(textBoxAktualnaCenaTest.Text))
                    {
                        // Jeśli testujemy, parsujemy cenę z TextBoxa
                        if (decimal.TryParse(textBoxAktualnaCenaTest.Text, out decimal testPrice))
                        {
                            price = testPrice;
                        }
                        else
                        {
                            price = await _investmentService.GetLatestPriceAsync(inv.Name, inv.Type?.Name);
                        }
                    }
                    else
                    {
                        // Jeśli nie testujemy, normalnie pobieramy z API
                        price = await _investmentService.GetLatestPriceAsync(inv.Name, inv.Type?.Name);
                    }

                    if (price.HasValue)
                    {
                        currentPrices[inv.Id] = price.Value;
                        inv.CurrentPrice = price.Value;
                    }
                }

                var summary = await _investmentService.GetAccountSummaryAsync(_currentUserId, currentPrices);

                PopulateListView(investments);
                UpdateSummaryLabels(summary);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd odświeżania: {ex.Message}");
        }
    }

    private async void btnOdswiez_Click(object sender, EventArgs e)
    {
        var secondsPassed = (DateTime.Now - _lastRefreshTime).TotalSeconds;
        if (secondsPassed < RefreshCooldownSeconds)
        {
            int wait = RefreshCooldownSeconds - (int)secondsPassed;
            MessageBox.Show($"Zwolnij! Spróbuj za {wait}s.", "Ograniczenie");
            return;
        }

        try
        {
            Cursor = Cursors.WaitCursor;
            await PerformFullRefreshAsync(showErrors: true);
        }
        finally
        {
            Cursor = Cursors.Default;
        }
    }

    private void checkBoxTrybTestowy_CheckedChanged(object sender, EventArgs e)
    {
        bool isChecked = checkBoxTrybTestowy.Checked;
        // Te kontrolki muszą istnieć w Designerze, żeby to zadziałało:
        if (labelTestPrice != null) labelTestPrice.Visible = isChecked;
        if (textBoxAktualnaCenaTest != null) textBoxAktualnaCenaTest.Visible = isChecked;
    }

    // --- METODY POMOCNICZE UI ---

    private void PopulateListView(List<Investment> investments)
    {
        listView1.BeginUpdate();
        SetupActiveColumns();
        listView1.Items.Clear();

        // Zamieniamy na pętlę for, aby mieć indeks (i) do efektu zebry
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
            decimal profitPercent = inv.BuyPrice != 0 ? (inv.CurrentPrice - inv.BuyPrice) / inv.BuyPrice : 0;

            string profitText = $"{(profitLoss >= 0 ? "+" : "")}{profitLoss:F2} USD ({profitPercent:P2})";
            var subItem = item.SubItems.Add(profitText);

            // Ustawienie ikon na podstawie typu
            item.ImageIndex = inv.Type?.Name switch
            {
                "Kryptowaluty" => 0,
                "Akcje" => 1,
                "Surowce" => 3,
                _ => 2
            };

            // --- STYLIZACJA MODERN DARK MODE ---

            // 1. Najpierw ustal kolor tła dla tego wiersza
            Color rowBackColor = (i % 2 == 0)
                ? Color.FromArgb(18, 18, 18)  // Główne tło
                : Color.FromArgb(25, 20, 35); // Subtelny fiolet

            // 2. Wyłączamy dziedziczenie stylów, bo chcemy kolorować tekst Zysku/Straty niezależnie
            item.UseItemStyleForSubItems = false;

            // 3. PRZECHODZIMY PRZEZ WSZYSTKIE KOMÓRKI (SubItems) i ustawiamy im tło oraz kolor czcionki
            foreach (ListViewItem.ListViewSubItem sub in item.SubItems)
            {
                sub.BackColor = rowBackColor;
                sub.ForeColor = Color.White; // Domyślny kolor tekstu dla wszystkich komórek
            }

            // 4. Nadpisujemy kolor TEKSTU (nie tła!) tylko dla ostatniej komórki (Zysk/Strata)
            subItem.ForeColor = profitLoss >= 0 ? Color.LimeGreen : Color.Tomato; // Tomato jest czytelniejsze na ciemnym tle

            item.Tag = inv.Id;
            listView1.Items.Add(item);
        }

        listView1.EndUpdate();
    }

    private async Task UpdateWatchlist()
    {
        listViewWatchlist.Items.Clear();
        var trackedTickers = await _investmentService.GetWatchlistAsync();

        foreach (var item in trackedTickers)
        {
            // Zakładamy, że InvestmentService zwraca teraz obiekt MarketData { Price, PercentChange }
            var data = await _investmentService.GetMarketDataAsync(item.Ticker, "Akcje");

            if (data == null) continue;

            // 1. Nazwa (Ticker)
            var lvItem = new ListViewItem(item.Ticker);
            lvItem.UseItemStyleForSubItems = false; // <-- TO JEST KLUCZOWA LINIA
            lvItem.Font = new Font(listViewWatchlist.Font, FontStyle.Bold);
            lvItem.ForeColor = Color.White;

            // 2. Cena Aktualna
            lvItem.SubItems.Add($"{data.Price:N2} USD");

            // 3. Zmiana procentowa
            string sign = data.PercentChange >= 0 ? "+" : "";
            var subChange = lvItem.SubItems.Add($"{sign}{data.PercentChange:N2}%");

            // --- LOGIKA KOLOROWANIA ---
            if (data.PercentChange > 0)
            {
                subChange.ForeColor = Color.Lime; // Soczysty zielony dla wzrostów
            }
            else if (data.PercentChange < 0)
            {
                subChange.ForeColor = Color.FromArgb(255, 80, 80); // Jasny czerwony (lepiej widoczny na czarnym)
            }
            else
            {
                subChange.ForeColor = Color.Gray;
            }

            listViewWatchlist.Items.Add(lvItem);
        }

        // Korekta szerokości kolumn, żeby nic nie było ucięte (jak na obrazku image_e09e7a.png)
        if (listViewWatchlist.Columns.Count >= 3)
        {
            listViewWatchlist.Columns[0].Width = 60;
            listViewWatchlist.Columns[1].Width = 100;
            listViewWatchlist.Columns[2].Width = 80;
        }
    }
    private void PopulateHistoryListView(List<Investment> soldInvestments)
    {
        listView1.BeginUpdate();
        SetupHistoryColumns();
        listView1.Items.Clear();

        // Używamy for, żeby mieć 'i' do zebry
        for (int i = 0; i < soldInvestments.Count; i++)
        {
            var inv = soldInvestments[i];
            var sale = inv.ReturnsHistories.OrderByDescending(r => r.Date).FirstOrDefault();

            // Tutaj obliczasz zysk - używamy nazwy 'profit'
            decimal profit = (sale?.Value ?? 0) * inv.NumberOfShares - (inv.BuyPrice * inv.NumberOfShares);

            var item = new ListViewItem(inv.Name);
            item.SubItems.Add($"{inv.NumberOfShares}");
            item.SubItems.Add($"{inv.BuyPrice:F2}");
            item.SubItems.Add(inv.DateOfInvestment.ToShortDateString());
            item.SubItems.Add(sale?.Date.ToShortDateString() ?? "-");
            item.SubItems.Add($"{sale?.Value:F2}");

            // PRZYPISANIE DO subItem, żeby nie było błędu "podkreślenia na czerwono"
            var subItem = item.SubItems.Add($"{profit:F2} USD");

            item.Tag = inv.Id;
            item.ImageIndex = inv.Type?.Name switch
            {
                "Kryptowaluty" => 0,
                "Akcje" => 1,
                "Surowce" => 3,
                _ => 2
            };

            // --- STYLIZACJA MODERN DARK MODE ---

            Color rowBackColor = (i % 2 == 0)
                ? Color.FromArgb(18, 18, 18)
                : Color.FromArgb(25, 20, 35);

            item.UseItemStyleForSubItems = false;

            foreach (ListViewItem.ListViewSubItem sub in item.SubItems)
            {
                sub.BackColor = rowBackColor;
                sub.ForeColor = Color.White;
            }

            // Używamy zmiennej 'profit', którą obliczyłeś wyżej
            subItem.ForeColor = profit >= 0 ? Color.LimeGreen : Color.Tomato;

            listView1.Items.Add(item);
        }
        listView1.EndUpdate();
    }

    private void UpdateSummaryLabels(AccountSummary summary)
    {
        labelBilans.Text = $"Bilans konta: {(summary.TotalChange >= 0 ? "+" : "")}{summary.TotalChange:F2} USD";
        labelBilans.ForeColor = summary.TotalChange >= 0 ? Color.LightGreen : Color.Red;

        labelBilansAktualny.Text = $"Bilans aktualny: {(summary.CurrentBalance >= 0 ? "+" : "")}{summary.CurrentBalance:F2} USD";
        labelBilansAktualny.ForeColor = summary.CurrentBalance >= 0 ? Color.LightGreen : Color.Red;
    }

    private void SetupActiveColumns()
    {
        listView1.Columns.Clear();

        // Pobieramy szerokość bez paska przewijania (dla bezpieczeństwa)
        int totalWidth = listView1.ClientSize.Width;

        // Definiujemy szerokości jako ułamki całości
        listView1.Columns.Add("Nazwa", (int)(totalWidth * 0.15), HorizontalAlignment.Left);
        listView1.Columns.Add("Ilość", (int)(totalWidth * 0.10), HorizontalAlignment.Right);
        listView1.Columns.Add("Cena Zakupu", (int)(totalWidth * 0.15), HorizontalAlignment.Right);
        listView1.Columns.Add("Data", (int)(totalWidth * 0.12), HorizontalAlignment.Right);
        listView1.Columns.Add("Cel", (int)(totalWidth * 0.08), HorizontalAlignment.Right);
        listView1.Columns.Add("Stop Loss", (int)(totalWidth * 0.10), HorizontalAlignment.Right);
        listView1.Columns.Add("Typ", (int)(totalWidth * 0.10), HorizontalAlignment.Center);

        // Ostatnia kolumna bierze "resztę" (magiczne -2)
        listView1.Columns.Add("Zysk/Strata", -2, HorizontalAlignment.Right);
    }

    private void SetupHistoryColumns()
    {
        listView1.Columns.Clear();

        // Pobieramy szerokość wnętrza kontrolki
        int totalWidth = listView1.ClientSize.Width;

        // Musimy mieć DOKŁADNIE tyle samo kolumn, ile dodajemy SubItems w PopulateHistoryListView
        listView1.Columns.Add("Nazwa", (int)(totalWidth * 0.15), HorizontalAlignment.Left);
        listView1.Columns.Add("Ilość", (int)(totalWidth * 0.10), HorizontalAlignment.Right);
        listView1.Columns.Add("Cena Zakupu", (int)(totalWidth * 0.14), HorizontalAlignment.Right);
        listView1.Columns.Add("Data Kupna", (int)(totalWidth * 0.12), HorizontalAlignment.Right);

        // Zmieniamy nagłówki, bo w historii nie ma "Celu" ani "Stop Lossu"
        listView1.Columns.Add("Data Sprzedaży", (int)(totalWidth * 0.12), HorizontalAlignment.Right);
        listView1.Columns.Add("Cena Sprzedaży", (int)(totalWidth * 0.14), HorizontalAlignment.Right);

        // Ostatnia kolumna (Zysk/Strata)
        listView1.Columns.Add("Zysk/Strata całkowity", -2, HorizontalAlignment.Right);
    }

    // --- ZDARZENIA MENU I PRZYCISKÓW ---

    private void btnAktualne_Click(object sender, EventArgs e) { _isShowingHistory = false; RefreshData(); }
    private void btnHistoria_Click(object sender, EventArgs e) { _isShowingHistory = true; RefreshData(); }

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
        var form = Program.ServiceProvider.GetRequiredService<ReportForm>();
        if (form.ShowDialog() == DialogResult.OK)
        {
            // Używamy właściwości Ticker z Twojego kodu na image_dfb559.png
            string wybranyTicker = form.Ticker;

            var result = await _investmentService.GenerateReportAsync(_currentUserId, form.DataOd, form.DataDo, wybranyTicker);

            // Budujemy tekst nagłówka raportu
            // Sprawdzamy czy właściwość FiltrTickerAktywny z image_dfb559.png jest prawdziwa
            string naglowek = form.FiltrTickerAktywny && !string.IsNullOrEmpty(wybranyTicker)
                              ? $"Raport ({wybranyTicker})"
                              : "Raport ogólny";

            // Obliczanie procentów (opcjonalnie)
            string infoProcentowe = "";
            if (result.TotalInvested > 0)
            {
                decimal proc = (result.Profit / result.TotalInvested) * 100;
                infoProcentowe = $" ({(proc >= 0 ? "+" : "")}{proc:N2}%)";
            }

            // Ustawienie tekstu w labelu
            labelRaport.Text = $"{naglowek}: {form.DataOd:d} - {form.DataDo:d}\n" +
                               $"Zysk/Strata: {result.Profit:N2} USD{infoProcentowe}";

            // Kolorowanie zależne od wyniku
            labelRaport.ForeColor = result.Profit >= 0 ? Color.LightGreen : Color.Red;
            labelRaport.Visible = true;
        }
    }

    private async void importujDaneToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var ofd = new OpenFileDialog { Filter = "JSON|*.json" };
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            if (await _exportService.ImportUserDataAsync(_currentUserId, ofd.FileName))
            {
                MessageBox.Show("Zaimportowano pomyślnie!");
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
        var form = Program.ServiceProvider.GetRequiredService<AddStockForm>();
        form.SetMode(kind, _currentUserId);
        if (form.ShowDialog() == DialogResult.OK) RefreshData();
    }

    private void wylogujToolStripMenuItem1_Click(object sender, EventArgs e) { Wylogowano = true; this.Close(); }

    private void MainWindow_Resize(object sender, EventArgs e)
    {
        AdjustMenuSpacing();
        SetupActiveColumns();
    }

    private async void btnAddWatchlist_Click(object sender, EventArgs e)
    {
        // Wyświetla okienko z prośbą o ticker
        string ticker = Microsoft.VisualBasic.Interaction.InputBox(
            "Wpisz symbol akcji lub kryptowaluty (np. BTC, AAPL):",
            "Dodaj do obserwowanych",
            "");

        if (!string.IsNullOrWhiteSpace(ticker))
        {
            // 1. Zapisujemy w bazie przez serwis, który wcześniej przygotowaliśmy
            await _investmentService.AddToWatchlistAsync(ticker.ToUpper());

            // 2. Odświeżamy listę w UI, żeby nowy element od razu się pojawił
            await UpdateWatchlist();
        }
    }

    private async Task PerformFullRefreshAsync(bool showErrors = true)
    {
        try
        {
            // UI: Przygotowanie danych wejściowych
            decimal? priceForCheck = null;
            if (checkBoxTrybTestowy.Checked && decimal.TryParse(textBoxAktualnaCenaTest.Text, out decimal val))
            {
                priceForCheck = val;
            }

            // CORE: Wywołanie logiki biznesowej
            var alerts = await _investmentService.ExecuteAutomatedCheckAndRefreshAsync(_currentUserId, priceForCheck);

            // UI: Reakcja na wyniki
            if (alerts.Any() && showErrors)
            {
                MessageBox.Show(string.Join(Environment.NewLine, alerts), "Automatyczna Sprzedaż");
            }

            // UI: Odświeżenie widoków
            RefreshData();
            await UpdateWatchlist();

            _lastRefreshTime = DateTime.Now;
        }
        catch (Exception ex)
        {
            if (showErrors) MessageBox.Show($"Błąd: {ex.Message}");
        }
    }

    private async void timerWatchList_Tick(object sender, EventArgs e)
    {
        // Wywołujemy wspólną logikę odświeżania portfela i automatu
        await PerformFullRefreshAsync(showErrors: false);
    }
}