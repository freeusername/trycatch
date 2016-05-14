using System;
using System.Web;

namespace WebShop.Mvc.Modules
{
    public class TokenModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += OnPreSendRequestHeaders;
        }

        public void Dispose() { }

        void OnPreSendRequestHeaders(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session == null)
                return;

            var token = HttpContext.Current.Session["token"];

            if (token == null)
                return;

            var authorizationValue = "Bearer " + token;
            HttpContext.Current.Response.Headers.Add("Authorization", authorizationValue);
        }
    }
}