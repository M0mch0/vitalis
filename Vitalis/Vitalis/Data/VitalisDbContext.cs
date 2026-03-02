using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Vitalis.Data
{
    public class VitalisDbContext : IdentityDbContext
    {
        public VitalisDbContext(DbContextOptions<VitalisDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Models.Tag> Tags { get; set; } = null!;
        public virtual DbSet<Models.Meal> Meals { get; set; } = null!;
        public virtual DbSet<Models.Ingredient> Ingredients { get; set; } = null!;
        public virtual DbSet<Models.NutrientProfile> NutrientProfiles { get; set; } = null!;
        public virtual DbSet<Models.MealIngredient> MealIngredients { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VitalisDbContext).Assembly);
        }



    }
}
