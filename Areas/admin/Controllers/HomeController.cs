using Microsoft.AspNetCore.Mvc;

namespace sbs.Areas.admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
