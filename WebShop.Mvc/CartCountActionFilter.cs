using System.Web;
using System.Web.Mvc;

namespace WebShop.Mvc
{
    public class CartCountActionFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.CartCount = HttpContext.Current.Session.Keys.Count;
        }
    }
}