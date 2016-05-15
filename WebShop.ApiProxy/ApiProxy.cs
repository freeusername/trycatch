using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using WebShop.DTO;
using WebShop.DTO.Model;

namespace WebShop.ApiProxy
{
    public class ApiProxy : IApiProxy
    {
        private const string GetTokenUrl = "Token";
        private const string AccountRegisterUrl = "api/Account/Register";
        private const string GetArticlesUrl = "api/Articles/{0}/{1}"; // 0 - pageNumber, 1- pageSize

        private readonly RestClient _authClient;

        public ApiProxy(string apiUrl)
        {
            _authClient = new RestClient(apiUrl);
            AutoMapper.Mapper.Initialize(opts => opts.CreateMissingTypeMaps = true);
        }

        public ICustomHttpResult<TokenDTO> RequestToken(string email, string password)
        {
            var request = new RestRequest(GetTokenUrl, Method.POST);

            request.AddParameter("grant_type", "password");
            request.AddParameter("username", email);
            request.AddParameter("password", password);

            var response = _authClient.Execute<TokenDTO>(request);

            return new CustomHttpResult<TokenDTO>(response);
        }

        public ICustomHttpResult Register(RegisterModel registerModel)
        {
            var request = new RestRequest(AccountRegisterUrl, Method.POST);
            request.AddObject(registerModel);

            var response = _authClient.Execute<object>(request);

            return new CustomHttpResult<object>(response);
        }

        public ICustomHttpResult<ArticlePagedData> GetArticles(int page, int pageSize)
        {
            var url = string.Format(GetArticlesUrl, page, pageSize);
            var request = new RestRequest(url, Method.GET);

            var response = _authClient.Execute<ArticlePagedData>(request);

            return new CustomHttpResult<ArticlePagedData>(response);
        }
    }
}
