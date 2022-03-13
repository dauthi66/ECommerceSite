using ECommerceSite.Data;
using ECommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommerceSite.Controllers
{
    public class CartController : Controller
    {
        private readonly CrateContext _crateContext;
        private const string ShoppingCartKey = "ShoppingCart";

        public CartController(CrateContext crateContext)
        {
            _crateContext = crateContext;
        }

        //ID used as identifier for asp.net here and in controller
        public IActionResult Add(int id)
        {
            Crate? crateToAdd = _crateContext.Crates.Where(c => c.CrateId == id).SingleOrDefault();

            if (crateToAdd == null)
            {
                TempData["Message"] = "Sorry that game no longer exists.";
                return RedirectToAction("Index", "Crate");
            }

            CartCrateViewModel cartCrate = new()
            {
                CrateId = crateToAdd.CrateId,
                Title = crateToAdd.Title,
                Price = crateToAdd.Price
            };

            //Put all the object into a collection with any others added
            List<CartCrateViewModel> cartCrates = GetExistingCrateCookie();
            cartCrates.Add(cartCrate);
            CreateCartCookie(cartCrates);

            //TODO: add item to shopping cart cookie
            TempData["Message"] = "Crate successfully added to cart!";
            return RedirectToAction("Index", "Crate");
        }

        private void CreateCartCookie(List<CartCrateViewModel> cartCrates)
        {
            //create a string version of the crate
            string cartCookie = JsonConvert.SerializeObject(cartCrates);
            //Create cookie append(unique string key, object to turn to string, create new options)
            HttpContext.Response.Cookies.Append(ShoppingCartKey, cartCookie, new CookieOptions()
            {
                Expires = DateTime.Now.AddYears(1)
            });
        }

        /// <summary>
        /// Return the curent list of video games in the users shopping cart
        /// cart cookie, if none an empty list will be returned
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private List<CartCrateViewModel> GetExistingCrateCookie()
        {
            //ask user for cookie
            string? cookie = HttpContext.Request.Cookies[ShoppingCartKey];
            if (cookie == null)
            {
                //return a new list to use
                return new List<CartCrateViewModel>();
            }

            //return a list with selected items already in cart
            return JsonConvert.DeserializeObject<List<CartCrateViewModel>>(cookie);
        }

        public IActionResult Checkout()
        {
            List<CartCrateViewModel> cartCrates = GetExistingCrateCookie();
            return View(cartCrates);
        }

        public IActionResult Remove(int id)
        {
            //get cookie list
            List<CartCrateViewModel> cartCrates = GetExistingCrateCookie();
            //find which one to remove and remove it
            CartCrateViewModel? crateToFind = cartCrates.FirstOrDefault(c => c.CrateId == id);
            cartCrates.Remove(crateToFind);
            //recreate cookie
            CreateCartCookie(cartCrates);

            return RedirectToAction("Checkout");
        }
    }
}
