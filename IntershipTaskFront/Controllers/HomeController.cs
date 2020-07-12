using Microsoft.AspNetCore.Mvc;

namespace IntershipTaskFront.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}