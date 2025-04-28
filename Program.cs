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

            using (var loginForm = new LoginForm(dbManager))
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new Form1(dbManager)); 
                }
                else
                {
                    Application.Exit();
                }
            }
        }
    }
}