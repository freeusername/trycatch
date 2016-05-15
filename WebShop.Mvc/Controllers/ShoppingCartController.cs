using System;
using System.Web.Mvc;

namespace WebShop.Mvc.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public dynamic Add(string id)
        {
            Session[id] = 1;
            return Session.Keys.Count;
        }
    }
}