using Recepthantering_API.Models;

namespace Recepthantering_API.Repositories
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<Recipe>> GetAllAsync();
        Task<Recipe?> GetByIdAsync(int id);
        Task<IEnumerable<Recipe>> SearchAsync(string term);
        Task<IEnumerable<Recipe>> GetByDifficultyAsync(string difficulty);
        Task<Recipe> CreateAsync(Recipe recipe);
        Task<Recipe?> UpdateAsync(int id, Recipe recipe);
        Task<bool> DeleteAsync(int id);
    }
}
