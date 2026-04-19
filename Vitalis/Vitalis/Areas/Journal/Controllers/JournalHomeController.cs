using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Vitalis.Services.Core.Contracts;
using Vitalis.Web.ViewModels;

namespace Vitalis.Web.Areas.Journal.Controllers
{
    [Area("Journal")]
    public class JournalHomeController : BaseJournalController
    {
        private readonly IJournalService journalService;
        private readonly ICatalogService catalogService;

        public JournalHomeController(IJournalService journalService, ICatalogService catalogService)
        {
            this.journalService = journalService;
            this.catalogService = catalogService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (User.Identity.GetUserId() is null) return RedirectToAction("Login", "Account", new { area = "Identity" });
            var vm = await journalService.GetJournalEntryAsync(User.Identity.GetUserId());

            var meals = await catalogService.GetAllMealsAsync();
            var ingredients = await catalogService.GetAllIngredientsAsync();

            ViewBag.AvailableMeals = meals;
            ViewBag.AvailableIngredients = ingredients;

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> AddToJournal(JournalEntryViewModel vm)
        {

            await journalService.AddToJournalAsync(User.Identity.GetUserId(), vm);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromJournal(int id, bool MealOrIng)
        {
              await journalService.RemoveFromJournalAsync(User.Identity.GetUserId(), id, MealOrIng);

            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateQuantity(int id, double quantity)
        {
             await journalService.UpdateQuantityAsync(User.Identity.GetUserId(), id, quantity);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAmount(int id, int amount)
        {
            await journalService.UpdateAmountAsync(User.Identity.GetUserId(), id, amount);

            return RedirectToAction("Index");
        }
    }
}
