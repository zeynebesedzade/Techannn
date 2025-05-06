using Microsoft.AspNetCore.Mvc;

namespace Techan.Areas.Admin.Controllers
{
    public class DasboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
