# HardkorowyKodsu

HardkorowyKodsu to przykładowa wieloprojektowa aplikacja w języku C#, składająca się
z:

1. HardkorowyKodsu.Server – serwer (ASP.NET Core Web API) do przeglądania
    struktury bazy danych (tabele, widoki, kolumny).
2. HardkorowyKodsu.Client – aplikacja Windows Forms komunikująca się z serwerem
    (REST API) i wyświetlająca strukturę bazy.
3. HardkorowyKodsu.Tests – projekt testowy (XUnit), zawierający przykładowe testy
    jednostkowe (m.in. z użyciem EF Core InMemory).

## Spis treści

```
 Opis
 Struktura rozwiązania
 Funkcjonalności
 Wymagania
 Konfiguracja i uruchomienie
o Serwer (HardkorowyKodsu.Server)
o Klient (HardkorowyKodsu.Client)
 Testy jednostkowe
 Uwagi i dodatkowe informacje
 Licencja
```
## Opis

Głównym celem projektu jest pokazanie, jak:

```
 Zbudować ASP.NET Core Web API z użyciem Entity Framework Core (np. SQL
Server).
 Rozdzielić odpowiedzialność na warstwę kontrolera , warstwę serwisową (business
logic) oraz warstwę repozytoriów (dostęp do bazy).
 Zaimplementować globalny middleware obsługi wyjątków (global exception
handling).
 Włączyć dokumentację Swagger (OpenAPI).
 Skorzystać z walidacji (m.in. Data Annotations, [ApiController]) i
asynchroniczności (async/await).
 Napisanie testów jednostkowych z wykorzystaniem XUnit i EF Core InMemory.
 Zaprezentować prostą aplikację kliencką Windows Forms, komunikującą się z Web
API i wyświetlającą strukturę bazy.
```
## Struktura rozwiązania


arduino
Copy
HardkorowyKodsu.sln
HardkorowyKodsu.Server/
┣ Controllers/
┃ ┗ DatabaseSchemaController.cs
┣ Data/
┃ ┣ HardkorowyKodsuDbContext.cs
┃ ┣ IDatabaseSchemaRepository.cs
┃ ┗ DatabaseSchemaRepository.cs
┣ Middlewares/
┃ ┣ GlobalExceptionMiddleware.cs
┃ ┗ GlobalExceptionMiddlewareExtensions.cs
┣ Models/
┃ ┣ DatabaseObject.cs
┃ ┗ ColumnInfo.cs
┣ Services/
┃ ┣ IDatabaseSchemaService.cs
┃ ┗ DatabaseSchemaService.cs
┣ appsettings.json
┣ Program.cs
┗ HardkorowyKodsu.Server.csproj

HardkorowyKodsu.Client/
┣ Forms/
┃ ┗ MainForm.cs
┣ Program.cs
┗ HardkorowyKodsu.Client.csproj

HardkorowyKodsu.Tests/
┣ DatabaseSchemaControllerTests.cs
┗ HardkorowyKodsu.Tests.csproj

### HardkorowyKodsu.Server

```
 Controllers – klasa kontrolera Web API (np. DatabaseSchemaController),
przyjmująca żądania HTTP.
 Data – klasa kontekstu EF Core (HardkorowyKodsuDbContext) oraz repozytoria
(interfejs i implementacja).
 Middlewares – kod odpowiedzialny za globalną obsługę wyjątków.
 Models – definicje modeli używanych do przechowywania danych (np.
DatabaseObject, ColumnInfo).
 Services – warstwa serwisowa (np. DatabaseSchemaService), gdzie znajduje się
logika aplikacyjna.
 Program.cs – główny plik startowy dla aplikacji ASP.NET Core, zawierający
konfigurację DI, middleware (w tym global exception handler), Swaggera i
mapowanie kontrolerów.
 appsettings.json – plik konfiguracyjny (np. connection string).
```
### HardkorowyKodsu.Client


```
 Forms – formularze Windows Forms (np. MainForm.cs), zawierające logikę
interfejsu użytkownika (ładowanie tabel, wyświetlanie kolumn itd.).
 Program.cs – punkt startowy aplikacji WinForms.
```
### HardkorowyKodsu.Tests

```
 DatabaseSchemaControllerTests.cs – przykładowy zestaw testów jednostkowych
wykorzystujących XUnit.
 Możliwość użycia EF Core InMemory dla testów, aby nie potrzebować realnej bazy
w trakcie testów jednostkowych.
```
## Funkcjonalności

1. Entity Framework Core (SQL Server) – obsługa bazy danych (pobieranie nazw
    tabel, widoków, kolumn).
2. Warstwa repozytorium – logika dostępu do bazy (zapytania, FromSqlRaw).
3. Warstwa serwisowa – logika biznesowa (np. sprawdzanie istnienia obiektu, obsługa
    wyjątków).
