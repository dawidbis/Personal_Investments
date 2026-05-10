using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Personal_Investment.Core.Api.Finnhub;
using Personal_Investment.Core.Api.Polygon;
using Personal_Investment.Core.Api.TwelveData;
using Personal_Investment.Core.Security;
using Personal_Investment.Data.DatabaseConnection;
using Personal_Investment.Data.Services;
using Personal_Investment.UI.Forms;
using System;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Forms.Application;

namespace Personal_Investment.UI;

static class Program
{
    public static IServiceProvider ServiceProvider { get; private set; }
    private static IConfiguration Configuration;

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.SetHighDpiMode(HighDpiMode.SystemAware);

        var builder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        Configuration = builder.Build();

        // Konfiguracja kontenera us³ug
        var services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider = services.BuildServiceProvider();

        while (true)
        {
            // Pobieramy LoginForm z DI (on ma wstrzykniêty AuthService)
            using (var scope = ServiceProvider.CreateScope())
            {
                var loginForm = scope.ServiceProvider.GetRequiredService<LoginForm>();
                var dialogResult = loginForm.ShowDialog();

                if (dialogResult == DialogResult.OK)
                {
                    // Pobieramy MainWindow z DI
                    var mainWindow = scope.ServiceProvider.GetRequiredService<MainWindow>();

                    mainWindow.SetUser(loginForm.Username, loginForm.ZalogowanyUserId);

                    Application.Run(mainWindow);

                    // Jeœli u¿ytkownik zamkn¹³ okno bez wylogowania, wychodzimy z pêtli
                    if (!mainWindow.Wylogowano)
                    {
                        break;
                    }
                }
                else
                {
                    break; // Zamkniêto logowanie
                }
            }
        }
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(Configuration);

        // Rejestracja serwisów API wraz z HttpClientami
        services.AddHttpClient<FinnhubService>();
        services.AddHttpClient<PolygonService>();
        services.AddHttpClient<TwelveDataService>();
        services.AddScoped<InvestmentService>();

        // 1. Rejestracja bazy danych (SQLite jest najlepszy do portfolio - baza to jeden plik)
        // Usun¹³em zdublowane wywo³ania i podstawi³em SQLite
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("Data Source=investments.db"));

        // 2. Rejestracja logiki biznesowej
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<AuthService>();
        services.AddScoped<InvestmentService>();
        services.AddScoped<DataExportService>(); // Pamiêtaj o dodaniu serwisu eksportu!

        // 3. Rejestracja formularzy (jako Transient, bo UI tworzymy na ¿¹danie)
        services.AddTransient<LoginForm>();
        services.AddTransient<RegisterForm>();
        services.AddTransient<MainWindow>();
        services.AddTransient<ForgotPassword>();
        services.AddTransient<ChangePasswordForm>();
        services.AddTransient<AddStockForm>();
        services.AddTransient<ReportForm>();
    }
}