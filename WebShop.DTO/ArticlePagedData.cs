using System.Collections.Generic;

namespace WebShop.DTO
{
    public class ArticlePagedData
    {
        public List<Article> Articles { get; set; }
        public int TotalItemsCount { get; set; }
    }
}
