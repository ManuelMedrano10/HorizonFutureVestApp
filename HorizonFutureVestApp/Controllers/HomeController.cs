using Microsoft.AspNetCore.Mvc;

namespace HorizonFutureVestApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
