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
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public TagConfiguration() { }

        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasData(SeedTags);
        }

        private List<Tag> SeedTags => new List<Tag>
        {
            new Tag { Id = 1, Name = "Protein", ImageUrl = "https://example.com/images/tags/protein.png" },
            new Tag { Id = 2, Name = "Carbs", ImageUrl = "https://example.com/images/tags/carbs.png" },
            new Tag { Id = 3, Name = "Fats", ImageUrl = "https://example.com/images/tags/fats.png" },
            new Tag { Id = 4, Name = "Vegetable", ImageUrl = "https://example.com/images/tags/vegetable.png" },
            new Tag { Id = 5, Name = "Fruit", ImageUrl = "https://example.com/images/tags/fruit.png" },
            new Tag { Id = 6, Name = "Dairy", ImageUrl = "https://example.com/images/tags/dairy.png" },
            new Tag { Id = 7, Name = "Grain", ImageUrl = "https://example.com/images/tags/grain.png" },
            new Tag { Id = 8, Name = "Healthy", ImageUrl = "https://example.com/images/tags/healthy.png" },
            new Tag { Id = 9, Name = "Meat", ImageUrl = "https://example.com/images/tags/meat.png" }
        };
    }
}