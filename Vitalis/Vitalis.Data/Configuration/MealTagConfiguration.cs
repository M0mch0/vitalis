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
    public class MealTagConfiguration : IEntityTypeConfiguration<MealTag>
    {
        public void Configure(EntityTypeBuilder<MealTag> builder)
        {
            // Composite primary key
            builder.HasKey(mt => new { mt.MealId, mt.TagId });

            // Meal relationship
            builder.HasOne(mt => mt.Meal)
                .WithMany(m => m.Tags)
                .HasForeignKey(mt => mt.MealId);

            // Tag relationship
            builder.HasOne(mt => mt.Tag)
                .WithMany(t => t.Meals)
                .HasForeignKey(mt => mt.TagId);

            builder.HasData(MealTags);
        }

        private readonly List<MealTag> MealTags = new List<MealTag>
        {
            new MealTag { MealId = 1, TagId = 1 },
            new MealTag { MealId = 1, TagId = 2 },
            new MealTag { MealId = 1, TagId = 8 },

            new MealTag { MealId = 2, TagId = 1 },
            new MealTag { MealId = 2, TagId = 3 },
            new MealTag { MealId = 2, TagId = 8 },

            new MealTag { MealId = 3, TagId = 2 },
            new MealTag { MealId = 3, TagId = 5 },

            new MealTag { MealId = 4, TagId = 3 },
            new MealTag { MealId = 4, TagId = 6 }
        };
    }
}
