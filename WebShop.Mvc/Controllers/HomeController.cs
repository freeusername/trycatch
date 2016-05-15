using System;
using System.Net;
using System.Web.Mvc;
using PagedList;
using WebShop.ApiProxy;
using WebShop.Mvc.Models;

namespace WebShop.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private const int PageSize = 5;
        private readonly IApiProxy _apiProxy;

        public HomeController(IApiProxy apiProxy)
        {
            _apiProxy = apiProxy;
        }

        public ActionResult Index(int page = 1)
        {
            var articlesResponse = _apiProxy.GetArticles(page, PageSize);
            if (articlesResponse.StatusCode == HttpStatusCode.OK)
            {
                return View(new ArticlesModel
                {
                    Articles = new StaticPagedList<DTO.Article>(articlesResponse.Data.Articles, page, PageSize, articlesResponse.Data.TotalItemsCount)
                });
            }

            throw new Exception("Smth went wrong");
        }
    }
}