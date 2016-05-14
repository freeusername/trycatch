using System.Collections.Generic;
using WebShop.DAL.Models;
using WebShop.DAL.Repositories;

namespace WebShop.DAL.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _repository;

        public ArticleService(IArticleRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Article> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
