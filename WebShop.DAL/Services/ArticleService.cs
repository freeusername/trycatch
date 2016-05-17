using System;
using System.Collections.Generic;
using System.Linq;
using WebShop.DAL.Contracts;
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

        public IEnumerable<Article> GetArticles(Guid[] ids)
        {
            return _repository.GetAll()
                .ToList() // TODO don't load everything into memory, to be refactored
                .Where(o => ids.Contains(o.Id));
        }

        public Article GetById(Guid id)
        {
            return _repository.GetAll()
                .SingleOrDefault(o => o.Id == id);
        }

        public ArticlePagedData GetPaged(int page, int pageSize)
        {
            var all = _repository.GetAll();

            var articles = all
                .OrderBy(o => o.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            return new ArticlePagedData
            {
                Articles = articles,
                TotalItemsCount = all.Count()
            };
        }
    }
}
