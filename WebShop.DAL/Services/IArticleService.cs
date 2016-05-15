using System;
using System.Collections.Generic;
using WebShop.DAL.Models;

namespace WebShop.DAL.Services
{
    public interface IArticleService
    {
        IEnumerable<Article> GetAll();
        Article GetById(Guid id);
    }
}