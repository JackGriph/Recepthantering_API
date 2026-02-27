using Microsoft.EntityFrameworkCore;
using Recepthantering_API.Models;
using System.Text.Json;

namespace Recepthantering_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // List<string> kan inte lagras direkt i SQL — serialiseras till JSON
            modelBuilder.Entity<Recipe>()
                .Property(r => r.Instructions)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
                    v => JsonSerializer.Deserialize<List<string>>(v, JsonSerializerOptions.Default)!
                );
        }
    }
}
