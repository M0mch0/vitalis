using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Vitalis.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected bool IsUserAuthenticated()
        {
            if (User == null)
            {
                return false;
            }
            
            if(User.Identity == null)
            {
                return false;
            }
            return User.Identity.IsAuthenticated;
        }

        [Authorize]
        protected string? GetUserId()
        {
            return User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
