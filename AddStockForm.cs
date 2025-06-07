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

namespace Personal_Investment_App
{
    public partial class AddStockForm : Form
    {
        private readonly DatabaseManager dbManager;
        private readonly int userId;
        private readonly bool useMockPrice;

        public Investment CreatedInvestment { get; private set; }

        public AddStockForm(DatabaseManager dbManager, int userId, bool useMockPrice = false)
        {
            InitializeComponent();
            this.dbManager = dbManager;
            this.userId = userId;
            this.useMockPrice = useMockPrice;

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
            decimal? buyPrice = await FinnhubService.GetCurrentQuoteAsync(textBoxName.Text.Trim());
            if (buyPrice == null)
            {
                MessageBox.Show("Nie udało się pobrać ceny akcji.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            try
            {
                decimal? close = await FinnhubService.GetCurrentQuoteAsync(ticker);

                if (close == null || close <= 0)
                {
                    MessageBox.Show($"Nie znaleziono danych dla podanego tickera: {ticker}. Upewnij się, że symbol jest poprawny.", "Błędny ticker", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show($"Aktualna cena zamknięcia dla {ticker}: {close} USD", "Cena akcji", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas pobierania danych: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
