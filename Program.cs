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
            // To customize application configuration such as set high DPI settings or default font,  
            // see https://aka.ms/applicationconfiguration.  
            ApplicationConfiguration.Initialize();

            var dbManager = new DatabaseManager();
            var loginForm = new LoginForm(dbManager);

            Application.Run(loginForm);
        }
    }
}