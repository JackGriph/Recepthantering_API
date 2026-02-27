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

        public async Task<Recipe> CreateAsync(Recipe recipe)
        {
            // Affärslogik: sätt CreatedAt här om repository inte gör det
            recipe.CreatedAt = DateTime.UtcNow;
            return await _repository.CreateAsync(recipe);
        }

        public Task<Recipe?> UpdateAsync(int id, Recipe recipe)
            => _repository.UpdateAsync(id, recipe);

        public Task<bool> DeleteAsync(int id)
            => _repository.DeleteAsync(id);
    }
}
