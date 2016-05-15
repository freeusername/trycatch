using PagedList;

namespace WebShop.Mvc.Models
{
    public class ArticlesModel
    {
        public StaticPagedList<DTO.Article> Articles { get; set; }
    }
}