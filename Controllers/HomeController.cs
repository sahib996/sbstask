using Microsoft.AspNetCore.Mvc;

namespace sbs.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
