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
            return new[]
            {
                new Article
                {
                    Id = Guid.NewGuid(),
                    Name = "Coockies",
                    Description = "Netherlands coockies",
                    PriceExclVat = 1.98M
                },
                new Article
                {
                    Id = Guid.NewGuid(),
                    Name = "Orange juice",
                    Description = "100% pure juice",
                    PriceExclVat = 0.99M
                },
                new Article
                {
                    Id = Guid.NewGuid(),
                    Name = "Snickers",
                    Description = "Best chocolate bar in the world",
                    PriceExclVat = 0.49M
                }
            }.AsQueryable();
        }

        public Article Add(Article entity)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
