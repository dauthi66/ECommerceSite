using ECommerceSite.Models;
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

        [HttpPost]

        public IActionResult Create(Crate crate)
        {
            if (!ModelState.IsValid)
            {
                //Add to DB
                //Show successage on Page
                return View();
            }
            //if data bad send back
            return View(crate);
        }
    }
}
