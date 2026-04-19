using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitalis.Data.Models;

namespace Vitalis.Data.Configuration
{
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public IngredientConfiguration() { }

        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.HasData(SeedIngredients);
        }

        private List<Ingredient> SeedIngredients => new List<Ingredient>
        {
            new Ingredient
            {
                Id = 1,
                Name = "Chicken Breast",
                Notes = "Lean protein source",
                ImageUrl = "https://example.com/images/chicken-breast.jpg",
                NutrientProfileId = 1
            },
            new Ingredient
            {
                Id = 2,
                Name = "Brown Rice",
                Notes = "Whole grain carbohydrate",
                ImageUrl = "https://example.com/images/brown-rice.jpg",
                NutrientProfileId = 2
            },
            new Ingredient
            {
                Id = 3,
                Name = "Broccoli",
                Notes = "Rich in fiber and vitamins",
                ImageUrl = "https://example.com/images/broccoli.jpg",
                NutrientProfileId = 3
            },
            new Ingredient
            {
                Id = 4,
                Name = "Olive Oil",
                Notes = "Healthy fats",
                ImageUrl = "https://example.com/images/olive-oil.jpg",
                NutrientProfileId = 4
            },
            new Ingredient
            {
                Id = 5,
                Name = "Eggs",
                Notes = "High-quality protein and fats",
                ImageUrl = "https://example.com/images/eggs.jpg",
                NutrientProfileId = 5
            },
            new Ingredient
            {
                Id = 6,
                Name = "Salmon",
                Notes = "Rich in omega-3 fatty acids",
                ImageUrl = "https://example.com/images/salmon.jpg",
                NutrientProfileId = 6
            },
            new Ingredient
            {
                Id = 7,
                Name = "Sweet Potato",
                Notes = "Complex carbs and fiber",
                ImageUrl = "https://example.com/images/sweet-potato.jpg",
                NutrientProfileId = 7
            },
            new Ingredient
            {
                Id = 8,
                Name = "Spinach",
                Notes = "Iron-rich leafy green",
                ImageUrl = "https://example.com/images/spinach.jpg",
                NutrientProfileId = 8
            },
            new Ingredient
            {
                Id = 9,
                Name = "Greek Yogurt",
                Notes = "High in protein and probiotics",
                ImageUrl = "https://example.com/images/greek-yogurt.jpg",
                NutrientProfileId = 9
            },
            new Ingredient
            {
                Id = 10,
                Name = "Quinoa",
                Notes = "Complete protein grain alternative",
                ImageUrl = "https://example.com/images/quinoa.jpg",
                NutrientProfileId = 10
            },
            new Ingredient
            {
                Id = 11,
                Name = "Avocado",
                Notes = "Rich in healthy monounsaturated fats",
                ImageUrl = "https://example.com/images/avocado.jpg",
                NutrientProfileId = 11
            },
            new Ingredient
            {
                Id = 12,
                Name = "Almonds",
                Notes = "Nutrient-dense snack with healthy fats",
                ImageUrl = "https://example.com/images/almonds.jpg",
                NutrientProfileId = 12
            },
            new Ingredient
            {
                Id = 13,
                Name = "Blueberries",
                Notes = "Antioxidant-rich fruit",
                ImageUrl = "https://example.com/images/blueberries.jpg",
                NutrientProfileId = 13
            },
            new Ingredient
            {
                Id = 14,
                Name = "Oats",
                Notes = "High in soluble fiber",
                ImageUrl = "https://example.com/images/oats.jpg",
                NutrientProfileId = 14
            }

        };
    }
}
