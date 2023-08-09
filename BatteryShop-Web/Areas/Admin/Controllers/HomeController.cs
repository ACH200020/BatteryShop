using Microsoft.AspNetCore.Mvc;

namespace BatteryShop_Web.Areas.Admin.Controllers
{
    public class HomeController : AdminControllerBase
    {

        [Route("/admin")]
        public IActionResult Index()
        {
            return View();
        }

    }
}
