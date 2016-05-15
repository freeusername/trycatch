using System.Net;
using RestSharp;

namespace WebShop.ApiProxy
{
    public interface ICustomHttpResult
    {
        HttpStatusCode StatusCode { get; }
    }

    public interface ICustomHttpResult<out T> : ICustomHttpResult
        where T : class
    {
        T Data { get; }
    }

    public class CustomHttpResult<T> : ICustomHttpResult<T>
        where T : class
    {
        private readonly T _data;
        public T Data
        {
            get { return _data; }
        }

        private readonly HttpStatusCode _statusCode;
        public HttpStatusCode StatusCode
        {
            get { return _statusCode; }
        }

        public CustomHttpResult()
        {
            
        }

        public CustomHttpResult(IRestResponse<T> restResponse)
        {
            _data = restResponse.Data;
            _statusCode = restResponse.StatusCode;
        }
    }
}
