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

                    // G��wne okno zwraca true je�li u�ytkownik klikn�� "Wyloguj"
                    Application.Run(form1);

                    if (!form1.Wylogowano)
                    {
                        break; // zako�cz aplikacj� je�li zamkni�to okno bez wylogowania
                    }
                }
                else
                {
                    break; // zamkni�to formularz logowania � zako�cz aplikacj�
                }
            }
        }
    }
}