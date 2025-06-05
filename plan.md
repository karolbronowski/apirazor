# Podstawa

Templatka startowa z clean architecture (onion)

```
https://github.com/koglaza/pab-lab/tree/main/LAB01/CleanArchitectureProject
```
oparte na
```
https://github.com/jasontaylordev/CleanArchitecture
```

# Zakres materiału przygotowawczo-pomocniczego

### Lab 01 ONION (clean architecture)
```
https://code-maze.com/onion-architecture-in-aspnetcore/
```

### Lab 02 - REST 

- based on ASP.NET Core minimal & controller-based APIs

- bardzo przydatne odniesienie do kodów któe ma api zwracać, na rekru zwracali na to uwagę

- w specyfikacji projektu wymieniono ASP.NET Core Web Api zamiast core minimal

```
https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-9.0
https://koglaza.gitbook.io/backend/lab-02/rest-api
```


### Lab 04 - JWT

implementacja JWT 

```
https://koglaza.gitbook.io/backend/lab-04/jwt
```

### Lab 05 - Razor Pages

Przykład strony LOGOWANIA :) (można użyć ponownie więc...)
```
https://github.com/koglaza/pab-lab/tree/main/LAB05
# Zwróć uwagę na:
# - Bind property
# - HTTPContext.SignInAsync
# - CookiePolicyOptions, AddAuthentication
```

### Lab 06 - Entity Framework

Baza danych w pamięci poczytaj na necie 

# Plan pracy (3 projekty):

## Termin: 20 czerwca - REALNY TERMIN: 12 czerwca... (13-22 outuję na erasmusie)

## Projekt 1 REST API

### Domena: Spotify

Encje: Song, Artist, Listener

- [] Song:
```
interface Song {
  id: string;
  title: string;
  artistId: string;
  listenedTimes: number;
  createdAt: Date;
  updatedAt: Date;
}
```
- [] Artist:
```
interface Artist {
  id: string;
  name: string;
  centsPerThousandListeners: number;
  createdAt: Date;
  updatedAt: Date;
}
```
- [] Listener:
```
interface Listener {
  id: string;
  name: string;
  favouriteSongsIds: Song['id'][]
  createdAt: Date;
  updatedAt: Date;
}
```

### Middleware

Obsługa nagłówków HTTP (zgaduję że jedyną rolą tego middleware więc ma być przypisanei odpowiednich kodów odpowiedzi?)

### TESTY cURL API
Plik skryptowy testujący wszystkie endpointy API - szereg komend cURL

### JWT, EF InMemory, Swagger, Configuration

- [] JWT - implementacja w C# (https://koglaza.gitbook.io/backend/lab-04/jwt)
- [] repozytorium EF InMemory (baza danych w pamięci)
- [] Swagger (dokumentacja API)
- [] Configuration - nie mam pojęcia o co chodzi, o enva? Ale co w tym envie skonfigurować?
- [] unit tests - gpt'em z rozmysłem i jakoś to będzie, bez integracyjnych itd. za to odpowiada plik skryptowy patrz wyżej

### TODO - przekopiować listę dla UI w projekcie 3 i dostosować

## Projekt 2 GraphQL:

To pomijamy.

## Projekt 3 Razor Pages
- [] Panel administracyjny do modułu rest api (obsługa api: listowanie(zczytywanie), edycja, usuwanie, tworzenie)
- - [] lista piosenek
- - - [] piosenka item 
- - - - [] listener: dodaj/usuń z listy SWOICH piosenek
- - - - [] artist/admin: dodaj piosenkę do LUB usuń piosenkę z serwisu (usuwa też wszystkim userom), oraz edycja
- - - [] filtrowanie po artyście (dla artysty i usera)
- - - [] filtrowanie po userze (dla listowania piosenek tylko dla usera)
- - [] lista artystów
- - - [] artysta item - automatycznie bez listy dla artysty
- - - - [] edycja artysty (artysta + matchowane ID - musisz być sobą/admin)
- - - - - [] renegocjowanie stawki (losowe)
- - - - [] usuwanie artysty (artysta + matchowane ID - musisz być sobą/admin)
- - - [] dodawanie artysty (rejestracja/admin)
- - [] lista userów (admin):
- - - [] user item (z piosenkami patrz wyżej) - automatycznie bez listy dla usera
- - - - [] edycja usera
- - - - [] usuwanie usera
- - - [] dodawanie usera (rejestracja)
- [] strona logowania (3 role systemowe: admin, listener, artist)
- - [] Admin jako listener czy odrębny interfejs poza api w db?
- - [] JWT
