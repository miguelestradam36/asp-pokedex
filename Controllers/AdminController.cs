using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Pokemon.Controllers
{
    public class AdminController : Controller
    {
        [Route("admin/")]
        [RequireHttps]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [Route("admin/login")]
        [RequireHttps]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
