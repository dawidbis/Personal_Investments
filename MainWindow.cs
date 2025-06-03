#pragma warning disable CS0436
using DatabaseConnection;
using Microsoft.EntityFrameworkCore;
using Polygon_api;

namespace Personal_Investment_App
{

    public partial class MainWindow : Form
    {
        private DatabaseManager dbManager;
        public bool Wylogowano { get; private set; } = false;
        private readonly string zalogowanyUzytkownik;

        private void SetupListView(int userId)
        {
            listView1.View = View.Details;
            listView1.FullRowSelect = true;

            listView1.Columns.Clear();
            listView1.Columns.Add("Nazwa", 150);
            listView1.Columns.Add("Kwota", 100);
            listView1.Columns.Add("Data", 100);
            listView1.Columns.Add("Oczekiwany Zwrot", 140);
            listView1.Columns.Add("Typ", 100);
            listView1.Columns.Add("Ryzyko", 80);
            listView1.Columns.Add("Kategoria", 100);
            listView1.Columns.Add("Cena zamknięcia", 140);

            listView1.Items.Clear();

            var inwestycje = dbManager.Investments
                .Include(i => i.Type)
                .ThenInclude(t => t.Category)
                .Where(i => i.UserId == userId)
                .ToList();

            int index = 0;
            foreach (var inv in inwestycje)
            {
                var item = new ListViewItem(inv.Name);
                item.SubItems.Add(inv.AmountInvested.ToString("C"));
                item.SubItems.Add(inv.DateOfInvestment.ToShortDateString());
                item.SubItems.Add(inv.ExpectedReturn.ToString("P2"));
                item.SubItems.Add(inv.Type?.Name ?? "Brak");
                item.SubItems.Add(inv.Type?.RiskLevel ?? "Brak");
                item.SubItems.Add(inv.Type?.Category?.Name ?? "Brak");
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

            var form = new AddStockForm(dbManager, userId.Value);

            form.FormClosed += (s, args) =>
            {
                var inv = form.CreatedInvestment;
                if (inv != null)
                {
                    var type = dbManager.InvestmentTypes.FirstOrDefault(t => t.Id == inv.TypeId);
                    var category = dbManager.InvestmentCategories.FirstOrDefault(c => c.Id == type.CategoryId);

                    var item = new ListViewItem();
                    item.SubItems.Add(inv.Name);
                    item.SubItems.Add(inv.AmountInvested.ToString("C"));
                    item.SubItems.Add(inv.DateOfInvestment.ToShortDateString());
                    item.SubItems.Add(inv.ExpectedReturn.ToString("P2"));
                    item.SubItems.Add(type?.Name ?? "Nieznany");
                    item.SubItems.Add(type?.RiskLevel ?? "Nieznane");
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
                $"Czy na pewno chcesz sprzedać inwestycję \"{investmentName}\"?\nZostanie zapisana w historii jako sprzedana po aktualnej cenie rynkowej.",
                "Potwierdź sprzedaż",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            var candleService = new AlphaVantageService();
            var candles = await candleService.GetDailyCandlesAsync(investmentName); // zakładamy że nazwa == symbol
            var latest = candles.OrderByDescending(c => c.Date).FirstOrDefault();

            if (latest == null)
            {
                MessageBox.Show("Nie udało się pobrać aktualnej ceny dla tej inwestycji.", "Błąd API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal marketPrice = latest.C;

            bool success = dbManager.SellInvestment(investmentId, marketPrice);

            if (success)
            {
                MessageBox.Show($"Inwestycja \"{investmentName}\" została sprzedana po cenie {marketPrice:C}.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);

                int? userId = dbManager.GetUserIdByUsername(zalogowanyUzytkownik);
                if (userId.HasValue)
                {
                    SetupListView(userId.Value); // Odśwież listę
                }
            }
            else
            {
                MessageBox.Show("Nie udało się sprzedać inwestycji.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void btnOdswiez_Click(object sender, EventArgs e)
        {
            var candleService = new AlphaVantageService();

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                var item = listView1.Items[i];

                // ticker jest w kolumnie 0 („Nazwa”), nie w 1
                string symbol = item.SubItems[0].Text;

                var candles = await candleService.GetDailyCandlesAsync(symbol);
                var latest = candles.OrderByDescending(c => c.Date).FirstOrDefault();
                string lastClose = latest?.C.ToString("F2") ?? "Brak";

                // kolumna "Cena zamknięcia" ma indeks 7
                if (item.SubItems.Count <= 7)
                    item.SubItems.Add(lastClose);
                else
                    item.SubItems[7].Text = lastClose;

            }

            MessageBox.Show("Dane zostały odświeżone.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
