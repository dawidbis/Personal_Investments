namespace Polygon_api
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnOdczytaj_Click(object sender, EventArgs e)
        {
            string ticker = txtTicker.Text.ToUpper();

            if (string.IsNullOrWhiteSpace(ticker))
            {
                MessageBox.Show("WprowadŸ symbol akcji (ticker).");
                return;
            }

            try
            {
                var service = new AlphaVantageService();
                var allCandles = await service.GetDailyCandlesAsync(ticker);

                if (allCandles == null || allCandles.Count == 0)
                {
                    MessageBox.Show("Brak danych.");
                    return;
                }

                // Pobieramy np. tylko ostatnie 30 dni
                var recentCandles = allCandles
                    .OrderByDescending(c => c.Date)
                    .Take(1)
                    .ToList();

                using var db = new dbManager();

                var existing = db.AlphaVantageCandles
                    .Where(c => c.Ticker == ticker && recentCandles.Select(n => n.Date).Contains(c.Date))
                    .Select(c => new { c.Ticker, c.Date })
                    .ToHashSet();

                var newCandles = recentCandles
                    .Where(c => !existing.Contains(new { c.Ticker, c.Date }))
                    .ToList();

                db.AlphaVantageCandles.AddRange(newCandles);
                await db.SaveChangesAsync();

                MessageBox.Show($"Zapisano {newCandles.Count} nowych rekordów dla {ticker}.");
                dataGridView1.DataSource = newCandles;
            }
            catch (Exception ex)
            {
                MessageBox.Show("B³¹d: " + ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) { }

        private void txtFrom_TextChanged(object sender, EventArgs e) { }

        private void txtTo_TextChanged(object sender, EventArgs e) { }
    }
}
