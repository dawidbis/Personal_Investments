# 📈 Personal Investment Tracker

Nowoczesna aplikacja desktopowa (.NET 8 WinForms) do kompleksowego zarządzania portfelem inwestycyjnym. System umożliwia śledzenie akcji, kryptowalut oraz surowców w czasie rzeczywistym dzięki integracji z wieloma dostawcami danych giełdowych.

## 🚀 Kluczowe cechy projektu

- **Architektura N-Tier**: Projekt podzielony na logiczne warstwy (Core, Data, UI) dla zachowania zasady Single Responsibility.
- **Dependency Injection (DI)**: Pełne wykorzystanie kontenera wstrzykiwania zależności (`Microsoft.Extensions.DependencyInjection`) dla formularzy i serwisów.
- **Integracja z API**: Obsługa trzech profesjonalnych dostawców danych:
  - **Finnhub**: Ceny akcji i krypto w czasie rzeczywistym.
  - **Polygon.io**: Dane historyczne i agregaty giełdowe.
  - **Twelve Data**: Ceny surowców i metali szlachetnych.
- **Smart Portfolio Management**:
  - Automatyczne sprawdzanie warunków sprzedaży (Profit/Stop-Loss).
  - Dynamiczne obliczanie zysku/straty (P/L) z uwzględnieniem kosztów zakupu.
  - System "Fallback" – stabilność aplikacji nawet przy limitach zapytań API.
- **Bezpieczeństwo & Dane**: 
  - Hashowanie haseł (PBKDF2).
  - Przechowywanie danych w SQLite (EF Core).
  - Import/Eksport danych do formatu JSON.

## 🛠 Technologie

- **Backend**: C# 12, .NET 8
- **ORM**: Entity Framework Core (SQLite)
- **Komunikacja**: HttpClientFactory, Newtonsoft.Json
- **Konfiguracja**: IConfiguration (appsettings.json)

## 🏗 Struktura folderów API

Zastosowano modularny podział dostawców danych:
- `Api/Finnhub/` - Obsługa akcji i krypto (Real-time).
- `Api/Polygon/` - Obsługa danych historycznych.
- `Api/TwelveData/` - Obsługa surowców.

## ⚙️ Uruchomienie lokalne

1. Sklonuj repozytorium.
2. W folderze projektu `Personal-Investment.UI` utwórz plik `appsettings.json` na bazie dostarczonego `appsettings.Example.json`.
3. Uzupełnij własne klucze API.
4. Uruchom projekt (Baza danych zostanie utworzona automatycznie przy pierwszym starcie).