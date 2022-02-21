using Microsoft.AspNetCore.Mvc;
using ECommerceSite.Models;

namespace ECommerceSite.Controllers
{
    public class MembersController : Controller
    {
        private readonly Data.CrateContext _context;
        public MembersController(Data.CrateContext context)
        {
            _context = context;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel regModel)
        {
            if (ModelState.IsValid)
            {
                //Map RegisterViewModel data to Member object
                //Creates a view of the member object limiting to only
                //specified properties in RegisterViewModel class in Member class
                Member newMember = new()
                {
                    Email = regModel.Email,
                    Password = regModel.Password,
                };

                _context.Members.Add(newMember);
                await _context.SaveChangesAsync();
                //redirect
                return RedirectToAction("Index", "Home");
            }
            return View(regModel);
        }
    }
}
