using Microsoft.AspNetCore.Mvc;
using Moq;
using Recepthantering_API.Controllers;
using Recepthantering_API.Models;
using Recepthantering_API.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Recepthantering.Test
{
    public class RecipesControllerTests
    {
        private readonly Mock<IRecipeService> _mockService;
        private readonly RecipesController _controller;

        public RecipesControllerTests()
        {
            _mockService = new Mock<IRecipeService>();
            _controller = new RecipesController(_mockService.Object);
        }

        [Fact]
        public async Task GetAll_Returns200OkWithRecipes()
        {
            // Arrange
            var recipes = new List<Recipe>
            {
                new Recipe { Id = 1, Name = "Pasta" },
                new Recipe { Id = 2, Name = "Pizza" }
            };
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(recipes);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedRecipes = Assert.IsAssignableFrom<IEnumerable<Recipe>>(okResult.Value);
            Assert.Equal(2, returnedRecipes.Count());
        }

        [Fact]
        public async Task GetById_Returns404_WhenRecipeNotFound()
        {
            // Arrange
            _mockService.Setup(s => s.GetByIdAsync(999)).ReturnsAsync((Recipe?)null);

            // Act
            var result = await _controller.GetById(999);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_Returns201Created_WithValidDto()
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
                Instructions = new List<string> { "Steg 1" }
            };

            var createdRecipe = new Recipe
            {
                Id = 1,
                Name = "Ny Rätt",
                CreatedAt = DateTime.UtcNow
            };

            _mockService.Setup(s => s.CreateAsync(dto)).ReturnsAsync(createdRecipe);

            // Act
            var result = await _controller.Create(dto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(201, createdResult.StatusCode);
            var returnedRecipe = Assert.IsType<Recipe>(createdResult.Value);
            Assert.Equal("Ny Rätt", returnedRecipe.Name);
        }
    }
}
