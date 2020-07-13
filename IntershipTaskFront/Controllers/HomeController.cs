using IntershipTaskFront.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IntershipTaskFront.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICenterService _centerService;

        public HomeController(ICenterService centerService)
        {
            _centerService = centerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _centerService.GetItems();
            return View(result);
        }
    }
}