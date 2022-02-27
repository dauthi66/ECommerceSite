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
                //log user in
                LogUserIn(newMember.Email);
                //redirect
                return RedirectToAction("Index", "Home");
            }
            return View(regModel);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                // Check DB for credentials
                Member? memberSearch = (from member in _context.Members
                                      where member.Email == loginModel.Email &&
                                            member.Password == loginModel.Password
                                      select member).SingleOrDefault();
                if (memberSearch != null)
                {       //start session with key "email"
                    LogUserIn(loginModel.Email);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(String.Empty, "Credentials not found!");

            }
            //return page if no record found, or ModelState is invalid
            return View(loginModel);
        }

        /// <summary>
        /// Sets the Email for the current user so it can be checked
        /// to see if the user is logged in. If not, "Email" session key null
        /// </summary>
        /// <param name="email"></param>
        private void LogUserIn(string email)
        {
            HttpContext.Session.SetString("Email", email);
        }

        public IActionResult Logout()
        {
            //clear session and return to main page
            HttpContext.Session?.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
