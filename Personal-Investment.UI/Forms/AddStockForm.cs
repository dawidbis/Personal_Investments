using Personal_Investment.Core.Api.Finnhub;
using Personal_Investment.Core.Api.Polygon;
using Personal_Investment.Core.Api.TwelveData;
using Personal_Investment.Core.Enums;
using Personal_Investment.Core.Models;
using Personal_Investment.Data.Services;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personal_Investment.UI.Forms;

public partial class AddStockForm : Form
{
    private readonly InvestmentService _investmentService;
    private readonly FinnhubService _finnhub;
    private readonly PolygonService _polygon;
    private readonly TwelveDataService _twelveData;

    private int _userId;
    private InvestmentKind _investmentKind;
    private string placeholderText = string.Empty;
    private bool isPlaceholderActive = true;

    public Investment? CreatedInvestment { get; private set; }

    // JEDEN konstruktor dla wszystkiego - WinForms i DI to udźwigną
    public AddStockForm(
        InvestmentService investmentService,
        FinnhubService finnhub,
        PolygonService polygon,
        TwelveDataService twelveData)
    {
        InitializeComponent();

        _investmentService = investmentService;
        _finnhub = finnhub;
        _polygon = polygon;
        _twelveData = twelveData;
    }

    public void SetMode(InvestmentKind kind, int userId)
    {
        _investmentKind = kind;
        _userId = userId;

        UpdateFormLabels();
        SetPlaceholder();

        // Podpinamy zdarzenia focusu
        textBoxName.GotFocus += (s, e) => RemovePlaceholder();
        textBoxName.LostFocus += (s, e) => { if (string.IsNullOrWhiteSpace(textBoxName.Text)) SetPlaceholder(); };
    }

    private void UpdateFormLabels()
    {
        switch (_investmentKind)
        {
            case InvestmentKind.Kryptowaluta:
                placeholderText = "Np. BTC";
                lblName.Text = "Ticker kryptowaluty:";
                btnCenaAkcji.Text = "Sprawdź Cenę Krypto";
                this.Text = "Dodaj kryptowalutę";
                break;
            case InvestmentKind.Surowiec:
                placeholderText = "Np. XAU/USD";
                lblName.Text = "Ticker surowca:";
                btnCenaAkcji.Text = "Sprawdź Cenę Surowca";
                this.Text = "Dodaj surowiec";
                break;
            default:
                placeholderText = "Np. AAPL";
                lblName.Text = "Ticker akcji:";
                btnCenaAkcji.Text = "Sprawdź Cenę Akcji";
                this.Text = "Dodaj akcję";
                break;
        }
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

    private async void buttonSave_Click(object sender, EventArgs e)
    {
        if (!ValidateForm()) return;

        try
        {
            Cursor = Cursors.WaitCursor;
            string symbol = textBoxName.Text.Trim().ToUpper();
            DateTime selectedDate = dateTimePicker.Value.Date;

            decimal? buyPrice = await FetchPriceAsync(symbol, selectedDate);

            if (buyPrice == null)
            {
                MessageBox.Show("Nie udało się pobrać ceny dla wybranego instrumentu/daty.", "Błąd API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var investment = new Investment
            {
                Name = symbol,
                NumberOfShares = decimal.Parse(textBoxAmount.Text),
                DateOfInvestment = selectedDate,
                ExpectedReturnPercent = decimal.Parse(textBoxExpectedReturn.Text) / 100m,
                StopLossPercent = decimal.Parse(txtStopLoss.Text) / 100m,
                Notes = textBoxNotes.Text,
                BuyPrice = buyPrice.Value,
                UserId = _userId
            };

            CreatedInvestment = await _investmentService.AddInvestmentAsync(investment, _investmentKind);

            MessageBox.Show("Inwestycja została pomyślnie dodana!", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Wystąpił błąd: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            Cursor = Cursors.Default;
        }
    }

    private async Task<decimal?> FetchPriceAsync(string symbol, DateTime date)
    {
        bool isHistorical = date < DateTime.Today;

        // KLUCZOWA ZMIANA: używamy _finnhub, _polygon, _twelveData zamiast nazw klas
        return _investmentKind switch
        {
            InvestmentKind.Akcja => isHistorical
                ? await _polygon.GetHistoricalClosePriceAsync(symbol, date)
                : await _finnhub.GetCurrentQuoteAsync(symbol),

            InvestmentKind.Kryptowaluta => isHistorical
                ? await _polygon.GetHistoricalCryptoClosePriceAsync(symbol, date)
                : await _finnhub.GetCurrentCryptoQuoteAsync(symbol),

            InvestmentKind.Surowiec => isHistorical
                ? await _twelveData.GetHistoricalClosePriceAsync(symbol, date)
                : await _twelveData.GetTodayClosePriceAsync(symbol),

            _ => null
        };
    }

    private bool ValidateForm()
    {
        if (string.IsNullOrWhiteSpace(textBoxName.Text) || isPlaceholderActive)
        {
            MessageBox.Show("Proszę podać symbol instrumentu.", "Brak danych", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        bool isAmountOk = decimal.TryParse(textBoxAmount.Text, out decimal amount) && amount > 0;
        bool isReturnOk = decimal.TryParse(textBoxExpectedReturn.Text, out _);
        bool isStopLossOk = decimal.TryParse(txtStopLoss.Text, out decimal sl) && sl < 0;

        if (!isAmountOk || !isReturnOk || !isStopLossOk)
        {
            MessageBox.Show("Wprowadzone liczby są nieprawidłowe. Pamiętaj: Stop Loss musi być ujemny.", "Błąd walidacji", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        return true;
    }

    private async void btnCenaAkcji_ClickAsync(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(textBoxName.Text) || isPlaceholderActive) return;

        Cursor = Cursors.WaitCursor;
        var price = await FetchPriceAsync(textBoxName.Text.Trim(), dateTimePicker.Value.Date);
        Cursor = Cursors.Default;

        if (price.HasValue)
            MessageBox.Show($"Cena dla {textBoxName.Text.ToUpper()}: {price.Value:F2} USD");
        else
            MessageBox.Show("Nie znaleziono ceny.");
    }
}