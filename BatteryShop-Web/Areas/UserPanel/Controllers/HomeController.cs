using CoreLayer.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BatteryShop_Web.Areas.UserPanel.Models.User;

namespace BatteryShop_Web.Areas.UserPanel.Controllers
{
    public class HomeController : UserPanelControllerBase
    {
        [Authorize]
        
        
        public IActionResult Index(IndexUserViewModel model)
        {
            model.Name = User.GetNameUser();
            return View(model);
        }
    }
}
