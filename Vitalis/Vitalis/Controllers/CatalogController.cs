using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vitalis.Data;
using Vitalis.Models;

namespace Vitalis.Controllers
{
    public class CatalogController : Controller
    {
        private readonly VitalisDbContext dbContext;

        public CatalogController(VitalisDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Meals()
        {
            return View();
        }
        public IActionResult Ingredients()
        {
            return View();
        }
        public IActionResult Tags()
        {
            return View();
        }

    }
}
