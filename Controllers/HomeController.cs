using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsWebsite.Data;
using NewsWebsite.Models;

namespace NewsWebsite.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        NewsDbContext _context;
        public HomeController( NewsDbContext context)
        {
            //_logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var result = _context.Categories.ToList();
            return View(result);
        }
        [Authorize]
        public IActionResult Contact()
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult SaveContact(ContactUs model)
        {
            _context.contacts.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Contact");
        }
        [Authorize]

        public IActionResult News(int id)
        {
            ViewBag.cat = _context.Categories.Find(id)!.Name;
         var result=   _context.News.Where(x => x.CategoryId == id).OrderByDescending(x=>x.Date).ToList();
            return View(result);
        }
        public IActionResult TeamMember()
        {
          var result=_context.teamMembers.ToList();
            return View(result);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
