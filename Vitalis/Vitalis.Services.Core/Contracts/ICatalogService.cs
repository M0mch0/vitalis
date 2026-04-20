using System;
using System.Collections.Generic;
using System.Linq;
using Vitalis.Web.ViewModels;

namespace Vitalis.Services.Core.Contracts
{
    public interface ICatalogService
    {
        Task<(IEnumerable<MealViewModel> Meals, int TotalPages)> GetAllMealsAsync(string? searchQuery = null, int pageNumber = 1, int pageSize = 9);

        Task<(IEnumerable<IngredientViewModel> Ingredients, int TotalPages)> GetAllIngredientsAsync(string? searchQuery = null, int pageNumber = 1, int pageSize = 9);

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
