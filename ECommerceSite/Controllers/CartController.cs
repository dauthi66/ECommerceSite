using ECommerceSite.Data;
using ECommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceSite.Controllers
{
    public class CartController : Controller
    {
        private readonly CrateContext _crateContext;

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
            //TODO: add item to shopping cart cookie
            TempData["Message"] = "Crate successfully added to cart!";
            return RedirectToAction("Index", "Crate");
        }
    }
}
