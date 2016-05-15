using System.Collections.Generic;
using WebShop.DAL.Models;

namespace WebShop.DAL.Contracts
{
    public class ArticlePagedData
    {
        public IEnumerable<Article> Articles { get; set; }
        public int TotalItemsCount { get; set; }
    }
}
