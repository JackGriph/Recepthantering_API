using System.ComponentModel.DataAnnotations;

namespace Recepthantering_API.Models
{
    public class IngredientDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
