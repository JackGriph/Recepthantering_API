using Recepthantering_API.Models;

namespace Recepthantering_API.Services
{
    public interface IRecipeService
    {
        Task<IEnumerable<Recipe>> GetAllAsync();
        Task<Recipe?> GetByIdAsync(int id);
        Task<IEnumerable<Recipe>> SearchAsync(string term);
        Task<IEnumerable<Recipe>> GetByDifficultyAsync(string difficulty);
        Task<Recipe> CreateAsync(CreateRecipeDTO dto);
        Task<Recipe?> UpdateAsync(int id, CreateRecipeDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
