using DatabaseConnection;
using ProgramLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Polygon_api;
using Personal_Investment_App.FinnhubApi;

namespace Personal_Investment_App
{
    public partial class AddStockForm : Form
    {
        private readonly DatabaseManager dbManager;
        private readonly int userId;

        public Investment CreatedInvestment { get; private set; }

        public AddStockForm(DatabaseManager dbManager, int userId)
        {
            InitializeComponent();
            this.dbManager = dbManager;
            this.userId = userId;

            buttonSave.Click += buttonSave_ClickAsync;

            SetPlaceholder();

            textBoxName.TextChanged += TextBoxName_TextChanged;
            textBoxName.GotFocus += TextBoxName_GotFocus;
            textBoxName.LostFocus += TextBoxName_LostFocus;
        }


        private readonly string placeholderText = "Podaj ticker NASDAQ np. GOOGL";
        private bool isPlaceholderActive = true;

        private void SetPlaceholder()
        {
            isPlaceholderActive = true;
            textBoxName.ForeColor = Color.Gray;
            textBoxName.Text = placeholderText;
        }

        private void RemovePlaceholder()
        {
            if (isPlaceholderActive)
            {
                isPlaceholderActive = false;
                textBoxName.Text = "";
                textBoxName.ForeColor = Color.White;
            }
        }

        private void TextBoxName_TextChanged(object sender, EventArgs e)
        {
            if (!textBoxName.Focused)
            {
                // Jeśli pole nie jest aktywne i jest puste, pokaż placeholder
                if (string.IsNullOrEmpty(textBoxName.Text))
                {
                    SetPlaceholder();
                }
            }
        }

        private void TextBoxName_GotFocus(object sender, EventArgs e)
        {
            // Zawsze usuń placeholder, gdy zaczyna pisać (aktywny i pusty)
            if (isPlaceholderActive)
            {
                RemovePlaceholder();
            }
        }

        private void TextBoxName_LostFocus(object sender, EventArgs e)
        {
            // Jeśli po opuszczeniu pola jest puste, pokaż placeholder
            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                SetPlaceholder();
            }
        }

        public static DateTime GetLastTradingDay(DateTime date)
        {
            // Jeśli weekend, cofnij do piątku
            while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                date = date.AddDays(-1);
            }

            return date;
        }

        private async void buttonSave_ClickAsync(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxName.Text) || isPlaceholderActive)
            {
                MessageBox.Show("Podaj nazwę akcji (ticker).", "Wymagane", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(textBoxAmount.Text, out var amount))
            {
                MessageBox.Show("Podaj prawidłową liczbę akcji.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(textBoxExpectedReturn.Text, out var expectedReturn))
            {
                MessageBox.Show("Podaj prawidłowy oczekiwany zwrot.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtStopLoss.Text, out var stopLoss))
            {
                MessageBox.Show("Podaj prawidłowy poziom Stop Loss.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Walidacja logiczna
            if (expectedReturn <= 0)
            {
                MessageBox.Show("Oczekiwany zwrot musi być większy niż 0%.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (stopLoss >= 0)
            {
                MessageBox.Show("Stop Loss musi być wartością ujemną (np. -5).", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Pobranie typu inwestycji
            var stockType = dbManager.GetOrCreateStockInvestmentType();
            if (stockType == null)
            {
                MessageBox.Show("Nie znaleziono typu inwestycji 'Akcje' w bazie.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Pobranie ceny akcji
            DateTime selectedDate = dateTimePicker.Value.Date;
            if (selectedDate > DateTime.Today)
            {
                MessageBox.Show("Data inwestycji nie może być z przyszłości.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal? buyPrice;

            if (selectedDate < DateTime.Today)
            {
                // Jeśli data jest wcześniejsza niż dziś → użyj Polygon.io
                DateTime tradingDate = PolygonService.GetLastTradingDay(selectedDate);
                buyPrice = await PolygonService.GetHistoricalClosePriceAsync(textBoxName.Text.Trim(), tradingDate);
            }
            else
            {
                // Jeśli dziś → użyj Finnhub do pobrania bieżącej ceny
                buyPrice = await FinnhubService.GetCurrentQuoteAsync(textBoxName.Text.Trim());
            }

            if (buyPrice == null)
            {
                MessageBox.Show("Nie udało się pobrać ceny akcji dla wybranej daty.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Utworzenie inwestycji
            var investment = new Investment
            {
                Name = textBoxName.Text.Trim(),
                NumberOfShares = (int)amount,
                DateOfInvestment = dateTimePicker.Value,
                ExpectedReturnPercent = expectedReturn / 100m,
                StopLossPercent = stopLoss / 100m,
                Notes = textBoxNotes.Text,
                BuyPrice = buyPrice.Value,
                TypeId = stockType.Id,
                UserId = userId
            };

            CreatedInvestment = dbManager.AddInvestment(investment);

            MessageBox.Show("Inwestycja została dodana.");
            this.Close();
        }


        private void textBoxAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxExpectedReturn_TextChanged(object sender, EventArgs e)
        {

        }

        private async void btnCenaAkcji_ClickAsync(object sender, EventArgs e)
        {
            string ticker = textBoxName.Text.Trim().ToUpper();

            if (string.IsNullOrWhiteSpace(ticker) || isPlaceholderActive)
            {
                MessageBox.Show("Najpierw podaj symbol akcji (ticker).", "Brak danych", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime selectedDate = dateTimePicker.Value.Date;
            DateTime today = DateTime.Now.Date;

            try
            {
                decimal? price;

                if (selectedDate < today)
                {
                    // Pobierz historyczną cenę z Polygon.io
                    price = await PolygonService.GetHistoricalClosePriceAsync(ticker, selectedDate);
                    if (price == null)
                    {
                        MessageBox.Show($"Brak danych historycznych dla {ticker} z dnia {selectedDate:yyyy-MM-dd}.", "Brak danych", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    MessageBox.Show($"Cena zamknięcia dla {ticker} z dnia {selectedDate:yyyy-MM-dd}: {price:F2} USD", "Cena historyczna", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Pobierz aktualną cenę z Finnhub
                    price = await FinnhubService.GetCurrentQuoteAsync(ticker);
                    if (price == null || price <= 0)
                    {
                        MessageBox.Show($"Nie znaleziono aktualnych danych dla tickera: {ticker}.", "Błąd danych", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    MessageBox.Show($"Aktualna cena dla {ticker}: {price:F2} USD", "Cena bieżąca", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas pobierania danych: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
