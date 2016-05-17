using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebShop.ApiProxy;
using WebShop.Mvc.Helpers;

namespace WebShop.Mvc.Controllers
{
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
                var articlesResponse = _apiProxy.GetArticles(articleIds.ToArray());
                if (articlesResponse.StatusCode == HttpStatusCode.OK)
                    return View(articlesResponse.Data);
                else if (articlesResponse.StatusCode == HttpStatusCode.Unauthorized)
                    return RedirectToAction("Login", "Account");
                else
                {
                    throw new Exception("Internal Error. Please contact an administrator.");
                }
            }

            return View("EmptyShoppingCart");
        }

        [HttpGet]
        public dynamic Add(Guid id)
        {
            //TODO Move all Session article related stuff to the separate Service
            if (Session[SessionHelper.ArticlesKeyName] == null)
            {
                Session[SessionHelper.ArticlesKeyName] = new List<Guid> { id };
                return 1;
            }

            var idsList = Session[SessionHelper.ArticlesKeyName] as List<Guid>;
            if (idsList == null)
                throw new Exception("Internal Error. Please contact an administrator.");

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

            throw new Exception("Internal Erro. Please contact an administrator");
        }
    }
}