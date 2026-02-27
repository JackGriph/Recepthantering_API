using Microsoft.AspNetCore.Mvc;
using Recepthantering_API.Models;
using Recepthantering_API.Services;

namespace Recepthantering_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _service;

        public RecipesController(IRecipeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetAll()
        {
            var recipes = await _service.GetAllAsync();
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetById(int id)
        {
            var recipe = await _service.GetByIdAsync(id);
            if (recipe == null)
                return NotFound();

            return Ok(recipe);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Recipe>>> Search([FromQuery] string q)
        {
            var results = await _service.SearchAsync(q);
            return Ok(results);
        }

        [HttpGet("difficulty/{level}")]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetByDifficulty(string level)
        {
            var recipes = await _service.GetByDifficultyAsync(level);
            return Ok(recipes);
        }

        [HttpPost]
        public async Task<ActionResult<Recipe>> Create([FromBody] CreateRecipeDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Recipe>> Update(int id, [FromBody] CreateRecipeDTO dto)  
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}