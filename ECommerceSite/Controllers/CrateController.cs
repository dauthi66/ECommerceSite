using ECommerceSite.Data;
using ECommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceSite.Controllers
{
    public class CrateController : Controller
    {
        //underscore to identify it is a field. readonly does not allow reassignment
        //already configured in program.cs
        private readonly CrateContext _context;

        public CrateController(CrateContext context)
        {
            _context = context;
        }

        //displays page to user. To add razor view click on create()
        public IActionResult Create()
        {
            return View();
        }
        //obtains data from client and adds to datbase
        [HttpPost]
        public IActionResult Create(Crate crate)
        {
            if (ModelState.IsValid)
            {
                _context.Crates.Add(crate); //prepares insert
                _context.SaveChanges(); //executes pending insert
                //Show success message on page
                ViewData["Message"] = $"{crate.Title} was added successfully!";

                return View();
            }
            //if not valid return object back
            return View(crate);
        }
    }
}
