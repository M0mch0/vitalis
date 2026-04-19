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
    public class NutrientProfileConfiguration : IEntityTypeConfiguration<NutrientProfile>
    {
        public NutrientProfileConfiguration() { }

        public void Configure(EntityTypeBuilder<NutrientProfile> builder)
        {
            builder.HasData(SeedNutrientProfiles);
        }

        private List<NutrientProfile> SeedNutrientProfiles => new List<NutrientProfile>
        {
            new NutrientProfile { Id = 1, Protein = 31, Carbohydrates = 0,  Fat = 4 },  
            new NutrientProfile { Id = 2, Protein = 3,  Carbohydrates = 23, Fat = 1 },  
            new NutrientProfile { Id = 3, Protein = 3,  Carbohydrates = 7,  Fat = 0 },  
            new NutrientProfile { Id = 4, Protein = 0,  Carbohydrates = 0,  Fat = 100 },
            new NutrientProfile { Id = 5, Protein = 13, Carbohydrates = 1,  Fat = 11 }, 
            new NutrientProfile { Id = 6, Protein = 20, Carbohydrates = 0,  Fat = 13 }, 
            new NutrientProfile { Id = 7, Protein = 2,  Carbohydrates = 20, Fat = 0 },  
            new NutrientProfile { Id = 8, Protein = 3,  Carbohydrates = 4,  Fat = 0 },  
            new NutrientProfile { Id = 9, Protein = 10, Carbohydrates = 4,  Fat = 5 },  
            new NutrientProfile { Id = 10, Protein = 4, Carbohydrates = 21, Fat = 2 },  
            new NutrientProfile { Id = 11, Protein = 2, Carbohydrates = 9,  Fat = 15 }, 
            new NutrientProfile { Id = 12, Protein = 21, Carbohydrates = 22, Fat = 49 },
            new NutrientProfile { Id = 13, Protein = 1, Carbohydrates = 14, Fat = 0 },  
            new NutrientProfile { Id = 14, Protein = 17, Carbohydrates = 66, Fat = 7 }  

        };
    }
}