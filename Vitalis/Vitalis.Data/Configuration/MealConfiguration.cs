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
    public class MealConfiguration : IEntityTypeConfiguration<Meal>
    {
        public MealConfiguration() { }

        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            builder.HasData(SeedMeals);
        }

        private readonly List<Meal> SeedMeals = new List<Meal>
        {
            new Meal
            {
                Id = 1,
                Name = "Grilled Chicken with Rice",
                Notes = "High protein balanced meal",
                ImageUrl = "https://example.com/images/meals/chicken-rice.jpg"
            },
            new Meal
            {
                Id = 2,
                Name = "Salmon with Sweet Potato",
                Notes = "Rich in omega-3 and complex carbs",
                ImageUrl = "https://example.com/images/meals/salmon-sweetpotato.jpg"
            },
            new Meal
            {
                Id = 3,
                Name = "Oatmeal with Blueberries",
                Notes = "Great for breakfast",
                ImageUrl = "https://example.com/images/meals/oatmeal-blueberries.jpg"
            },
            new Meal
            {
                Id = 4,
                Name = "Avocado Yogurt Bowl",
                Notes = "Healthy fats and protein combo",
                ImageUrl = "https://example.com/images/meals/avocado-yogurt.jpg"
            }
        };
    }

}