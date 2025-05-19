#pragma warning disable CA1416 
using DatabaseConnection;
namespace Personal_Investment_App
{
    internal static class Program
    {
        /// <summary>  
        ///  The main entry point for the application.  
        /// </summary>  
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var dbManager = new DatabaseManager();

            while (true)
            {
                var loginForm = new LoginForm(dbManager);
                var dialogResult = loginForm.ShowDialog();

                if (dialogResult == DialogResult.OK)
                {
                    var form1 = new MainWindow(dbManager,loginForm.Username);

                    // G³ówne okno zwraca true jeœli u¿ytkownik klikn¹³ "Wyloguj"
                    Application.Run(form1);

                    if (!form1.Wylogowano)
                    {
                        break; // zakoñcz aplikacjê jeœli zamkniêto okno bez wylogowania
                    }
                }
                else
                {
                    break; // zamkniêto formularz logowania – zakoñcz aplikacjê
                }
            }
        }
    }
}