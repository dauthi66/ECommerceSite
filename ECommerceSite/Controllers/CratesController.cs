using Microsoft.AspNetCore.Mvc;

namespace ECommerceSite.Controllers
{
    public class CratesController : Controller
    {

        //displays view (page)  
        public IActionResult Create()
        {
            return View();
        }
    }
}
