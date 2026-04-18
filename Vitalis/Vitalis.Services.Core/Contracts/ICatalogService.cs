using System;
using System.Collections.Generic;
using System.Linq;
using Vitalis.Web.ViewModels;

namespace Vitalis.Services.Core.Contracts
{
    public interface ICatalogService
    {
        Task<IEnumerable<MealViewModel>> GetAllMealsAsync();

        Task<IEnumerable<IngredientViewModel>> GetAllIngredientsAsync();

        Task<IEnumerable<TagViewModel>> GetAllTagsAsync();

        Task<ViewTagViewModel> GetViewByTagAsync(int id);

        Task<IngredientViewModel> GetIngredientByIdAsync(int id);

        Task<MealViewModel> GetMealByIdAsync(int id);

        //----------------------------------------------//

        Task<CreateMealViewModel> GetCreateMealViewModel();

        Task<CreateMealViewModel> GetCreateMealViewModel(int id);

        Task<CreateIngredientViewModel> GetCreateIngredientViewModel();

        Task<CreateIngredientViewModel> GetCreateIngredientViewModel(int id);

        Task<TagViewModel> GetCreateTagViewModel();

        Task<TagViewModel> GetCreateTagViewModel(int id);

        //----------------------------------------------//

        Task AddMealAsync(CreateMealViewModel vm);

        Task AddIngredientAsync(CreateIngredientViewModel vm);

        Task AddTagAsync(TagViewModel vm);

        Task DeleteMealAsync(int id);

        Task DeleteIngredientAsync(int id);

        Task DeleteTagAsync(int id);
    }
}
