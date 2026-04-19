using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitalis.Data.Models;
using Vitalis.Data.Repository.Contracts;

namespace Vitalis.Data.Repository
{
    public class IngRepository : BaseRepository, IIngRepository
    {
        public IngRepository(VitalisDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Ingredient>> GetAllIngredientsAsync()
        {
            return await Context.Ingredients
                .AsNoTracking()
                .Include(i => i.NutrientProfile)
                .Include(i => i.Tags)
                .ThenInclude(t => t.Tag)
                .ToArrayAsync();
        }

        public async Task<Ingredient> GetByIdAsync(int id)
        {
            return await Context.Ingredients
                .AsNoTracking()
                .Include(i => i.NutrientProfile)
                .Include(i => i.Tags)
                .ThenInclude(t => t.Tag)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task AddAsync(Ingredient ing)
        {
            await Context.Ingredients.AddAsync(ing);
            await Context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var ing = await GetByIdAsync(id);
            if (ing != null)
            {
                Context.Ingredients.Remove(ing);
                await Context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Ingredient ing)
        {
            var existingIng = await GetByIdAsync(ing.Id);
            if (existingIng != null)
            {
                existingIng.Name = ing.Name;
                existingIng.NutrientProfile = ing.NutrientProfile;
                existingIng.Tags = ing.Tags;
                existingIng.Notes = ing.Notes;
                existingIng.ImageUrl = ing.ImageUrl;
                Context.Ingredients.Update(existingIng);
                await Context.SaveChangesAsync();
            }
        }
    }
}
