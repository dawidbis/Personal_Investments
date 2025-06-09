#pragma warning disable CS0436
using DatabaseConnection;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using Personal_Investment_App.FinnhubApi;
using Polygon_api;
using ProgramLogic;
using System.Data;
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

        private enum WidokAkcji { Aktywne, Historia }
        private WidokAkcji aktualnyWidok = WidokAkcji.Aktywne;

        private void SetupListView(int userId)
        {
            aktualnyWidok = WidokAkcji.Aktywne;

            listView1.View = View.Details;
            listView1.FullRowSelect = true;

            listView1.Columns.Clear();
            listView1.Columns.Add("Nazwa", 100);
            listView1.Columns.Add("Liczba Akcji", 100);
            listView1.Columns.Add("Cena zakupu Akcji", 154);
            listView1.Columns.Add("Data", 100);
            listView1.Columns.Add("Oczekiwany Zwrot", 130);
            listView1.Columns.Add("Stop Loss", 80);
            listView1.Columns.Add("Typ", 60);
            listView1.Columns.Add("Aktualna Cena", 140);
            listView1.Columns.Add("Aktualny Bilans", 190);

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
                item.SubItems.Add($"{inv.BuyPrice} USD" ?? "Brak");
                item.SubItems.Add(inv.DateOfInvestment.ToShortDateString());
                item.SubItems.Add(inv.ExpectedReturnPercent.ToString("P2"));
                item.SubItems.Add(inv.StopLossPercent.ToString("P2"));
                item.SubItems.Add(inv.Type?.Name ?? "Brak");
                item.SubItems.Add("...");
                item.SubItems.Add("...");

                item.ImageIndex = inv.Type?.Category?.Name switch
                {
                    "Akcje" => 1,           // graph.png
                    "Kryptowaluty" => 0,    //bitcoin.png   
                    "Surowce" => 3,          //bars.png
                    _ => -1
                };

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

            checkBoxTrybTestowy.Visible = zalogowanyUzytkownik.ToLower() == "admin";

            int? userId = dbManager.GetUserIdByUsername(zalogowanyUzytkownik);
            if (userId.HasValue)
            {
                SetupListView(userId.Value);
            }

            autoCheckTimer = new Timer();
            autoCheckTimer.Interval = 10 * 60 * 100;
            autoCheckTimer.Tick += AutoCheckTimer_Tick;
            autoCheckTimer.Start();

            SetupListView(userId ?? 0); // Ustawienie listy inwestycji przy starcie
            btnOdswiez_Click(null, null); // Odświeżenie danych inwestycji
        }

        private async void AutoCheckTimer_Tick(object sender, EventArgs e)
        {
            int? userId = dbManager.GetUserIdByUsername(zalogowanyUzytkownik);
            if (userId == null)
                return;

            try
            {
                // Zbieranie testowych cen z ListView (SubItem[7] = Aktualna cena)
                var testPrices = new Dictionary<string, decimal>();
                foreach (ListViewItem item in listView1.Items)
                {
                    string symbol = item.SubItems[0].Text;

                    if (item.SubItems.Count > 7)
                    {
                        string cenaTekst = item.SubItems[7].Text.Replace(" USD", "").Trim();

                        if (decimal.TryParse(cenaTekst, out decimal parsed))
                        {
                            testPrices[symbol] = parsed;
                        }
                    }
                }

                // Przekazanie testowych cen do metody logiki inwestycyjnej
                var alerts = await dbManager.CheckInvestmentsAutomaticallyAsync(
                    userId.Value,
                    IsTrybTestowy,
                    testPrices
                );

                if (alerts.Any())
                {
                    string message = string.Join(Environment.NewLine, alerts);
                    // Możesz dodać logowanie do np. TextBox / ListBox zamiast MessageBox
                    MessageBox.Show(message, "Alert inwestycyjny", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd automatycznego sprawdzania inwestycji: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Odśwież dane po automatycznej kontroli
            btnOdswiez_Click(sender, e);
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

            var form = new AddStockForm(dbManager, userId.Value, InvestmentKind.Akcja);

            form.FormClosed += (s, args) =>
            {
                var inv = form.CreatedInvestment;
                if (inv != null)
                {
                    var type = dbManager.InvestmentTypes.FirstOrDefault(t => t.Id == inv.TypeId);
                    //var category = dbManager.InvestmentCategories.FirstOrDefault(c => c.Id == type.CategoryId);

                    var item = new ListViewItem();
                    item.SubItems.Add(inv.Name);
                    item.SubItems.Add($"{inv.NumberOfShares} szt");
                    item.SubItems.Add($"{inv.BuyPrice} USD" ?? "Brak");
                    item.SubItems.Add(inv.DateOfInvestment.ToShortDateString());
                    item.SubItems.Add(inv.ExpectedReturnPercent.ToString("P2"));
                    item.SubItems.Add(inv.StopLossPercent.ToString("P2"));
                    item.SubItems.Add(type?.Name ?? "Nieznany");

                    listView1.Items.Add(item);
                }

                if (form.CreatedInvestment != null)
                {
                    SetupListView(userId.Value); // Odśwież listę
                }
            };

            form.Show();
        }
        
        private void kryptowalutaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int? userId = dbManager.GetUserIdByUsername(zalogowanyUzytkownik);

            if (userId == null)
            {
                MessageBox.Show("Nie można znaleźć zalogowanego użytkownika w bazie.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var form = new AddStockForm(dbManager, userId.Value, InvestmentKind.Kryptowaluta);

            form.FormClosed += (s, args) =>
            {
                var inv = form.CreatedInvestment;
                if (inv != null)
                {
                    var type = dbManager.InvestmentTypes.FirstOrDefault(t => t.Id == inv.TypeId);
                    //var category = dbManager.InvestmentCategories.FirstOrDefault(c => c.Id == type.CategoryId);

                    var item = new ListViewItem();
                    item.SubItems.Add(inv.Name);
                    item.SubItems.Add($"{inv.NumberOfShares} szt");
                    item.SubItems.Add($"{inv.BuyPrice} USD" ?? "Brak");
                    item.SubItems.Add(inv.DateOfInvestment.ToShortDateString());
                    item.SubItems.Add(inv.ExpectedReturnPercent.ToString("P2"));
                    item.SubItems.Add(inv.StopLossPercent.ToString("P2"));
                    item.SubItems.Add(type?.Name ?? "Nieznany");

                    listView1.Items.Add(item);
                }

                if (form.CreatedInvestment != null)
                {
                    SetupListView(userId.Value); // Odśwież listę
                }
            };

            form.Show();
        }

        private void surowiecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int? userId = dbManager.GetUserIdByUsername(zalogowanyUzytkownik);

            if (userId == null)
            {
                MessageBox.Show("Nie można znaleźć zalogowanego użytkownika w bazie.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var form = new AddStockForm(dbManager, userId.Value, InvestmentKind.Surowiec);

            form.FormClosed += (s, args) =>
            {
                var inv = form.CreatedInvestment;
                if (inv != null)
                {
                    var type = dbManager.InvestmentTypes.FirstOrDefault(t => t.Id == inv.TypeId);
                    //var category = dbManager.InvestmentCategories.FirstOrDefault(c => c.Id == type.CategoryId);

                    var item = new ListViewItem();
                    item.SubItems.Add(inv.Name);
                    item.SubItems.Add($"{inv.NumberOfShares} szt");
                    item.SubItems.Add($"{inv.BuyPrice} USD" ?? "Brak");
                    item.SubItems.Add(inv.DateOfInvestment.ToShortDateString());
                    item.SubItems.Add(inv.ExpectedReturnPercent.ToString("P2"));
                    item.SubItems.Add(inv.StopLossPercent.ToString("P2"));
                    item.SubItems.Add(type?.Name ?? "Nieznany");

                    listView1.Items.Add(item);
                }

                if (form.CreatedInvestment != null)
                {
                    SetupListView(userId.Value); // Odśwież listę
                }
            };

            form.Show();
        }
        private void obligacjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int? userId = dbManager.GetUserIdByUsername(zalogowanyUzytkownik);

            if (userId == null)
            {
                MessageBox.Show("Nie można znaleźć zalogowanego użytkownika w bazie.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var form = new AddStockForm(dbManager, userId.Value, InvestmentKind.Kryptowaluta);

            form.FormClosed += (s, args) =>
            {
                var inv = form.CreatedInvestment;
                if (inv != null)
                {
                    var type = dbManager.InvestmentTypes.FirstOrDefault(t => t.Id == inv.TypeId);
                    //var category = dbManager.InvestmentCategories.FirstOrDefault(c => c.Id == type.CategoryId);

                    var item = new ListViewItem();
                    item.SubItems.Add(inv.Name);
                    item.SubItems.Add($"{inv.NumberOfShares} szt");
                    item.SubItems.Add($"{inv.BuyPrice} USD" ?? "Brak");
                    item.SubItems.Add(inv.DateOfInvestment.ToShortDateString());
                    item.SubItems.Add(inv.ExpectedReturnPercent.ToString("P2"));
                    item.SubItems.Add(inv.StopLossPercent.ToString("P2"));
                    item.SubItems.Add(type?.Name ?? "Nieznany");

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
            if (listView1.SelectedItems.Count == 0 || aktualnyWidok == WidokAkcji.Historia)
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

            using (var context = new DatabaseManager())
            {
                var inwestycja = context.Investments
                    .Include(i => i.Type)
                    .FirstOrDefault(i => i.Id == investmentId);

                if (inwestycja == null || inwestycja.Type == null)
                {
                    MessageBox.Show("Nie znaleziono inwestycji lub jej typu.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

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
                    if (inwestycja.Type.Name == "Akcje")
                    {
                        var latest = await FinnhubService.GetCurrentQuoteAsync(inwestycja.Name);
                        if (latest == null)
                        {
                            MessageBox.Show("Nie udało się pobrać aktualnej ceny akcji.", "Błąd API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        marketPrice = (decimal)latest;
                    }
                    else if (inwestycja.Type.Name == "Kryptowaluty")
                    {
                        var latest = await FinnhubService.GetCurrentCryptoQuoteAsync(inwestycja.Name);
                        if (latest == null)
                        {
                            MessageBox.Show("Nie udało się pobrać aktualnej ceny kryptowaluty.", "Błąd API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        marketPrice = (decimal)latest;
                    }
                    else if(inwestycja.Type.Name=="Surowce")
                    {
                        var latest = await TwelveDataService.GetTodayClosePriceAsync(inwestycja.Name);
                        if (latest == null)
                        {
                            MessageBox.Show("Nie udało się pobrać aktualnej ceny surowca.", "Błąd API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        marketPrice = (decimal)latest;
                    }
                    else
                    {
                        MessageBox.Show("Nieobsługiwany typ inwestycji.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
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
                decimal expectedReturn = inv.GetProperty("ExpectedReturnPercent").GetDecimal();
                decimal stopLoss = inv.GetProperty("StopLossPercent").GetDecimal();
                decimal buyPrice = inv.GetProperty("BuyPrice").GetDecimal();
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
                    StopLossPercent = stopLoss,
                    BuyPrice = buyPrice,
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

        public async Task RefreshTotalBalanceAsync(int userId, bool useMockOnFail = false, Dictionary<int, decimal>? pricesFromListView = null)
        {
            decimal totalInvested = 0;
            decimal totalCurrentValue = 0;
            decimal totalSoldValue = 0;

            // Nowe zmienne do aktualnego bilansu (dla niesprzedanych)
            decimal aktualnieZainwestowane = 0;
            decimal aktualnaWartosc = 0;

            var inwestycjeUzytkownika = dbManager.Investments
                .Include(i => i.ReturnsHistories)
                .Where(i => i.UserId == userId)
                .ToList();

            foreach (var inv in inwestycjeUzytkownika)
            {
                if (inv.NumberOfShares == 0)
                    continue;

                decimal? buyPrice = inv.BuyPrice;
                if (buyPrice == null || buyPrice == 0)
                    continue;

                decimal invested = buyPrice.Value * inv.NumberOfShares;
                totalInvested += invested;

                if (!inv.IsSold)
                {
                    aktualnieZainwestowane += invested;

                    decimal? currentPrice = null;

                    if (pricesFromListView != null && pricesFromListView.TryGetValue(inv.Id, out decimal fromUI))
                    {
                        currentPrice = fromUI;
                    }
                    else
                    {
                        try
                        {
                            currentPrice = await FinnhubService.GetCurrentQuoteAsync(inv.Name);
                        }
                        catch
                        {
                            if (useMockOnFail) currentPrice = buyPrice;
                        }
                    }

                    if (currentPrice != null)
                    {
                        decimal currentValue = currentPrice.Value * inv.NumberOfShares;
                        totalCurrentValue += currentValue;
                        aktualnaWartosc += currentValue;
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
            decimal totalChange = totalFinal - totalInvested;
            decimal aktualnyBilans = aktualnaWartosc - aktualnieZainwestowane;

            string znak = totalChange >= 0 ? "+" : "";
            string znakAktualny = aktualnyBilans >= 0 ? "+" : "";

            labelBilans.Text = $"Bilans konta:         {znak}{totalChange:F2} USD";
            labelBilans.ForeColor = totalChange > 0 ? Color.LightGreen :
                                    totalChange < 0 ? Color.Red :
                                                      Color.Gold;

            labelBilansAktualny.Text = $"Bilans aktualny:   {znakAktualny}{aktualnyBilans:F2} USD";
            labelBilansAktualny.ForeColor = aktualnyBilans > 0 ? Color.LightGreen :
                                            aktualnyBilans < 0 ? Color.Red :
                                                                 Color.Gold;
        }


        private async void btnOdswiez_Click(object sender, EventArgs e)
        {
            if (aktualnyWidok == WidokAkcji.Historia)
            {
                btnHistoria_Click(sender, e); // po prostu ponownie załaduj historię
                return;
            }

            int? userId = dbManager.GetUserIdByUsername(zalogowanyUzytkownik);
            if (userId == null)
            {
                MessageBox.Show("Nie znaleziono zalogowanego użytkownika.");
                return;
            }

            var zaznaczoneId = listView1.SelectedItems
            .Cast<ListViewItem>()
            .Where(i => i.Tag is int)
            .Select(i => (int)i.Tag)
            .ToHashSet();

            SetupListView((int)userId);

            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Tag is int id && zaznaczoneId.Contains(id))
                {
                    item.Selected = true;
                }
            }

            bool useMockOnFail = checkBoxTrybTestowy.Checked;

            // Pobierz inwestycje użytkownika z bazy danych
            List<Investment> inwestycjeUzytkownika;
            using (var context = new DatabaseManager())
            {
                inwestycjeUzytkownika = context.Investments
                    .Include(i => i.Type) // ✅ to jest wymagane!
                    .Where(i => i.UserId == userId && !i.IsSold)
                    .ToList();
            }

            var cenyZListView = new Dictionary<int, decimal>(); // Klucz: Investment.Id

            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Tag is not int investmentId)
                    continue;

                var inwestycja = inwestycjeUzytkownika.FirstOrDefault(i => i.Id == investmentId);
                if (inwestycja == null)
                {
                    item.ForeColor = Color.Gray;
                    continue;
                }

                decimal? buyPrice = inwestycja.BuyPrice;
                if (buyPrice == null || buyPrice == 0)
                {
                    item.ForeColor = Color.Gray;
                    continue;
                }

                decimal? currentPrice = null;

                // TRYB TESTOWY
                if (useMockOnFail)
                {
                    if (item.Selected)
                    {
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
                        continue; // pomijamy niezaznaczone
                    }
                }
                else
                {
                    if (inwestycja.Type?.Name == "Akcje")
                    {
                        currentPrice = await FinnhubService.GetCurrentQuoteAsync(inwestycja.Name);
                    }
                    else if (inwestycja.Type?.Name == "Kryptowaluty")
                    {
                        currentPrice = await FinnhubService.GetCurrentCryptoQuoteAsync(inwestycja.Name);
                    }
                    else
                    {
                        currentPrice=await TwelveDataService.GetTodayClosePriceAsync(inwestycja.Name);
                    }
                }

                if (currentPrice == null)
                {
                    item.ForeColor = Color.Orange;
                    continue;
                }

                // Dodajemy aktualną cenę do słownika
                cenyZListView[investmentId] = currentPrice.Value;

                // Upewnij się, że są wystarczające subitemy
                while (item.SubItems.Count <= 8)
                {
                    item.SubItems.Add(""); // wypełni brakujące pola
                }

                // Wyświetlanie aktualnej ceny
                item.SubItems[7].Text = $"{currentPrice.Value:F2} USD";

                // Oblicz bilans
                decimal totalBuyValue = buyPrice.Value * inwestycja.NumberOfShares;
                decimal totalCurrentValue = currentPrice.Value * inwestycja.NumberOfShares;
                decimal valueChange = totalCurrentValue - totalBuyValue;

                decimal change = ((totalCurrentValue - totalBuyValue) / totalBuyValue) * 100;
                string bilansText = change >= 0 ? $"{valueChange:F2} USD,   +{change:F2}%" : $"{valueChange:F2} USD,   {change:F2}%";

                item.SubItems[8].Text = bilansText;
                item.ForeColor = change > 0 ? Color.LightGreen : change < 0 ? Color.Red : Color.Yellow;
            }

            // ⬇️ NAJWAŻNIEJSZA CZĘŚĆ, KTÓREJ BRAKOWAŁO ⬇️
            await RefreshTotalBalanceAsync(userId.Value, useMockOnFail, cenyZListView);

            //MessageBox.Show("Dane zostały odświeżone.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnHistoria_Click(object sender, EventArgs e)
        {
            aktualnyWidok = WidokAkcji.Historia;

            int? userId = dbManager.GetUserIdByUsername(zalogowanyUzytkownik);
            if (userId == null)
            {
                MessageBox.Show("Nie znaleziono zalogowanego użytkownika.");
                return;
            }

            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.Columns.Clear();
            listView1.Items.Clear();

            listView1.Columns.Add("Nazwa", 150);
            listView1.Columns.Add("Liczba Akcji", 100);
            listView1.Columns.Add("Cena Zakupu", 100);
            listView1.Columns.Add("Data Zakupu", 100);
            listView1.Columns.Add("Data Sprzedaży", 120);
            listView1.Columns.Add("Wartość Sprzedaży", 130);
            listView1.Columns.Add("Zysk/Strata", 254);
            listView1.Columns.Add("Typ", 100);

            var sprzedaneInwestycje = dbManager.Investments
                .Include(i => i.Type)
                .ThenInclude(t => t.Category)
                .Include(i => i.ReturnsHistories)
                .Where(i => i.UserId == userId && i.IsSold)
                .ToList();

            int index = 0;
            foreach (var inv in sprzedaneInwestycje)
            {
                var ostatniaSprzedaz = inv.ReturnsHistories
                    .OrderByDescending(r => r.Date)
                    .FirstOrDefault();

                if (ostatniaSprzedaz == null)
                    continue;

                decimal buyPrice = inv.BuyPrice;
                decimal numberOfShares = inv.NumberOfShares;
                decimal totalBuy = buyPrice * numberOfShares;
                decimal totalSell = ostatniaSprzedaz.Value * numberOfShares;
                decimal changeValue = totalSell - totalBuy;

                decimal changePercent = totalBuy != 0m
                    ? ((totalSell - totalBuy) / totalBuy) * 100m
                    : 0m;

                var item = new ListViewItem(inv.Name);
                item.SubItems.Add($"{numberOfShares} szt");
                item.SubItems.Add($"{buyPrice:F2} USD");
                item.SubItems.Add(inv.DateOfInvestment.ToShortDateString());
                item.SubItems.Add(ostatniaSprzedaz.Date.ToShortDateString());
                item.SubItems.Add($"{ostatniaSprzedaz.Value:F2} USD");
                item.SubItems.Add($"{changeValue:F2} USD,   {(changePercent >= 0 ? "+" : "")}{changePercent:F2}%");
                item.SubItems.Add(inv.Type?.Name ?? "Brak");

                item.ImageIndex = inv.Type?.Category?.Name switch
                {
                    "Akcje" => 1,           // graph.png
                    "Kryptowaluty" => 0,    //bitcoin.png   
                    "Surowce"=> 3,          //bars.png
                    _ => -1
                };

                item.BackColor = (index % 2 == 0)
                    ? Color.Black
                    : Color.FromArgb(60, 0, 90);

                item.ForeColor = changePercent > 0 ? Color.LightGreen :
                                 changePercent < 0 ? Color.Red :
                                 Color.Yellow;

                listView1.Items.Add(item);
                index++;
            }
        }

        private void btnAktualne_Click(object sender, EventArgs e)
        {
            aktualnyWidok = WidokAkcji.Aktywne;

            btnOdswiez_Click(sender, e); // Odśwież aktywne inwestycje
        }

        private void generujRaportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new ReportForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    bool sprzedane = form.FiltrSprzedane;
                    bool filtrTicker = form.FiltrTickerAktywny;
                    string ticker = form.Ticker?.ToUpper();
                    DateTime dataOd = form.DataOd.Date;
                    DateTime dataDo = form.DataDo.Date;

                    int userId = dbManager.GetUserIdByUsername(zalogowanyUzytkownik) ?? 0;

                    // Wyłącznie sprzedane inwestycje
                    var query = dbManager.Investments
                        .Where(inv => inv.UserId == userId && inv.IsSold)
                        .Where(inv => inv.DateOfInvestment >= dataOd && inv.DateOfInvestment <= dataDo);

                    if (filtrTicker && !string.IsNullOrEmpty(ticker))
                    {
                        query = query.Where(inv => inv.Name.ToUpper() == ticker);
                    }

                    var investments = query.ToList();

                    decimal wartoscZainwestowana = 0;
                    decimal wartoscUzyskana = 0;

                    foreach (var inv in investments)
                    {
                        decimal kupno = inv.BuyPrice * inv.NumberOfShares;
                        wartoscZainwestowana += kupno;

                        var sprzedaz = inv.ReturnsHistories
                            .OrderByDescending(r => r.Date)
                            .FirstOrDefault();

                        if (sprzedaz != null)
                        {
                            wartoscUzyskana += sprzedaz.Value * inv.NumberOfShares;
                        }
                        else
                        {
                            wartoscUzyskana += kupno;
                        }
                    }

                    decimal zysk = wartoscUzyskana - wartoscZainwestowana;

                    // Wyznacz kolor w zależności od wyniku
                    Color kolorZysku;
                    if (zysk > 0)
                        kolorZysku = Color.LightGreen;
                    else if (zysk < 0)
                        kolorZysku = Color.Red;
                    else
                        kolorZysku = Color.Gold;

                    // Buduj nagłówek
                    string zakres = $"Raport za okres: \n   {dataOd:yyyy-MM-dd} do {dataDo:yyyy-MM-dd}";
                    string filtrAkcji = filtrTicker && !string.IsNullOrEmpty(ticker)
                        ? $"dla akcji: {ticker}"
                        : "dla wszystkich akcji";

                    string naglowek = $"{zakres}\n{filtrAkcji}";

                    var culture = new System.Globalization.CultureInfo("en-US");

                    // Przygotuj labelRaport
                    labelRaport.Text =
                        $"{naglowek}\n\n" +
                        $"Wartość zainwestowana:\n   {wartoscZainwestowana:F2} USD\n" +
                        $"Wartość uzyskana ze sprzedaży:\n   {wartoscUzyskana:F2} USD\n" +
                        $"Zysk / Strata:\n    {zysk:F2} USD";

                    labelRaport.ForeColor = kolorZysku;
   
                    labelRaport.Visible = true;

                    if (!panelUser.Controls.Contains(labelRaport))
                        panelUser.Controls.Add(labelRaport);
                }
            }
        }
    }
}
