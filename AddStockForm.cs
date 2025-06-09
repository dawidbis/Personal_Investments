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
    public enum InvestmentKind
    {
        Akcja,
        Kryptowaluta
    }

    public partial class AddStockForm : Form
    {
        private readonly DatabaseManager dbManager;
        private readonly int userId;

        public Investment CreatedInvestment { get; private set; }

        private readonly InvestmentKind investmentKind;

        public AddStockForm(DatabaseManager dbManager, int userId, InvestmentKind kind)
        {
            InitializeComponent();
            this.dbManager = dbManager;
            this.userId = userId;
            this.investmentKind = kind;

            buttonSave.Click += buttonSave_ClickAsync;

            UpdateFormLabels(); // najpierw ustawiamy placeholderText
            SetPlaceholder();   // potem ustawiamy go w polu tekstowym

            textBoxName.TextChanged += TextBoxName_TextChanged;
            textBoxName.GotFocus += TextBoxName_GotFocus;
            textBoxName.LostFocus += TextBoxName_LostFocus;

            UpdateFormLabels(); // <-- aktualizacja etykiet zależnie od typu inwestycji
        }



        private string placeholderText;
        private bool isPlaceholderActive = true;

        private void UpdateFormLabels()
        {
            if (investmentKind == InvestmentKind.Kryptowaluta)
            {
                placeholderText = "Podaj ticker Binance np. BTC";
                lblName.Text = "Ticker kryptowaluty:";
                lblAmount.Text = "Ilość (np. 0.5 BTC):";
                lblExpectedReturn.Text = "Oczekiwany zwrot (%):";
                lblDate.Text = "Data zakupu:";
                lblNotes.Text = "Notatki:";
                label1.Text = "Stop Loss (%):";
                btnCenaAkcji.Text = "Sprawdź Cenę Krypto";
                this.Text = "Dodaj kryptowalutę";
            }
            else
            {
                placeholderText = "Podaj ticker NASDAQ np. GOOGL";
                lblName.Text = "Ticker akcji:";
                lblAmount.Text = "Liczba akcji:";
                lblExpectedReturn.Text = "Oczekiwany zwrot (%):";
                lblDate.Text = "Data inwestycji:";
                lblNotes.Text = "Notatki:";
                label1.Text = "Stop Loss (%):";
                btnCenaAkcji.Text = "Sprawdź Cenę Akcji";
                this.Text = "Dodaj akcję";
            }

            SetPlaceholder();
        }
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
                MessageBox.Show($"Podaj nazwę {(investmentKind == InvestmentKind.Akcja ? "akcji (ticker)" : "kryptowaluty")}.", "Wymagane", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(textBoxAmount.Text, out var amount))
            {
                MessageBox.Show($"Podaj prawidłową liczbę {(investmentKind == InvestmentKind.Akcja ? "akcji" : "jednostek kryptowaluty")}.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            // Wybór typu inwestycji
            InvestmentType investmentType = investmentKind switch
            {
                InvestmentKind.Akcja => dbManager.GetOrCreateStockInvestmentType(),
                InvestmentKind.Kryptowaluta => dbManager.GetOrCreateCryptoInvestmentType(),
                _ => null
            };

            if (investmentType == null)
            {
                MessageBox.Show("Nie znaleziono typu inwestycji w bazie.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime selectedDate = dateTimePicker.Value.Date;
            if (selectedDate > DateTime.Today)
            {
                MessageBox.Show("Data inwestycji nie może być z przyszłości.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal? buyPrice = null;
            string symbol = textBoxName.Text.Trim();

            if (investmentKind == InvestmentKind.Akcja)
            {
                if (selectedDate < DateTime.Today)
                {
                    DateTime tradingDate = PolygonService.GetLastTradingDay(selectedDate);
                    buyPrice = await PolygonService.GetHistoricalClosePriceAsync(symbol, tradingDate);
                }
                else
                {
                    buyPrice = await FinnhubService.GetCurrentQuoteAsync(symbol);
                }
            }
            else if (investmentKind == InvestmentKind.Kryptowaluta)
            {
                if (selectedDate < DateTime.Today)
                {
                    buyPrice = await PolygonService.GetHistoricalCryptoClosePriceAsync(symbol,selectedDate);
                }
                else
                {
                    buyPrice = await FinnhubService.GetCurrentCryptoQuoteAsync(symbol);
                }
            }

            if (buyPrice == null)
            {
                MessageBox.Show($"Nie udało się pobrać ceny {(investmentKind == InvestmentKind.Akcja ? "akcji" : "kryptowaluty")} dla wybranej daty.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var investment = new Investment
            {
                Name = symbol,
                NumberOfShares = amount,
                DateOfInvestment = selectedDate,
                ExpectedReturnPercent = expectedReturn / 100m,
                StopLossPercent = stopLoss / 100m,
                Notes = textBoxNotes.Text,
                BuyPrice = buyPrice.Value,
                TypeId = investmentType.Id,
                UserId = userId
            };

            CreatedInvestment = dbManager.AddInvestment(investment);

            MessageBox.Show($"{(investmentKind == InvestmentKind.Akcja ? "Inwestycja w akcję" : "Inwestycja w kryptowalutę")} została dodana.");
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
                MessageBox.Show("Najpierw podaj symbol instrumentu.", "Brak danych", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime selectedDate = dateTimePicker.Value.Date;
            DateTime today = DateTime.Now.Date;

            try
            {
                decimal? price = null;

                if (selectedDate < today)
                {
                    // Pobierz cenę historyczną w zależności od rodzaju inwestycji
                    price = investmentKind switch
                    {
                        InvestmentKind.Akcja => await PolygonService.GetHistoricalClosePriceAsync(ticker, selectedDate),
                        InvestmentKind.Kryptowaluta => await PolygonService.GetHistoricalCryptoClosePriceAsync(ticker, selectedDate),
                        _ => null
                    };

                    if (price == null)
                    {
                        MessageBox.Show($"Brak danych historycznych dla {ticker} z dnia {selectedDate:yyyy-MM-dd}.", "Brak danych", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    MessageBox.Show($"Cena z dnia {selectedDate:yyyy-MM-dd} dla {ticker}: {price:F2} USD", "Cena historyczna", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Pobierz aktualną cenę
                    price = investmentKind switch
                    {
                        InvestmentKind.Akcja => await FinnhubService.GetCurrentQuoteAsync(ticker),
                        InvestmentKind.Kryptowaluta => await FinnhubService.GetCurrentCryptoQuoteAsync(ticker),
                        _ => null
                    };

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
