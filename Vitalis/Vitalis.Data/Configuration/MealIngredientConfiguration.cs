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
    public class MealIngredientConfiguration : IEntityTypeConfiguration<MealIngredient>
    {
        public MealIngredientConfiguration() { }

        public void Configure(EntityTypeBuilder<MealIngredient> builder)
        {
            builder.HasData(SeedMealIngredients);
        }

        private readonly List<MealIngredient> SeedMealIngredients = new List<MealIngredient>
        {
            // Meal 1: Chicken + Rice + Broccoli 
            new MealIngredient { Id = 1, MealId = 1, IngredientId = 1, Quantity = 200 },
            new MealIngredient { Id = 2, MealId = 1, IngredientId = 2, Quantity = 150 },
            new MealIngredient { Id = 3, MealId = 1, IngredientId = 3, Quantity = 100 },

            // Meal 2: Salmon + Sweet Potato + Spinach
            new MealIngredient { Id = 4, MealId = 2, IngredientId = 6, Quantity = 180 },
            new MealIngredient { Id = 5, MealId = 2, IngredientId = 7, Quantity = 200 },
            new MealIngredient { Id = 6, MealId = 2, IngredientId = 8, Quantity = 80 }, 

            // Meal 3: Oats + Blueberries + Yogurt
            new MealIngredient { Id = 7, MealId = 3, IngredientId = 14, Quantity = 100 }, 
            new MealIngredient { Id = 8, MealId = 3, IngredientId = 13, Quantity = 50 },  
            new MealIngredient { Id = 9, MealId = 3, IngredientId = 9, Quantity = 150 },  

            // Meal 4: Avocado + Yogurt + Almonds
            new MealIngredient { Id = 10, MealId = 4, IngredientId = 11, Quantity = 100 },
            new MealIngredient { Id = 11, MealId = 4, IngredientId = 9, Quantity = 150 }, 
            new MealIngredient { Id = 12, MealId = 4, IngredientId = 12, Quantity = 30 }

        };
    }

}