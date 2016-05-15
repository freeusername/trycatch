using System;
using System.Web.Http;
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

        // TODO [Authorize]
        [Route("")]
        public IHttpActionResult Get()
        {
            var articles = _articlesService.GetAll();
            return Ok(articles);
        }

        [Route("{page}/{pageSize}")]
        public IHttpActionResult GetPagedData(int page, int pageSize)
        {
            var articles = _articlesService.GetPaged(page, pageSize);
            return Ok(articles);
        }

        [Route("")]
        public IHttpActionResult Get(Guid id)
        {
            var articles = _articlesService.GetById(id);
            return Ok(articles);
        }
    }

    public interface IArticleController
    {
    }
}