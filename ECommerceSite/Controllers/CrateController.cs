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
                    //int id must match id in MapControllerRoute() from program.cs
                    // int? means its optional, do not need page number for first load.
        public async Task<IActionResult> Index(int? id)
        {
            //corrects number of crates to skip per page
            const int PageOffset = 1;
            const int NumCratesToDisplayPerPage = 3;
            //using ternary operator ? method - what happens when true : what happens when false;
            //if id has a value, it is that value, otherwise it is 1.
            int currPage = id.HasValue ? id.Value : 1;
            //find how many pages we will need for all the products, rounding up
            double exactNumOfPages = (double)await _context.Crates.CountAsync() / NumCratesToDisplayPerPage;
            int totalNumOfPages = Convert.ToInt32(Math.Ceiling(exactNumOfPages));

            //even shorter method set curr page to id, unless null then set to 1
            //int currPage = id ?? 1;

            //normal way to write above:
            //if (id.HasValue)
            //{
            //    currPage = id.Value;
            //}
            //else
            //{
            //    currPage = 1;
            //}

            //get all games from the db
            List<Crate> crates = await _context.Crates
                //skip this many pages of crates
                .Skip(NumCratesToDisplayPerPage * (currPage - PageOffset))
                //take this many crates from database
                .Take(NumCratesToDisplayPerPage)
                .ToListAsync();
            //List<Crate> crates = await (from game in _context.Crates
            //                            select game)
            //                            .Skip(NumCratesToDisplayPerPage * (currPage - PageOffset))                   
            //                            .Take(NumCratesToDisplayPerPage)                                        
            //                            .ToListAsync();
            //show on web page

            //pass data to catalogue model
            CrateCatalogueViewModel catalogueModel = new(crates, totalNumOfPages, currPage);
            //pass model to view, make sure to change from IEnumerable on view
            return View(catalogueModel);
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
                await _context.Crates.AddAsync(crate); //prepares insert
                //thanks to async computer resources can be alocated elsewhere while this is pending a response
                await _context.SaveChangesAsync(); //executes pending insert
                //Show success message on page
                TempData["Message"] = $"{crate.Title} was added successfully!";
                return RedirectToAction("Index");
            }
            //if not valid return object back
            return View(crate);
        }

        public async Task<IActionResult> Edit(int id)
        {
            //query server for a specific primary key, use async to allow for multi-processing
            Crate? crateToEdit = await _context.Crates.FindAsync(id);
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
                //return RedirectToAction("Index");
                TempData["Message"] = $"{crateModel.Title} was edited successfully!";
                return RedirectToAction("Index");
            } 
            return View(crateModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            Crate? crateDetails = await _context.Crates.FindAsync(id);

            if (crateDetails == null)
            {
                return NotFound();
            }
            return View(crateDetails);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Crate? crateToDelete = await _context.Crates.FindAsync(id);

            if (crateToDelete == null)
            {
                return NotFound();
            }
            return View(crateToDelete);
        }

        // ActionName allows you to post as Delete, but be named something else to avoid method conflicts
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Crate? crateToDelete = await _context.Crates.FindAsync(id);

            if (crateToDelete != null)
            {
                _context.Crates.Remove(crateToDelete);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"{crateToDelete.Title} was deleted successfully!";
                return RedirectToAction("Index");
            }

            TempData["Message"] = "This game was already deleted or does not exist";
            return RedirectToAction("Index");
        }
    }
}
