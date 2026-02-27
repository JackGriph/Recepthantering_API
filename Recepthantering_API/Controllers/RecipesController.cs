using Microsoft.AspNetCore.Mvc;
using Recepthantering_API.Models;
using Recepthantering_API.Services;

namespace Recepthantering_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeDTO>>> GetAll()
        {
            var recipes = await _recipeService.GetAllAsync();
            var dtos = recipes.Select(r => new RecipeDTO
            {
                Id = r.Id,
                Name = r.Name
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDetailDTO>> GetById(int id)
        {
            var recipe = await _recipeService.GetByIdAsync(id);
            if (recipe is null)
                return NotFound();

            var dto = new RecipeDetailDTO
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                PrepTimeMinutes = recipe.PrepTimeMinutes,
                CookTimeMinutes = recipe.CookTimeMinutes,
                Servings = recipe.Servings,
                Ingredients = recipe.Ingredients.Select(i => new IngredientDTO
                {
                    Id = i.Id,
                    Name = i.Name
                }).ToList(),
                Instructions = recipe.Instructions
            };
            return Ok(dto);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<RecipeDTO>>> Search([FromQuery] string q)
        {
            var recipes = await _recipeService.SearchAsync(q);
            var dtos = recipes.Select(r => new RecipeDTO
            {
                Id = r.Id,
                Name = r.Name
            });
            return Ok(dtos);
        }

        [HttpGet("difficulty/{level}")]
        public async Task<ActionResult<IEnumerable<RecipeDTO>>> GetByDifficulty(string level)
        {
            var recipes = await _recipeService.GetByDifficultyAsync(level);
            var dtos = recipes.Select(r => new RecipeDTO
            {
                Id = r.Id,
                Name = r.Name
            });
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult<RecipeDTO>> Create(CreateRecipeDTO dto)
        {
            var created = await _recipeService.CreateAsync(dto);

            var responseDto = new RecipeDTO
            {
                Id = created.Id,
                Name = created.Name
            };
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, responseDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RecipeDTO>> Update(int id, CreateRecipeDTO dto)
        {
            var updated = await _recipeService.UpdateAsync(id, dto);
            if (updated is null)
                return NotFound();

            var responseDto = new RecipeDTO
            {
                Id = updated.Id,
                Name = updated.Name
            };
            return Ok(responseDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _recipeService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}