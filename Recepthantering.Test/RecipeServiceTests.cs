using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Recepthantering_API.Models;
using Recepthantering_API.Repositories;
using Recepthantering_API.Services;

namespace Recepthantering.Test
{
    public class RecipeServiceTests
    {
        private readonly Mock<IRecipeRepository> _mockRepo;
        private readonly RecipeService _service;

        public RecipeServiceTests()
        {
            _mockRepo = new Mock<IRecipeRepository>();
            _service = new RecipeService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsListOfRecipes()
        {
            // Arrange
            var recipes = new List<Recipe>
            {
                new Recipe { Id = 1, Name = "Pasta" },
                new Recipe { Id = 2, Name = "Pizza" }
            };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(recipes);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ReturnsRecipe()
        {
            // Arrange
            var recipe = new Recipe { Id = 1, Name = "Test Recipe" };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(recipe);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Recipe", result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_NonExistingId_ReturnsNull()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Recipe?)null);

            // Act
            var result = await _service.GetByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_SetsCreatedAtAndReturnsRecipe()
        {
            // Arrange
            var dto = new CreateRecipeDTO
            {
                Name = "Ny Rätt",
                Description = "Beskrivning",
                PrepTimeMinutes = 10,
                CookTimeMinutes = 20,
                Servings = 4,
                Difficulty = "Easy",
                Ingredients = new List<CreateIngredientDTO>
                {
                    new CreateIngredientDTO { Name = "Mjöl", Quantity = 2, Unit = "dl" }
                },
                Instructions = new List<string> { "Steg 1", "Steg 2" }
            };

            _mockRepo.Setup(r => r.CreateAsync(It.IsAny<Recipe>()))
                     .ReturnsAsync((Recipe r) =>
                     {
                         r.Id = 1;
                         return r;
                     });

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            Assert.Equal("Ny Rätt", result.Name);
            Assert.Equal(1, result.Id);
            Assert.True(result.CreatedAt <= DateTime.UtcNow);
            Assert.True(result.CreatedAt > DateTime.UtcNow.AddSeconds(-5));
        }

        [Fact]
        public async Task SearchAsync_ReturnsFilteredRecipes()
        {
            // Arrange
            var recipes = new List<Recipe>
            {
                new Recipe { Id = 1, Name = "Pasta Carbonara" }
            };
            _mockRepo.Setup(r => r.SearchAsync("Pasta")).ReturnsAsync(recipes);

            // Act
            var result = await _service.SearchAsync("Pasta");

            // Assert
            Assert.Single(result);
            Assert.Equal("Pasta Carbonara", result.First().Name);
        }
    }
}
