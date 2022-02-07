using Microsoft.AspNetCore.Mvc;

namespace ECommerceSite.Controllers
{
    public class CrateController : Controller
    {
        //displays page to user. To add razor view click on create()
        public IActionResult Create()
        {
            return View();
        }
    }
}
