using Microsoft.AspNetCore.Mvc;

namespace Vitalis.Web.Areas.Journal.Controllers
{
    [Area("Journal")]
    public class JournalHomeController : BaseJournalController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
