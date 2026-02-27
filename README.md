# Recepthantering API

Ett REST API byggt med ASP.NET Core 10 for hantering av recept.

## Arkitektur

Projektet foljer ett lagerbaserat monster med tydlig separation:

```
Controller  ->  Service  ->  Repository  ->  Datakalla
  (HTTP)      (Logik)      (Dataatkomst)
```

- **Controllers** - Tunna, hanterar bara HTTP request/response
- **Services** - Affarlogik och mappning (DTO -> Entity)
- **Repositories** - Dataatkomst via interface (for narlaget in-memory)
- **Models** - Entiteter och DTOs med validering

## API-endpoints

| Metod | Route | Beskrivning |
|-------|-------|-------------|
| GET | `/api/recipes` | Hamta alla recept |
| GET | `/api/recipes/{id}` | Hamta specifikt recept (detaljvy) |
| GET | `/api/recipes/search?q={term}` | Sok recept pa namn/beskrivning |
| GET | `/api/recipes/difficulty/{level}` | Filtrera pa svarighetsgrad |
| POST | `/api/recipes` | Skapa nytt recept |
| PUT | `/api/recipes/{id}` | Uppdatera recept |
| DELETE | `/api/recipes/{id}` | Ta bort recept |

## Kom igang

### Forutsattningar

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### Kör projektet

```bash
cd Recepthantering_API
dotnet run
```

API:et startar pa `http://localhost:5233`

### Swagger

Oppna `http://localhost:5233/swagger` i webblasaren for att testa endpoints interaktivt.

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
    { "name": "Mjol", "quantity": 3, "unit": "dl" },
    { "name": "Mjolk", "quantity": 6, "unit": "dl" },
    { "name": "Agg", "quantity": 3, "unit": "st" }
  ],
  "instructions": [
    "Blanda mjol och halva mjolken till en slat smet",
    "Tillsatt resten av mjolken och aggen",
    "Stek i smor pa medelhog varme"
  ]
}
```

## Teknikstack

- ASP.NET Core 10
- Entity Framework Core 10 (forberett)
- xUnit (tester)
- Swagger / OpenAPI
