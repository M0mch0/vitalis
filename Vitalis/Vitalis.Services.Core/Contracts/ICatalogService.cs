using System;
using System.Collections.Generic;
using System.Linq;
using Vitalis.Web.ViewModels;

namespace Vitalis.Services.Core.Contracts
{
    public interface ICatalogService
    {
        Task<IEnumerable<MealViewModel>> GetAllMealsAsync(string? searchQuery = null);

        Task<IEnumerable<IngredientViewModel>> GetAllIngredientsAsync(string? searchQuery = null);

        Task<IEnumerable<TagViewModel>> GetAllTagsAsync();

        Task<ViewTagViewModel> GetViewByTagAsync(int id);

        Task<IngredientViewModel> GetIngredientByIdAsync(int id);

        Task<MealViewModel> GetMealByIdAsync(int id);

        //----------------------------------------------

        Task DeleteMealAsync(int id);

        Task DeleteIngredientAsync(int id);

        Task DeleteTagAsync(int id);
    }
}
