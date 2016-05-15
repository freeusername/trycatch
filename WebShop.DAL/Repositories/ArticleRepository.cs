using System;
using System.Linq;
using WebShop.DAL.Models;

namespace WebShop.DAL.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly IDatabaseContext _context;

        public ArticleRepository(IDatabaseContext context)
        {
            _context = context;
        }

        public IQueryable<Article> GetAll()
        {
            return _context.Articles;
        }

        public Article Add(Article entity)
        {
            return _context.Articles.Add(entity);
        }

        public void Delete(Article entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(Article entity)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
