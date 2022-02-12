using ECommerceSite.Data;
using ECommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> Index()
        {
            //get all games from the db
            List<Crate> crates = await _context.Crates.ToListAsync();
            //List<Crate> crates = await (from game in _context.Crates
                                  //select game).ToListAsync();
            //show on web page
            return View(crates);
        }

        //displays page to user. To add razor view click on create()
        public IActionResult Create()
        {
            return View();
        }
        //obtains data from client and adds to datbase
        [HttpPost]
        //async allows multi-processing. Turn IActionResult into a Task of the IActionResult Type
        //also must add await in front of _context and change SaveChanges() to SaveChangesAsyn
        public async Task<IActionResult> Create(Crate crate)
        {
            if (ModelState.IsValid)
            {
                _context.Crates.Add(crate); //prepares insert
                //thanks to async computer resources can be alocated elsewhere while this is pending a response
                await _context.SaveChangesAsync(); //executes pending insert
                //Show success message on page
                ViewData["Message"] = $"{crate.Title} was added successfully!";

                return View();
            }
            //if not valid return object back
            return View(crate);
        }

        public async Task<IActionResult> Edit(int id)
        {
            //query server for a specific ID, use async to allow for multi-processing
            Crate crateToEdit = await _context.Crates.FindAsync(id);
            if (crateToEdit == null)
            {
                return NotFound();
            }
            return View(crateToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Crate crateModel)
        {
            if (ModelState.IsValid)
            {
                _context.Crates.Update(crateModel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(crateModel);
        }

        public IActionResult Details()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}
