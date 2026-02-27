using Recepthantering_API.Models;
using Recepthantering_API.Repositories;

namespace Recepthantering_API.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _repository;

        public RecipeService(IRecipeRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Recipe>> GetAllAsync()
            => _repository.GetAllAsync();

        public Task<Recipe?> GetByIdAsync(int id)
            => _repository.GetByIdAsync(id);

        public Task<IEnumerable<Recipe>> SearchAsync(string term)
            => _repository.SearchAsync(term);

        public Task<IEnumerable<Recipe>> GetByDifficultyAsync(string difficulty)
            => _repository.GetByDifficultyAsync(difficulty);

        public async Task<Recipe> CreateAsync(CreateRecipeDTO dto)
        {
            var recipe = MapToRecipe(dto);
            recipe.CreatedAt = DateTime.UtcNow;
            return await _repository.CreateAsync(recipe);
        }

        public async Task<Recipe?> UpdateAsync(int id, CreateRecipeDTO dto)
        {
            var recipe = MapToRecipe(dto);
            return await _repository.UpdateAsync(id, recipe);
        }

        public Task<bool> DeleteAsync(int id)
            => _repository.DeleteAsync(id);

        private static Recipe MapToRecipe(CreateRecipeDTO dto)
        {
            return new Recipe
            {
                Name = dto.Name,
                Description = dto.Description,
                PrepTimeMinutes = dto.PrepTimeMinutes,
                CookTimeMinutes = dto.CookTimeMinutes,
                Servings = dto.Servings,
                Difficulty = dto.Difficulty,
                Instructions = dto.Instructions,
                Ingredients = dto.Ingredients.Select(i => new Ingredient
                {
                    Name = i.Name,
                    Quantity = i.Quantity,
                    Unit = i.Unit
                }).ToList()
            };
        }
    }
}
