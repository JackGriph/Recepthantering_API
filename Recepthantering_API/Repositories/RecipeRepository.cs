using Microsoft.EntityFrameworkCore;
using Recepthantering_API.Data;
using Recepthantering_API.Models;

namespace Recepthantering_API.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly AppDbContext _context;

        public RecipeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Recipe>> GetAllAsync()
            => await _context.Recipes.Include(r => r.Ingredients).ToListAsync();

        public async Task<Recipe?> GetByIdAsync(int id)
            => await _context.Recipes.Include(r => r.Ingredients)
                                     .FirstOrDefaultAsync(r => r.Id == id);

        public async Task<IEnumerable<Recipe>> SearchAsync(string term)
            => await _context.Recipes.Include(r => r.Ingredients)
                .Where(r => r.Name.Contains(term) || r.Description.Contains(term))
                .ToListAsync();

        public async Task<IEnumerable<Recipe>> GetByDifficultyAsync(string difficulty)
            => await _context.Recipes.Include(r => r.Ingredients)
                .Where(r => r.Difficulty == difficulty)
                .ToListAsync();

        public async Task<Recipe> CreateAsync(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }

        public async Task<Recipe?> UpdateAsync(int id, Recipe updated)
        {
            var existing = await _context.Recipes.Include(r => r.Ingredients)
                                                 .FirstOrDefaultAsync(r => r.Id == id);
            if (existing is null) return null;

            existing.Name = updated.Name;
            existing.Description = updated.Description;
            existing.PrepTimeMinutes = updated.PrepTimeMinutes;
            existing.CookTimeMinutes = updated.CookTimeMinutes;
            existing.Servings = updated.Servings;
            existing.Difficulty = updated.Difficulty;
            existing.Ingredients = updated.Ingredients;
            existing.Instructions = updated.Instructions;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe is null) return false;

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
