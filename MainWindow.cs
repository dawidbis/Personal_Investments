#pragma warning disable CS0436
using DatabaseConnection;
using Microsoft.EntityFrameworkCore;

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
            listView1.Columns.Add("ID", 50);
            listView1.Columns.Add("Nazwa", 150);
            listView1.Columns.Add("Kwota", 100);
            listView1.Columns.Add("Data", 100);
            listView1.Columns.Add("Oczekiwany Zwrot", 120);
            listView1.Columns.Add("Typ", 100);
            listView1.Columns.Add("Ryzyko", 80);
            listView1.Columns.Add("Kategoria", 100);

            listView1.Items.Clear();

            var inwestycje = dbManager.Investments
                .Include(i => i.Type)
                .ThenInclude(t => t.Category)
                .Where(i => i.UserId == userId)
                .ToList();

            foreach (var inv in inwestycje)
            {
                var item = new ListViewItem(inv.Id.ToString());

                item.SubItems.Add(inv.Name);
                item.SubItems.Add(inv.AmountInvested.ToString("C"));
                item.SubItems.Add(inv.DateOfInvestment.ToShortDateString());
                item.SubItems.Add(inv.ExpectedReturn.ToString("P2"));
                item.SubItems.Add(inv.Type?.Name ?? "Brak");
                item.SubItems.Add(inv.Type?.RiskLevel ?? "Brak");
                item.SubItems.Add(inv.Type?.Category?.Name ?? "Brak");

                // Dodajemy ikonkę akcji (indeks 1 w ImageList)
                if (inv.Type?.Category?.Name == "Akcje")
                {
                    item.ImageIndex = 1;
                }
                else
                {
                    item.ImageIndex = -1; // Brak ikony
                }

                listView1.Items.Add(item);
            }
        }

        public MainWindow(DatabaseManager dbManager, string zalogowanyUzytkownik)

        {
            InitializeComponent();
            this.dbManager = dbManager;
            this.zalogowanyUzytkownik = zalogowanyUzytkownik;
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

                    var item = new ListViewItem(inv.Id.ToString());
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
    }
}
