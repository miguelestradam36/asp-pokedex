using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Pokemon.Controllers
{
    public class AdminController : Controller
    {
        // GET: AdminController
        public IActionResult Index()
        {
            return View();
        }
    }
}
