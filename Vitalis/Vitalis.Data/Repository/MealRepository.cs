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
    public class MealRepository : BaseRepository, IMealRepository
    {
        public MealRepository(VitalisDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Meal>> GetAllMealsAsync()
        {
            return await Context
                .Meals
                .AsNoTracking()
                .Include(m => m.Ingredients)
                .ThenInclude(mi => mi.Ingredient)
                .ThenInclude(i => i.NutrientProfile)
                .Include(m => m.Tags)
                .ToArrayAsync();
        }
        public async Task<Meal> GetByIdAsync(int id)
        {
            return await Context.Meals.FirstOrDefaultAsync(m => m.Id == id);

        }

        public async Task AddAsync(Meal meal)
        {
            await Context.Meals.AddAsync(meal);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var meal = await GetByIdAsync(id);
            if (meal != null)
            {
                Context.Meals.Remove(meal);
                await Context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Meal meal)
        {
            var existingMeal = await GetByIdAsync(meal.Id);
            if (existingMeal != null)
            {
                existingMeal.Name = meal.Name;
                existingMeal.Ingredients = meal.Ingredients;
                existingMeal.Notes = meal.Notes;
                existingMeal.ImageUrl = meal.ImageUrl;
                existingMeal.Tags = meal.Tags;
                existingMeal.Ingredients = meal.Ingredients;

                Context.Meals.Update(existingMeal);
                await Context.SaveChangesAsync();
            }
        }
    }

}
