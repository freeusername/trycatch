using WebShop.DTO;
using WebShop.DTO.Model;

namespace WebShop.ApiProxy
{
    public interface IApiProxy
    {
        ICustomHttpResult<TokenDTO> RequestToken(string email, string password);
        ICustomHttpResult Register(RegisterModel registerModel);
        ICustomHttpResult<ArticlePagedData> GetArticles(int page, int countOnPage);
    }
}