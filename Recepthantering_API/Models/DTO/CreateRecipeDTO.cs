using System.ComponentModel.DataAnnotations;

namespace Recepthantering_API.Models
{
    public class CreateRecipeDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Range(1, 480)]
        public int PrepTimeMinutes { get; set; }

        [Range(1, 480)]
        public int CookTimeMinutes { get; set; }
        
        [Range(1, 100)]
        public int Servings { get; set; }

        [Required]
        public string Difficulty { get; set; } = string.Empty; // Easy, Medium, Hard

        [Required]
        public List<CreateIngredientDTO> Ingredients { get; set; }

        [Required]
        [MinLength(1)]
        public List<string> Instructions { get; set; }
    }
}
