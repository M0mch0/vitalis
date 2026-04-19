using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vitalis.Data.Models;

namespace Vitalis.Data
{
    public class VitalisDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
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
        public virtual DbSet<Models.JournalEntry> JournalEntries { get; set; } = null!;
        public virtual DbSet<Models.JournalEntryMeal> JournalEntryMeals { get; set; } = null!;
        public virtual DbSet<Models.JournalEntryIngredient> JournalEntryIngredients { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.JournalEntry)
                .WithOne(j => j.User)
                .HasForeignKey<JournalEntry>(j => j.UserId)
                .IsRequired();

            modelBuilder.Entity<JournalEntry>()
                .HasIndex(j => j.UserId)
                .IsUnique();
           
            modelBuilder.Entity<JournalEntryMeal>()
                .HasKey(jm => new { jm.JournalEntryId, jm.MealId });

            modelBuilder.Entity<JournalEntryMeal>()
                .HasOne(jm => jm.JournalEntry)
                .WithMany(j => j.Meals)
                .HasForeignKey(jm => jm.JournalEntryId);

            modelBuilder.Entity<JournalEntryMeal>()
                .HasOne(jm => jm.Meal)
                .WithMany()
                .HasForeignKey(jm => jm.MealId);

            modelBuilder.Entity<JournalEntryIngredient>()
                .HasKey(ji => new { ji.JournalEntryId, ji.IngredientId });

            modelBuilder.Entity<JournalEntryIngredient>()
                .HasOne(ji => ji.JournalEntry)
                .WithMany(j => j.Ingredients)
                .HasForeignKey(ji => ji.JournalEntryId);

            modelBuilder.Entity<JournalEntryIngredient>()
                .HasOne(ji => ji.Ingredient)
                .WithMany()
                .HasForeignKey(ji => ji.IngredientId);


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VitalisDbContext).Assembly);
        }



    }
}
