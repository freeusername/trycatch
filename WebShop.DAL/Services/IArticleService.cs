using System;
using System.Collections.Generic;
using WebShop.DAL.Contracts;
using WebShop.DAL.Models;

namespace WebShop.DAL.Services
{
    public interface IArticleService
    {
        IEnumerable<Article> GetAll();
        Article GetById(Guid id);
        ArticlePagedData GetPaged(int page, int pageCount);
    }
}