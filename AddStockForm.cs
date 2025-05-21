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

            buttonSave.Click += buttonSave_Click;

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

        private void buttonSave_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBoxName.Text) || isPlaceholderActive)
            {
                MessageBox.Show("Podaj nazwę akcji (ticker).", "Wymagane", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(textBoxAmount.Text, out var amount))
            {
                MessageBox.Show("Podaj prawidłową kwotę zainwestowaną.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(textBoxExpectedReturn.Text, out var expectedReturn))
            {
                MessageBox.Show("Podaj prawidłowy oczekiwany zwrot.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            // Retrieve InvestmentType for stocks
            var type = dbManager.GetOrCreateStockInvestmentType();
            if (type == null)
            {
                MessageBox.Show("Nie znaleziono typu inwestycji 'Akcje' w bazie.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var stockType = dbManager.GetOrCreateStockInvestmentType();

            var investment = new Investment
            {
                Name = textBoxName.Text.Trim(),
                AmountInvested = decimal.Parse(textBoxAmount.Text),
                DateOfInvestment = dateTimePicker.Value,
                ExpectedReturn = decimal.Parse(textBoxExpectedReturn.Text) / 100m,
                Notes = textBoxNotes.Text,
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
    }
}
