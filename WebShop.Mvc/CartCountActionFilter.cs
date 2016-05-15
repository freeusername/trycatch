using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using WebShop.Mvc.Helpers;

namespace WebShop.Mvc
{
    public class CartCountActionFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var cartValue = HttpContext.Current.Session[SessionHelper.ArticlesKeyName] as List<Guid>;

            filterContext.Controller.ViewBag.CartCount = cartValue != null
                ? cartValue.Count
                : 0;
        }
    }
}