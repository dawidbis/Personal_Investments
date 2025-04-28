using DatabaseConnection;

namespace ProgramLogic
{
    public class User
    {
        private DatabaseManager? dbManager = null;
        private List<ActiveInvestment> activeInvestments = new();
        private List<Investment> closedInvestments = new();

        public User(DatabaseManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public List<ActiveInvestment> ActiveInvestments
        {
            get { return activeInvestments; }
            set { activeInvestments = value; }
        }

        public List<Investment> ClosedInvestments
        {
            get { return closedInvestments; }
            set { closedInvestments = value; }
        }

        public void AddInvestment(ActiveInvestment investment)
        {
            activeInvestments.Add(investment);
        }

        public void RemoveActiveInvestment(int id)
        {
            var investmentToRemove = activeInvestments.FirstOrDefault(i => i.ID == id);
            if (investmentToRemove != null)
            {
                activeInvestments.Remove(investmentToRemove);
            }
        }

        public void RemoveClosedInvestment(int id)
        {
            var investmentToRemove = closedInvestments.FirstOrDefault(i => i.ID == id);
            if (investmentToRemove != null)
            {
                closedInvestments.Remove(investmentToRemove);
            }
        }
        public void CloseInvestment(int activeInvestmentId, decimal closingValue)
        {
            var activeInvestment = activeInvestments.FirstOrDefault(i => i.ID == activeInvestmentId);
            if (activeInvestment == null)
            {
                throw new InvalidOperationException("Active investment not found.");
            }

            // Utwórz zamkniętą inwestycję na podstawie aktywnej
            var closedInvestment = new Investment
            {
                ID = activeInvestment.ID,
                InvestmentID = activeInvestment.InvestmentID,
                Name = activeInvestment.Name,
                AmountInvested = activeInvestment.AmountInvested,
                DateOfInvestment = activeInvestment.DateOfInvestment,
                DateClosed = DateTime.Now,
                FinalProfit = closingValue - activeInvestment.AmountInvested
            };

            // Aktualizuj listy
            activeInvestments.Remove(activeInvestment);
            closedInvestments.Add(closedInvestment);

            // (opcjonalnie) aktualizuj bazę danych przez dbManager, np. dbManager.Save(closedInvestment);
        }

        //public void FillListBoxWithInvestments(ListBox listBox, bool showActiveInvestments)
        //{
        //    listBox.Items.Clear();

        //    if (showActiveInvestments)
        //    {
        //        foreach (var investment in activeInvestments)
        //        {
        //            listBox.Items.Add($"[AKTYWNA] {investment.Name} - Zainwestowano: {investment.AmountInvested:C} - Aktualna wartość: {investment.CurrentValue:C} - Zysk: {investment.Profit:C} ({investment.ProfitPercentage:F2}%)");
        //        }
        //    }
        //    else
        //    {
        //        foreach (var investment in closedInvestments)
        //        {
        //            listBox.Items.Add($"[ZAKOŃCZONA] {investment.Name} - Zainwestowano: {investment.AmountInvested:C} - Zysk: {investment.Profit:C} ({investment.ProfitPercentage:F2}%) - Data zamknięcia: {investment.DateClosed:yyyy-MM-dd}");
        //        }
        //    }
        //}
    }
}
