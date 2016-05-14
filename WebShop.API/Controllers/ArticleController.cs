using System;
using System.Web.Http;
using System.Web.Routing;
using WebShop.DAL.Models;
using WebShop.DAL.Services;

namespace WebShop.Controllers
{
    [RoutePrefix("api/Articles")]
    public class ArticleController : ApiController, IArticleController
    {
        private readonly IArticleService _articlesService;
        
        public ArticleController(IArticleService articlesService)
        {
            _articlesService = articlesService;
        }

        //[Authorize]
        [Route("")]
        public IHttpActionResult Get()
        {
            var articles = _articlesService.GetAll();
            return Ok(articles);
        }
    }

    public interface IArticleController
    {
    }
}