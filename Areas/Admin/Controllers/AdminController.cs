using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NewsWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin,Manager")]

    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult dashboard()
        {
            return View();
        }

    }
}
