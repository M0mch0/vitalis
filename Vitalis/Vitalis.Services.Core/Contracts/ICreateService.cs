using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitalis.Web.ViewModels;

namespace Vitalis.Services.Core.Contracts
{
    public interface ICreateService
    {

        Task<CreateMealViewModel> GetCreateMealViewModel();

        Task<CreateMealViewModel> GetCreateMealViewModel(int id);

        Task<CreateIngredientViewModel> GetCreateIngredientViewModel();

        Task<CreateIngredientViewModel> GetCreateIngredientViewModel(int id);

        Task<TagViewModel> GetCreateTagViewModel();

        Task<TagViewModel> GetCreateTagViewModel(int id);

        //------------------------------------------------------

        Task AddMealAsync(CreateMealViewModel vm);

        Task AddIngredientAsync(CreateIngredientViewModel vm);

        Task AddTagAsync(TagViewModel vm);
    }
}
