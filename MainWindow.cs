#pragma warning disable CS0436
using DatabaseConnection;

namespace Personal_Investment_App
{
    public partial class MainWindow : Form
    {
        private DatabaseManager dbManager;
        public bool Wylogowano { get; private set; } = false;
        private readonly string zalogowanyUzytkownik;
        public MainWindow(DatabaseManager dbManager, string zalogowanyUzytkownik)

        {
            InitializeComponent();

            this.dbManager = dbManager;
            this.zalogowanyUzytkownik = zalogowanyUzytkownik;
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
                    var item = new ListViewItem(inv.Id.ToString());
                    item.SubItems.Add(inv.Name);
                    item.SubItems.Add(inv.AmountInvested.ToString("C"));
                    item.SubItems.Add(inv.DateOfInvestment.ToShortDateString());
                    item.SubItems.Add(inv.ExpectedReturn.ToString("P2"));
                    listView1.Items.Add(item);
                }
            };

            form.Show();
        }
    }
}
