using System;
using System.Collections.Generic;
using RestSharp;
using WebShop.DTO;
using WebShop.DTO.Model;

namespace WebShop.ApiProxy
{
    public class ApiProxy : IApiProxy
    {
        private const string GetTokenUrl = "Token";
        private const string AccountRegisterUrl = "api/Account/Register";
        private const string PagedArticlesUrl = "api/Articles/{0}/{1}"; // 0 - pageNumber, 1- pageSize
        private const string AllArticlesUrl = "api/Articles";
        private const string CheckoutUrl = "api/Checkout";

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
            var url = string.Format(PagedArticlesUrl, page, pageSize);
            var request = new RestRequest(url, Method.GET);

            var response = _authClient.Execute<ArticlePagedData>(request);

            return new CustomHttpResult<ArticlePagedData>(response);
        }

        public ICustomHttpResult<IEnumerable<Article>> GetArticles(Guid[] articleIds)
        {
            var request = new RestRequest(AllArticlesUrl, Method.GET);
            foreach (var id in articleIds)
                request.AddParameter("ids", id);

            var response = _authClient.Execute<List<Article>>(request);

            return new CustomHttpResult<List<Article>>(response);
        }

        public ICustomHttpResult<object> Checkout(string token)
        {
            var request = new RestRequest(CheckoutUrl, Method.POST);
            request.AddHeader("Authorization", "Bearer " + token);

            var response = _authClient.Execute<object>(request);

            return new CustomHttpResult<object>(response);
        }
    }
}
