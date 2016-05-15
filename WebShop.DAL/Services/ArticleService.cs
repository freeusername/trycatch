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

        public Article GetById(Guid id)
        {
            return _repository.GetAll()
                .SingleOrDefault(o => o.Id == id);
        }

        public ArticlePagedData GetPaged(int page, int pageSize)
        {
            var all = _repository.GetAll();

            var articles = all
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
