# HardkorowyKodsu

HardkorowyKodsu to przykładowa wieloprojektowa aplikacja w języku C#, składająca się z:

- **HardkorowyKodsu.Server** – serwer (ASP.NET Core Web API) do przeglądania struktury bazy danych (tabele, widoki, kolumny).
- **HardkorowyKodsu.Client** – aplikacja Windows Forms komunikująca się z serwerem (REST API) i wyświetlająca strukturę bazy.
- **HardkorowyKodsu.Tests** – projekt testowy (XUnit), zawierający przykładowe testy jednostkowe (m.in. z użyciem EF Core InMemory).

---

## Spis treści

1. Opis
2. Struktura rozwiązania
3. Funkcjonalności
4. Uwagi i dodatkowe informacje

---

## Opis

Głównym celem projektu jest pokazanie, jak:

- Zbudować ASP.NET Core Web API z użyciem Entity Framework Core (np. SQL Server).
- Rozdzielić odpowiedzialność na warstwę kontrolera, serwisową (business logic) oraz repozytoriów (dostęp do bazy).
- Zaimplementować globalny middleware obsługi wyjątków.
- Włączyć dokumentację Swagger (OpenAPI).
- Skorzystać z walidacji (m.in. Data Annotations, [ApiController]) i asynchroniczności (async/await).
- Napisać testy jednostkowe z wykorzystaniem XUnit i EF Core InMemory.
- Zaprezentować prostą aplikację kliencką Windows Forms komunikującą się z Web API.

---

## Struktura rozwiązania

```arduino
HardkorowyKodsu.sln
HardkorowyKodsu.Server/
 ├ Controllers/
 ┃  └ DatabaseSchemaController.cs
 ├ Data/
 ┃  ├ HardkorowyKodsuDbContext.cs
 ┃  ├ IDatabaseSchemaRepository.cs
 ┃  └ DatabaseSchemaRepository.cs
 ├ Middlewares/
 ┃  ├ GlobalExceptionMiddleware.cs
 ┃  └ GlobalExceptionMiddlewareExtensions.cs
 ├ Models/
 ┃  ├ DatabaseObject.cs
 ┃  └ ColumnInfo.cs
 ├ Services/
 ┃  ├ IDatabaseSchemaService.cs
 ┃  └ DatabaseSchemaService.cs
 ├ appsettings.json
 ├ Program.cs
 └ HardkorowyKodsu.Server.csproj

HardkorowyKodsu.Client/
 ├ Forms/
 ┃  └ MainForm.cs
 ├ Program.cs
 └ HardkorowyKodsu.Client.csproj

HardkorowyKodsu.Tests/
 ├ DatabaseSchemaControllerTests.cs
 └ HardkorowyKodsu.Tests.csproj
```

### Opis struktury:

- **HardkorowyKodsu.Server**
  - **Controllers** – klasa kontrolera Web API (np. DatabaseSchemaController).
  - **Data** – klasa kontekstu EF Core oraz repozytoria.
  - **Middlewares** – kod odpowiedzialny za globalną obsługę wyjątków.
  - **Models** – definicje modeli używanych do przechowywania danych.
  - **Services** – warstwa serwisowa.
  - **Program.cs** – plik startowy konfigurujący DI, middleware, Swagger i kontrolery.
  - **appsettings.json** – plik konfiguracyjny.

- **HardkorowyKodsu.Client**
  - **Forms** – formularze Windows Forms.
  - **Program.cs** – punkt startowy aplikacji.

- **HardkorowyKodsu.Tests**
  - **DatabaseSchemaControllerTests.cs** – testy jednostkowe z XUnit i EF Core InMemory.

---

## Funkcjonalności

- **Entity Framework Core** – obsługa bazy danych (pobieranie nazw tabel, widoków, kolumn).
- **Globalna obsługa wyjątków** – własne middleware.
- **Swagger/OpenAPI** – automatyczna dokumentacja endpointów.
- **Walidacja parametrów** – [Required], [MinLength(1)].
- **Aplikacja klienta Windows Forms** – obsługuje błędy z serwera i wyświetla dane.
- **Testy XUnit** – przykłady testów jednostkowych.

---

## Uwagi i dodatkowe informacje

- **Middleware** – przechwytuje wyjątki i zwraca błędy w formacie JSON.
- **Repozytoria** – realizują konkretne zapytania SQL.
- Możliwość rozbudowy (np. cache, autoryzacja).
