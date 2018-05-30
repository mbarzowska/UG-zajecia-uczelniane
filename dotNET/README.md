# Technologie .NET

:older_man: Prowadzący: [dr Tomasz Borzyszkowski](https://github.com/tborzyszkowski)

## [LIBRARY_UnitTesting](LIBRARY-unit-testing)
```$xslt
1. Napisać projekt biblioteki (diagramy klas, przypadki użycia, opis poszczególnych funkcji)
2. Do projektu z punktu pierwszego stworzyć testy jednostkowe, które:
  - będą zawierały najmniej 3 różne klasy assercji
  - będą zawierały najmniej jeden Data-Driven Unit Test
  - wykorzystają Microsoft Fakes (stubs & shims)
3. Zaimplementować projekt tak by spełniał testy z punktu drugiego
```
## [ASPMVC_App&Testing](ASPMVC-app-and-testing)
### Aplikacja:
```$xslt
Modele:
- atrybuty dotyczące ograniczeń danych (własne walidatory)
- atrybuty dotyczące widoków
- minimum 2 tabele z relacją jeden-do-wiele

Kontrolery:
- powiązanie uprawnień zalogowanych userów z kontrolerami (atrybuty wykorzystujące role)
- logowanie i przynależność do ról
- LINQ

Widoki
- widoki pisane w Razor + przekazywanie danych do widoku
- umiejętność + przykład ręcznego rozszerzenia widoku
- widoki częściowe + komunikacja z widokiem podstawowym
- zastosowanie podstawowych helperów + definicja własnych
- zastosowanie layoutów
```


### Testy:
```$xslt
1. Zawierać projekt testowy z testami jednostkowymi dla modeli i kontrolerów.
2. Obsługa przez ASP MVC nieprzewidzianych wyjątków.
3. W testach kontrolerów należy zademonstrować metody izolacji kodu.
  - wykorzystanie Repository Test Double opartych na własnym interfejsie repozytorium i wykorzystaniu
  konstruktorów do wyspecyfikowania repozytorium.
  - wykorzystanie jednego z frameworków IoC dla ASP MVC (StructureMap,  RhinoMocks i NSubstitute  lub Moq).
4. Wykorzystanie Selenium WebDriver oraz Coded UI do testowania interfejsu użytkownika.
```

## [XAMARIN.FORMS_app](XAMARIN.FORMS-app)
### Wymagania dotyczące zaliczenia na ocenę:
```$xslt
- zastosowanie pełnego wzorca MVVM - binding wartości oraz "command"
- adaptatywny interface użytkownika - dotyczy zarówno geometrii jak i systemu operacyjnego
- wykorzystanie zdarzeń związanych z cyklem życia aplikacji - OnStart, OnSleep, OnResume
- wykorzystanie konstrukcji "DependencyService" do ujednolicenia asynchronicznego dostępu do plików
- dodatkowe właściwości (dwie z wymienionych): 
	- nawigacja stron z przekazywaniem wartości
	- walidacja danych na formularzach
	- zarządzanie konfiguracją aplikacji
	- dostęp do danych zdalnych (np.: REST) i wykorzystanie pamięci podręcznej (cache)
- usability
```