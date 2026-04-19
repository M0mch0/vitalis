using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitalis.Data.Models;

namespace Vitalis.Data.Configuration
{
    public class IngredientTagConfiguration : IEntityTypeConfiguration<IngredientTag>
    {
        public void Configure(EntityTypeBuilder<IngredientTag> builder)
        {
            // Composite primary key
            builder.HasKey(mt => new { mt.IngredientId, mt.TagId });

            // Ingredient relationship
            builder.HasOne(mt => mt.Ingredient)
                .WithMany(i => i.Tags)
                .HasForeignKey(mt => mt.IngredientId);

            // Tag relationship
            builder.HasOne(mt => mt.Tag)
                .WithMany(t => t.Ingredients)
                .HasForeignKey(mt => mt.TagId);

            builder.HasData(ingredientTags);
        }

        private readonly List<IngredientTag> ingredientTags = new List<IngredientTag>
        {
            // Chicken Breast
                new IngredientTag { IngredientId = 1, TagId = 1 },
                new IngredientTag { IngredientId = 1, TagId = 8 },
                new IngredientTag { IngredientId = 1, TagId = 9 },

                // Brown Rice
                new IngredientTag { IngredientId = 2, TagId = 2 },
                new IngredientTag { IngredientId = 2, TagId = 7 },
                new IngredientTag { IngredientId = 2, TagId = 8 },

                // Broccoli
                new IngredientTag { IngredientId = 3, TagId = 4 },
                new IngredientTag { IngredientId = 3, TagId = 8 },

                // Olive Oil
                new IngredientTag { IngredientId = 4, TagId = 3 },
                new IngredientTag { IngredientId = 4, TagId = 8 },

                // Eggs
                new IngredientTag { IngredientId = 5, TagId = 1 },
                new IngredientTag { IngredientId = 5, TagId = 3 },
                // Salmon
                new IngredientTag { IngredientId = 6, TagId = 1 },
                new IngredientTag { IngredientId = 6, TagId = 3 },
                new IngredientTag { IngredientId = 6, TagId = 8 },
                // Sweet Potato
                new IngredientTag { IngredientId = 7, TagId = 2 },
                new IngredientTag { IngredientId = 7, TagId = 4 },

                // Spinach
                new IngredientTag { IngredientId = 8, TagId = 4 },
                new IngredientTag { IngredientId = 8, TagId = 8 },
                // Greek Yogurt
                new IngredientTag { IngredientId = 9, TagId = 1 },
                new IngredientTag { IngredientId = 9, TagId = 6 },
                               
                // Quinoa      
                new IngredientTag { IngredientId = 10, TagId = 2 },
                new IngredientTag { IngredientId = 10, TagId = 1 },
                new IngredientTag { IngredientId = 10, TagId = 7 },
                                
                // Avocado      
                new IngredientTag { IngredientId = 11, TagId = 3 },
                new IngredientTag { IngredientId = 11, TagId = 8 },
                                
                // Almonds      
                new IngredientTag { IngredientId = 12, TagId = 3 },
                new IngredientTag { IngredientId = 12, TagId = 1 },
                               
                // Blueberries 
                new IngredientTag { IngredientId = 13, TagId = 5 },
                new IngredientTag { IngredientId = 13, TagId = 8 },
                                
                // Oats         
                new IngredientTag { IngredientId = 14, TagId = 2 },
                new IngredientTag { IngredientId = 14, TagId = 7 }
        };
                

    }
}