4. **Globalna obsługa wyjątków** (własne middleware).
5. Swagger/OpenAPI – automatyczna dokumentacja endpointów.
6. Walidacja parametrów (np. [Required], [MinLength(1)]) + [ApiController].
7. Asynchroniczne metody (async/await) od repozytoriów po kontrolery.
8. Aplikacja klienta Windows Forms – wczytuje listę tabel/widoków, wyświetla
    kolumny, obsługuje błędy z serwera.
9. Testy XUnit – przykłady testów jednostkowych (kontrolerów, repozytoriów,
    serwisów) z wykorzystaniem bazy InMemory.

## Wymagania

```
 .NET 7.0 SDK (lub wersja zgodna z .csproj).
 Opcjonalnie Visual Studio 2022 / VS Code / JetBrains Rider.
 Działająca instancja SQL Server (lokalna lub zdalna) w przypadku korzystania z
realnych danych (np. AdventureWorks).
o W pliku appsettings.json wskazany jest przykładowy connection string do
bazy AdventureWorks. Upewnij się, że pasuje do Twojej konfiguracji.
```
## Konfiguracja i uruchomienie

1. Klonowanie / pobranie
    o Sklonuj repozytorium (lub pobierz paczkę ZIP) z całą strukturą.
2. **Otwórz rozwiązanie** HardkorowyKodsu.sln w wybranym IDE.
3. Przygotuj connection string


```
o W HardkorowyKodsu.Server/appsettings.json dopasuj sekcję
"ConnectionStrings": { "DefaultConnection": "..." } do swojej bazy
danych SQL Server.
```
### Serwer (HardkorowyKodsu.Server)

1. Przejdź do katalogu HardkorowyKodsu.Server/ (lub ustaw ten projekt jako startowy
    w IDE).
2. Uruchom:

```
bash
Copy
dotnet run
```
```
Domyślnie serwer wystartuje na http://localhost:5000 i
https://localhost:5001.
```
3. Swagger – przejdź w przeglądarce do https://localhost:5001/swagger (lub
    [http://localhost:5000/swagger),](http://localhost:5000/swagger),) aby zobaczyć interaktywną dokumentację i móc
    testować endpointy.

### Klient (HardkorowyKodsu.Client)

1. Przejdź do folderu HardkorowyKodsu.Client/.
2. Upewnij się, że w pliku MainForm.cs (konstruktor lub Load) wartość BaseAddress w
    HttpClient jest dostosowana do adresu, na którym działa Twój serwer:

```
csharp
Copy
_httpClient = new HttpClient
{
BaseAddress = new Uri("https://localhost:5001")
};
```
3. Uruchom aplikację (np. dotnet run lub poprzez IDE).
4. Po uruchomieniu okna głównego – MainForm:
    o Wczytane zostaną nazwy obiektów w bazie (tabele, widoki).
    o Po wybraniu obiektu z listy, wczytane i wyświetlone zostaną kolumny w
       kontrolce DataGridView.

## Testy jednostkowe

1. Przejdź do katalogu głównego projektu (gdzie jest HardkorowyKodsu.sln).
2. Wykonaj w konsoli:

```
bash
Copy
dotnet test
```

3. Projekt HardkorowyKodsu.Tests zawiera przykładowe testy XUnit, w tym:
    o DatabaseSchemaControllerTests – testy kontrolera z użyciem EF Core
       InMemory (symulacja bazy).

## Uwagi i dodatkowe informacje

```
 GlobalExceptionMiddleware – przechwytuje wszystkie nieobsłużone wyjątki i
zwraca spójny format błędu w JSON.
 Data Annotations + [ApiController] – w razie niepoprawnych parametrów
wejściowych (np. puste objectName), serwer zwraca automatycznie 400 Bad
Request.
 Warstwa serwisowa – zawiera dodatkową logikę (np. sprawdzanie, czy obiekt
istnieje w bazie). W przypadku problemów rzuca ArgumentException (lub inny
wyjątek), przechwytywany globalnie.
 Repozytoria – realizują konkretne zapytania SQL (np. FromSqlRaw) do obiektów
systemowych (sys.objects, INFORMATION_SCHEMA.COLUMNS).
 Możliwość rozbudowy – Kod można wzbogacić o dodatkowe funkcje, np. cache,
obsługę transakcji, autoryzację/tokeny JWT, itp.
```
## Licencja

Projekt ten to przykład poglądowy. Możesz go dowolnie modyfikować, kopiować lub
wykorzystywać do celów edukacyjnych i komercyjnych. Jeżeli chcesz upublicznić swoją
wersję, rozważ dodanie pliku z wybraną otwartą licencją (np. MIT, Apache 2.0).

`(c) 2023 HardkorowyKodsu – Przykładowa aplikacja klient-serwer w C#`


