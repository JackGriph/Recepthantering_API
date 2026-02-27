# Recepthantering API

Ett REST API byggt med ASP.NET Core 10 för hantering av recept.

## Arkitektur

Projektet följer ett lagerbaserat mönster med tydlig separation:

```
Controller  ->  Service  ->  Repository  ->  Datakälla
  (HTTP)      (Logik)      (Dataåtkomst)
```

- **Controllers** - Tunna, hanterar bara HTTP request/response
- **Services** - Affärslogik och mappning (DTO -> Entity)
- **Repositories** - Dataåtkomst via interface (för närläget in-memory)
- **Models** - Entiteter och DTOs med validering

## API-endpoints

| Metod | Route | Beskrivning |
|-------|-------|-------------|
| GET | `/api/recipes` | Hämta alla recept |
| GET | `/api/recipes/{id}` | Hämta specifikt recept (detaljvy) |
| GET | `/api/recipes/search?q={term}` | Sök recept på namn/beskrivning |
| GET | `/api/recipes/difficulty/{level}` | Filtrera på svårighetsgrad |
| POST | `/api/recipes` | Skapa nytt recept |
| PUT | `/api/recipes/{id}` | Uppdatera recept |
| DELETE | `/api/recipes/{id}` | Ta bort recept |

## Kom igång

### Förutsättningar

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### Kör projektet

```bash
cd Recepthantering_API
dotnet run
```

API:et startar på `http://localhost:5233`

### Swagger

Öppna `http://localhost:5233/swagger` i webbläsaren för att testa endpoints interaktivt.

## Projektstruktur

```
Recepthantering_API/
├── Controllers/
│   └── RecipesController.cs
├── Models/
│   ├── Recipe.cs
│   ├── Ingredient.cs
│   └── DTO/
│       ├── RecipeDTO.cs
│       ├── RecipeDetailDTO.cs
│       ├── IngredientDTO.cs
│       ├── CreateRecipeDTO.cs
│       └── CreateIngredientDTO.cs
├── Services/
│   ├── IRecipeService.cs
│   └── RecipeService.cs
├── Repositories/
│   ├── IRecipeRepository.cs
│   └── RecipeRepository.cs
└── Program.cs

Recepthantering.Test/
└── UnitTest1.cs
```

## Exempel - Skapa recept

```json
POST /api/recipes

{
  "name": "Pannkakor",
  "description": "Klassiska svenska pannkakor",
  "prepTimeMinutes": 10,
  "cookTimeMinutes": 20,
  "servings": 4,
  "difficulty": "Easy",
  "ingredients": [
    { "name": "Mjöl", "quantity": 3, "unit": "dl" },
    { "name": "Mjölk", "quantity": 6, "unit": "dl" },
    { "name": "Ägg", "quantity": 3, "unit": "st" }
  ],
  "instructions": [
    "Blanda mjöl och halva mjölken till en slät smet",
    "Tillsätt resten av mjölken och äggen",
    "Stek i smör på medelhög värme"
  ]
}
```

## Teknikstack

- ASP.NET Core 10
- Entity Framework Core 10 (förberett)
- xUnit (tester)
- Swagger / OpenAPI
