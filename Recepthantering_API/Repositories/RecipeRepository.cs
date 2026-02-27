using Recepthantering_API.Models;

namespace Recepthantering_API.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly List<Recipe> _recipes = [];
        private int _nextId = 1;

        public Task<IEnumerable<Recipe>> GetAllAsync()
            => Task.FromResult<IEnumerable<Recipe>>(_recipes);

        public Task<Recipe?> GetByIdAsync(int id)
            => Task.FromResult(_recipes.FirstOrDefault(r => r.Id == id));

        public Task<IEnumerable<Recipe>> SearchAsync(string term)
        {
            var result = _recipes.Where(r =>
                r.Name.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                r.Description.Contains(term, StringComparison.OrdinalIgnoreCase));

            return Task.FromResult(result);
        }

        public Task<IEnumerable<Recipe>> GetByDifficultyAsync(string difficulty)
        {
            var result = _recipes.Where(r =>
                r.Difficulty.Equals(difficulty, StringComparison.OrdinalIgnoreCase));

            return Task.FromResult(result);
        }

        public Task<Recipe> CreateAsync(Recipe recipe)
        {
            recipe.Id = _nextId++;
            recipe.CreatedAt = DateTime.UtcNow;
            _recipes.Add(recipe);
            return Task.FromResult(recipe);
        }

        public Task<Recipe?> UpdateAsync(int id, Recipe updated)
        {
            var existing = _recipes.FirstOrDefault(r => r.Id == id);
            if (existing is null)
                return Task.FromResult<Recipe?>(null);

            existing.Name = updated.Name;
            existing.Description = updated.Description;
            existing.PrepTimeMinutes = updated.PrepTimeMinutes;
            existing.CookTimeMinutes = updated.CookTimeMinutes;
            existing.Servings = updated.Servings;
            existing.Difficulty = updated.Difficulty;
            existing.Ingredients = updated.Ingredients;
            existing.Instructions = updated.Instructions;

            return Task.FromResult<Recipe?>(existing);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var recipe = _recipes.FirstOrDefault(r => r.Id == id);
            if (recipe is null)
                return Task.FromResult(false);

            _recipes.Remove(recipe);
            return Task.FromResult(true);
        }
    }
}
