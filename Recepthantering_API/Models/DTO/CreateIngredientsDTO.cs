using System.ComponentModel.DataAnnotations;

namespace Recepthantering_API.Models
{
    public class CreateIngredientDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public string Unit { get; set; } = string.Empty;
    }
}
