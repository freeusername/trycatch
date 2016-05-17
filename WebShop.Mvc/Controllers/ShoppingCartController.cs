using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebShop.ApiProxy;
using WebShop.Mvc.Helpers;

namespace WebShop.Mvc.Controllers
{
    // TODOMove to session service
    public class ShoppingCartController : Controller
    {
        private readonly IApiProxy _apiProxy;

        public ShoppingCartController(IApiProxy apiProxy)
        {
            _apiProxy = apiProxy;
        }

        // GET: ShoppingCart
        public ActionResult Index()
        {
            var articleIds = Session[SessionHelper.ArticlesKeyName] as List<Guid>;

            if (articleIds != null && articleIds.Any())
            {
                var articles = _apiProxy.GetArticles(articleIds.ToArray());
                //TODO check response.StatusCode

                return View(articles.Data);
            }

            return View("EmptyShoppingCart");
        }

        [HttpGet]
        public dynamic Add(Guid id)
        {
            if (Session[SessionHelper.ArticlesKeyName] == null)
            {
                Session[SessionHelper.ArticlesKeyName] = new List<Guid> { id };
                return 1;
            }

            var idsList = Session[SessionHelper.ArticlesKeyName] as List<Guid>;
            if (idsList == null)
                throw new Exception("Smth went wrong"); //TODO

            if (!idsList.Contains(id))
                idsList.Add(id);

            return idsList.Count;
        }

        [Authorize]
        public ActionResult Checkout()
        {
            var response = _apiProxy.Checkout(Session["token"] as string);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Session.Remove(SessionHelper.ArticlesKeyName);
                return View("SuccessfullCheckout");
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Account");
            }

            throw new Exception("Contact an administrator"); //TODO
        }
    }
}