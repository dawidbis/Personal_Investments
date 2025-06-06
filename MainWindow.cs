#pragma warning disable CS0436
using DatabaseConnection;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using Polygon_api;
using ProgramLogic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Personal_Investment_App
{

    public partial class MainWindow : Form
    {
        private DatabaseManager dbManager;
        public bool Wylogowano { get; private set; } = false;
        private readonly string zalogowanyUzytkownik;
        private Timer autoCheckTimer;
        private bool IsTrybTestowy => checkBoxTrybTestowy.Checked;

        private void SetupListView(int userId)
        {
            listView1.View = View.Details;
            listView1.FullRowSelect = true;

            listView1.Columns.Clear();
            listView1.Columns.Add("Nazwa", 150);
            listView1.Columns.Add("Liczba Akcji", 100);
            listView1.Columns.Add("Cena zakupu Akcji", 100);
            listView1.Columns.Add("Data", 100);
            listView1.Columns.Add("Oczekiwany Zwrot", 140);
            listView1.Columns.Add("Stop Loss", 100);
            listView1.Columns.Add("Typ", 80);
            listView1.Columns.Add("Aktualna Cena", 140);
            listView1.Columns.Add("Aktualny Bilans", 140);

            listView1.Items.Clear();

            var inwestycje = dbManager.Investments
                .Include(i => i.Type)
                .ThenInclude(t => t.Category)
                .Where(i => i.UserId == userId && !i.IsSold)
                .ToList();

            int index = 0;
            foreach (var inv in inwestycje)
            {
                var item = new ListViewItem(inv.Name); // <- to jest pierwszy element (kolumna „Nazwa”)
                item.SubItems.Add($"{inv.NumberOfShares} szt");

                decimal? cenaZakupu = inv.BuyPrice ?? inv.MockPrice;
                item.SubItems.Add(cenaZakupu?.ToString("C") ?? "Brak");

                item.SubItems.Add(inv.DateOfInvestment.ToShortDateString());
                item.SubItems.Add(inv.ExpectedReturnPercent.ToString("P2"));
                item.SubItems.Add(inv.StopLossPercent.ToString("P2"));
                item.SubItems.Add(inv.Type?.Name ?? "Brak");
                item.SubItems.Add("...");
                item.SubItems.Add("...");

                item.ImageIndex = (inv.Type?.Category?.Name == "Akcje") ? 1 : -1;

                // Ustawiamy kolor tła zależnie od indeksu
                item.BackColor = (index % 2 == 0)
                    ? Color.Black
                    : Color.FromArgb(60, 0, 90); // fiolet ciemny

                item.ForeColor = Color.White;

                item.Tag = inv.Id;

                listView1.Items.Add(item);
                index++;
            }
        }

        public MainWindow(DatabaseManager dbManager, string zalogowanyUzytkownik)

        {
            InitializeComponent();
            this.dbManager = dbManager;
            this.zalogowanyUzytkownik = zalogowanyUzytkownik;

            labelWelcome.Text = $"Cześć, {zalogowanyUzytkownik}!";

            int? userId = dbManager.GetUserIdByUsername(zalogowanyUzytkownik);
            if (userId.HasValue)
            {
                SetupListView(userId.Value);
            }

            autoCheckTimer = new Timer();
            autoCheckTimer.Interval = 10 * 60 * 100;
            autoCheckTimer.Tick += AutoCheckTimer_Tick;
            autoCheckTimer.Start(); 
        }

        private async void AutoCheckTimer_Tick(object sender, EventArgs e)
        {
            int? userId = dbManager.GetUserIdByUsername(zalogowanyUzytkownik);
            if (userId == null)
                return;

            try
            {
                // Zbieranie cen z ListView
                var testPrices = new Dictionary<string, decimal>();
                foreach (ListViewItem item in listView1.Items)
                {
                    string symbol = item.SubItems[0].Text;

                    if (item.SubItems.Count > 7 && decimal.TryParse(item.SubItems[7].Text, out decimal parsed))
                    {
                        testPrices[symbol] = parsed;
                    }
                }

                // Przekazanie słownika do metody
                var alerts = await dbManager.CheckInvestmentsAutomaticallyAsync(
                    userId.Value,
                    IsTrybTestowy,
                    testPrices
                );

                if (alerts.Any())
                {
                    string message = string.Join(Environment.NewLine, alerts);
                    MessageBox.Show(message, "Alert inwestycyjny", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetupListView(userId.Value); // Odśwież listę
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd automatycznego sprawdzania inwestycji: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void inwestycjePersonalneToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void wylogujToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();

            var loginForm = new LoginForm(dbManager);
            loginForm.FormClosed += (s, args) => this.Close();
            loginForm.Show();
            Wylogowano = true;
            this.Close(); // Zamknij główne okno – Program.cs odpali znowu LoginForm

        }

        private void UsunKontoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Czy na pewno chcesz usunąć swoje konto? Tej operacji nie można cofnąć.",
                "Potwierdzenie usunięcia konta",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (result == DialogResult.Yes)
            {
                if (dbManager.UsunKonto(zalogowanyUzytkownik))
                {
                    MessageBox.Show("Konto zostało usunięte.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Wylogowano = true;
                    this.Close(); // wróci do LoginForm
                }
                else
                {
                    MessageBox.Show("Wystąpił błąd podczas usuwania konta.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void akcjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int? userId = dbManager.GetUserIdByUsername(zalogowanyUzytkownik);

            if (userId == null)
            {
                MessageBox.Show("Nie można znaleźć zalogowanego użytkownika w bazie.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var form = new AddStockForm(dbManager, userId.Value, IsTrybTestowy);

            form.FormClosed += (s, args) =>
            {
                var inv = form.CreatedInvestment;
                if (inv != null)
                {
                    var type = dbManager.InvestmentTypes.FirstOrDefault(t => t.Id == inv.TypeId);
                    var category = dbManager.InvestmentCategories.FirstOrDefault(c => c.Id == type.CategoryId);

                    var item = new ListViewItem();
                    item.SubItems.Add(inv.Name);
                    item.SubItems.Add($"{inv.NumberOfShares} szt");

                    decimal? cenaZakupu = IsTrybTestowy ? inv.MockPrice : inv.BuyPrice;
                    item.SubItems.Add(cenaZakupu?.ToString("C") ?? "Brak");

                    item.SubItems.Add(inv.DateOfInvestment.ToShortDateString());
                    item.SubItems.Add(inv.ExpectedReturnPercent.ToString("P2"));
                    item.SubItems.Add(inv.StopLossPercent.ToString("P2"));
                    item.SubItems.Add(type?.Name ?? "Nieznany");
                    item.SubItems.Add(category?.Name ?? "Brak");

                    listView1.Items.Add(item);
                }

                if (form.CreatedInvestment != null)
                {
                    SetupListView(userId.Value); // Odśwież listę
                }
            };

            form.Show();
        }

        private async void sprzedajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Najpierw wybierz inwestycję z listy.", "Brak wyboru", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedItem = listView1.SelectedItems[0];
            string investmentName = selectedItem.Text;
            int investmentId = (int)selectedItem.Tag;

            var confirm = MessageBox.Show(
                $"Czy na pewno chcesz sprzedać inwestycję \"{investmentName}\"?",
                "Potwierdź sprzedaż",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            decimal marketPrice;

            if (IsTrybTestowy)
            {
                var aktualnaCenaTekst = selectedItem.SubItems[7].Text;

                if (!decimal.TryParse(aktualnaCenaTekst, out marketPrice))
                {
                    MessageBox.Show("Nie można odczytać aktualnej ceny z listy.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                var latest = await FinnhubService.GetCurrentQuoteAsync(investmentName);
                //var latest = await AlphaVantageService.GetLatestClosePriceAsync(investmentName);
                if (latest == null)
                {
                    MessageBox.Show("Nie udało się pobrać aktualnej ceny z API.", "Błąd API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                marketPrice = (decimal)latest;
            }

            bool success = dbManager.SellInvestment(investmentId, marketPrice);

            if (success)
            {
                MessageBox.Show($"Inwestycja \"{investmentName}\" została sprzedana po cenie {marketPrice}.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);

                int? userId = dbManager.GetUserIdByUsername(zalogowanyUzytkownik);
                if (userId.HasValue)
                {
                    SetupListView(userId.Value);
                }
            }
            else
            {
                MessageBox.Show("Nie udało się sprzedać inwestycji.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void eksportujDaneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "JSON files (*.json)|*.json";
                    saveFileDialog.Title = "Zapisz dane inwestycji";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var userId = dbManager.GetUserIdByUsername(zalogowanyUzytkownik); // zakładamy, że currentUsername masz w klasie
                        if (userId == null)
                        {
                            MessageBox.Show("Nie znaleziono zalogowanego użytkownika.");
                            return;
                        }

                        var allInvestments = dbManager.Investments
                            .Include(i => i.Type)
                                .ThenInclude(t => t.Category)
                            .Include(i => i.ReturnsHistories)
                            .Where(i => i.UserId == userId)
                            .ToList();

                        var activeInvestments = allInvestments
                            .Where(i => !i.IsSold)
                            .Select(i => new
                            {
                                i.Id,
                                i.Name,
                                Type = i.Type?.Name,
                                Category = i.Type?.Category?.Name,
                                i.NumberOfShares,
                                i.DateOfInvestment,
                                i.ExpectedReturnPercent,
                                i.StopLossPercent,
                                i.BuyPrice,
                                i.MockPrice,
                                i.Notes,
                                i.IsSold,
                                ReturnsHistory = i.ReturnsHistories.Select(r => new
                                {
                                    r.Date,
                                    r.Value
                                })
                            });

                        var soldInvestments = allInvestments
                            .Where(i => i.IsSold)
                            .Select(i => new
                            {
                                i.Id,
                                i.Name,
                                Type = i.Type?.Name,
                                Category = i.Type?.Category?.Name,
                                i.NumberOfShares,
                                i.DateOfInvestment,
                                i.ExpectedReturnPercent,
                                i.StopLossPercent,
                                i.BuyPrice,
                                i.MockPrice,
                                i.Notes,
                                i.IsSold,
                                SaleHistory = i.ReturnsHistories.Select(r => new
                                {
                                    r.Date,
                                    r.Value
                                })
                            });

                        var exportData = new
                        {
                            User = zalogowanyUzytkownik,
                            ExportDate = DateTime.Now,
                            ActiveInvestments = activeInvestments,
                            SoldInvestments = soldInvestments
                        };

                        var options = new JsonSerializerOptions
                        {
                            WriteIndented = true,
                            ReferenceHandler = ReferenceHandler.IgnoreCycles
                        };

                        string json = JsonSerializer.Serialize(exportData, options);
                        File.WriteAllText(saveFileDialog.FileName, json);

                        MessageBox.Show("Dane zostały wyeksportowane pomyślnie.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas eksportu: " + ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void importujDaneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "JSON files (*.json)|*.json";
                    openFileDialog.Title = "Importuj dane inwestycji";

                    if (openFileDialog.ShowDialog() != DialogResult.OK)
                        return;

                    string json = File.ReadAllText(openFileDialog.FileName);

                    using JsonDocument doc = JsonDocument.Parse(json);
                    JsonElement root = doc.RootElement;

                    int? userId = dbManager.GetUserIdByUsername(zalogowanyUzytkownik);
                    if (userId == null)
                    {
                        MessageBox.Show("Zalogowany użytkownik nie istnieje.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    ImportInvestments(root, "ActiveInvestments", false, userId.Value);
                    ImportInvestments(root, "SoldInvestments", true, userId.Value);

                    MessageBox.Show("Import zakończony sukcesem.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd importu: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private decimal? GetNullableDecimal(JsonElement element, string propertyName)
        {
            if (element.TryGetProperty(propertyName, out var prop))
            {
                if (prop.ValueKind == JsonValueKind.Number)
                    return prop.GetDecimal();
                if (prop.ValueKind == JsonValueKind.Null)
                    return null;
            }
            return null;
        }

        private void ImportInvestments(JsonElement root, string propertyName, bool isSold, int userId)
        {
            if (!root.TryGetProperty(propertyName, out JsonElement investments))
                return;

            foreach (JsonElement inv in investments.EnumerateArray())
            {
                string name = inv.GetProperty("Name").GetString();
                string typeName = inv.GetProperty("Type").GetString();
                string categoryName = inv.GetProperty("Category").GetString();
                int numberOfShares = inv.GetProperty("NumberOfShares").GetInt32();
                DateTime dateOfInvestment = inv.GetProperty("DateOfInvestment").GetDateTime();
                decimal expectedReturn = GetNullableDecimal(inv, "ExpectedReturnPercent") ?? 0;
                decimal stopLoss = GetNullableDecimal(inv, "StopLossPercent") ?? 0;
                decimal? buyPrice = GetNullableDecimal(inv, "BuyPrice");
                decimal? mockPrice = GetNullableDecimal(inv, "MockPrice");
                string notes = inv.TryGetProperty("Notes", out var notesProp) && notesProp.ValueKind != JsonValueKind.Null
                ? notesProp.GetString()
                : null;

                // Znajdź lub dodaj kategorię
                var category = dbManager.InvestmentCategories.FirstOrDefault(c => c.Name == categoryName);
                if (category == null)
                {
                    category = new InvestmentCategory
                    {
                        Name = categoryName,
                        Description = ""
                    };
                    dbManager.InvestmentCategories.Add(category);
                    dbManager.SaveChanges();
                }

                // Znajdź lub dodaj typ inwestycji
                var type = dbManager.InvestmentTypes.FirstOrDefault(t => t.Name == typeName && t.CategoryId == category.Id);
                if (type == null)
                {
                    type = new InvestmentType
                    {
                        Name = typeName,
                        CategoryId = category.Id
                    };
                    dbManager.InvestmentTypes.Add(type);
                    dbManager.SaveChanges();
                }

                var investment = new Investment
                {
                    Name = name,
                    TypeId = type.Id,
                    UserId = userId,
                    NumberOfShares = numberOfShares,
                    DateOfInvestment = dateOfInvestment,
                    ExpectedReturnPercent = expectedReturn,
                    StopLossPercent= stopLoss,
                    BuyPrice = buyPrice,
                    MockPrice = mockPrice,
                    Notes = notes,
                    IsSold = isSold
                };

                dbManager.Investments.Add(investment);
                dbManager.SaveChanges();

                // Wczytaj historię zwrotów
                if (inv.TryGetProperty("ReturnsHistory", out JsonElement returnsHistory))
                {
                    foreach (JsonElement ret in returnsHistory.EnumerateArray())
                    {
                        dbManager.ReturnsHistories.Add(new ReturnsHistory
                        {
                            InvestmentId = investment.Id,
                            Date = ret.GetProperty("Date").GetDateTime(),
                            Value = ret.GetProperty("Value").GetDecimal()
                        });
                    }
                }

                // Wczytaj historię sprzedaży (dla sprzedanych inwestycji)
                if (inv.TryGetProperty("SaleHistory", out JsonElement saleHistory))
                {
                    foreach (JsonElement ret in saleHistory.EnumerateArray())
                    {
                        dbManager.ReturnsHistories.Add(new ReturnsHistory
                        {
                            InvestmentId = investment.Id,
                            Date = ret.GetProperty("Date").GetDateTime(),
                            Value = ret.GetProperty("Value").GetDecimal()
                        });
                    }
                }

                dbManager.SaveChanges();

                SetupListView(userId); // Odśwież listę inwestycji
            }
        }
        public async Task RefreshTotalBalanceAsync(int userId, bool useMockOnFail = false, Dictionary<string, decimal>? pricesFromListView = null)
        {
            decimal totalInvested = 0;
            decimal totalCurrentValue = 0;
            decimal totalSoldValue = 0;

            var inwestycjeUzytkownika = dbManager.Investments
                .Include(i => i.ReturnsHistories)
                .Where(i => i.UserId == userId)
                .ToList();

            foreach (var inv in inwestycjeUzytkownika)
            {
                if (inv.NumberOfShares == 0)
                    continue;

                decimal? buyPrice = inv.BuyPrice;
                if ((buyPrice == null || buyPrice == 0) && useMockOnFail && inv.MockPrice.HasValue)
                {
                    buyPrice = inv.MockPrice.Value;
                }

                if (buyPrice == null || buyPrice == 0)
                    continue;

                decimal invested = buyPrice.Value * inv.NumberOfShares;
                totalInvested += invested;

                if (!inv.IsSold)
                {
                    decimal? currentPrice = null;
                    if (pricesFromListView != null && pricesFromListView.TryGetValue(inv.Name, out decimal fromUI))
                    {
                        currentPrice = fromUI;
                    }
                    else
                    {
                        currentPrice = await AlphaVantageService.GetLatestClosePriceAsync(inv.Name);
                        if (currentPrice == null && useMockOnFail && inv.MockPrice.HasValue)
                        {
                            currentPrice = inv.MockPrice.Value;
                        }
                    }

                    if (currentPrice != null)
                    {
                        totalCurrentValue += currentPrice.Value * inv.NumberOfShares;
                    }
                }
                else
                {
                    var sale = inv.ReturnsHistories
                        .OrderByDescending(r => r.Date)
                        .FirstOrDefault();

                    if (sale != null)
                        totalSoldValue += sale.Value * inv.NumberOfShares;
                }
            }

            decimal totalFinal = totalCurrentValue + totalSoldValue;
            decimal totalChange = totalInvested == 0 ? 0 : ((totalFinal - totalInvested) / totalInvested) * 100;
            string result = totalChange >= 0 ? $"+{totalChange:F2}%" : $"{totalChange:F2}%";

            labelBilans.Text = $"Bilans ogólny: {result}";
            labelBilans.ForeColor = totalChange >= 0 ? Color.DarkGreen : Color.DarkRed;
        }


        private async void btnOdswiez_Click(object sender, EventArgs e)
        {
            int? userId = dbManager.GetUserIdByUsername(zalogowanyUzytkownik);
            if (userId == null)
            {
                MessageBox.Show("Nie znaleziono zalogowanego użytkownika.");
                return;
            }

            bool useMockOnFail = checkBoxTrybTestowy.Checked;

            var inwestycjeUzytkownika = dbManager.Investments
                .Where(i => i.UserId == userId && !i.IsSold)
                .ToList();

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                var item = listView1.Items[i];
                string symbol = item.SubItems[0].Text;

                var inwestycja = inwestycjeUzytkownika.FirstOrDefault(i => i.Name == symbol);
                if (inwestycja == null)
                {
                    item.ForeColor = Color.Gray;
                    continue;
                }

                decimal? buyPrice = null;

                if (useMockOnFail && inwestycja.BuyPrice.HasValue)
                {
                    buyPrice = inwestycja.BuyPrice.Value;
                }
                else if(useMockOnFail && inwestycja.MockPrice.HasValue)
                {
                    buyPrice=inwestycja.MockPrice.Value;
                }
                else
                {
                    buyPrice = inwestycja.BuyPrice;
                }

                if (buyPrice == null || buyPrice == 0)
                {
                    item.ForeColor = Color.Gray;
                    continue;
                }

                decimal? currentPrice;

                if (checkBoxTrybTestowy.Checked)
{
                // Wymuszona testowa cena aktualna
                if (decimal.TryParse(textBoxAktualnaCenaTest.Text, out decimal testPrice))
                {
                    currentPrice = testPrice;
                }
                else
                {
                    MessageBox.Show("Nieprawidłowa wartość testowej ceny!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
}
                else
                {
                    currentPrice = await FinnhubService.GetCurrentQuoteAsync(symbol);
                    //currentPrice = await AlphaVantageService.GetLatestClosePriceAsync(symbol);

                    if (currentPrice == null && useMockOnFail && inwestycja.MockPrice.HasValue)
                    {
                        currentPrice = inwestycja.MockPrice.Value;
                    }
                }

                if (currentPrice == null)
                {
                    item.ForeColor = Color.Orange;
                    continue;
                }

                if (item.SubItems.Count <= 7)
                    item.SubItems.Add(currentPrice.Value.ToString("F2"));
                else
                    item.SubItems[7].Text = currentPrice.Value.ToString("F2");

                decimal totalBuyValue = buyPrice.Value * inwestycja.NumberOfShares;
                decimal totalCurrentValue = currentPrice.Value * inwestycja.NumberOfShares;

                decimal change = ((totalCurrentValue - totalBuyValue) / totalBuyValue) * 100;

                string bilansText = change >= 0 ? $"+{change:F2}%" : $"{change:F2}%";

                if (item.SubItems.Count <= 8)
                    item.SubItems.Add(bilansText);
                else
                    item.SubItems[8].Text = bilansText;

                item.ForeColor = change >= 0 ? Color.LightGreen : Color.Red;
            }

            var cenyZListView = new Dictionary<string, decimal>();
            foreach (ListViewItem item in listView1.Items)
            {
                string symbol = item.SubItems[0].Text;
                if (item.SubItems.Count > 7 && decimal.TryParse(item.SubItems[7].Text, out decimal cena))
                {
                    cenyZListView[symbol] = cena;
                }
            }

            await RefreshTotalBalanceAsync(userId.Value, useMockOnFail, cenyZListView); // PRZEKAZUJ FLAGĘ DALEJ

            MessageBox.Show("Dane zostały odświeżone.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void checkBoxTrybTestowy_CheckedChanged(object sender, EventArgs e)
        {
            bool widoczny = checkBoxTrybTestowy.Checked;
            labelTestPrice.Visible = widoczny;
            textBoxAktualnaCenaTest.Visible = widoczny;

            // Odśwież listę inwestycji, jeśli trzeba
            int? userId = dbManager.GetUserIdByUsername(zalogowanyUzytkownik);
            if (userId != null)
            {
                SetupListView(userId.Value);
            }
        }
    }
}
