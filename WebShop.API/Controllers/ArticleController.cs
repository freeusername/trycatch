using System;
using System.Collections.Generic;
using System.Web.Http;
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

        /// <summary>
        /// Returns paging data
        /// /api/Articles/{page}/{pageSize}
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("{page}/{pageSize}")]
        public IHttpActionResult GetPagedData(int page, int pageSize)
        {
            var articles = _articlesService.GetPaged(page, pageSize);
            return Ok(articles);
        }

        /// <summary>
        /// Return particular article
        /// /api/Articles/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("")]
        public IHttpActionResult Get(Guid id)
        {
            var articles = _articlesService.GetById(id);
            return Ok(articles);
        }

        /// <summary>
        /// Get particular articles by its ids otherwise returns all articles
        /// /api/articles
        /// /api/articles?ids={id}&ids={id}...
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [Route("")]
        public IHttpActionResult GetArticles([FromUri]Guid[] ids)
        {
            var articles = ids == null
                ? _articlesService.GetAll()
                : _articlesService.GetArticles(ids);

            return Ok(articles);
        }
    }

    public interface IArticleController
    {
    }
}