using System;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Unity.WebApi;
using WebShop;
using WebShop.Providers;

[assembly: OwinStartup(typeof(Startup))]

namespace WebShop
{
    public class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            var container = UnityConfig.RegisterComponents();
            HttpConfiguration config = new HttpConfiguration
            {
                DependencyResolver = new UnityDependencyResolver(container)
            };

            var simpleAuthorizationServerProvider = (SimpleAuthorizationServerProvider)container.Resolve(typeof(SimpleAuthorizationServerProvider), string.Empty);//new SimpleAuthorizationServerProvider();
            ConfigureOAuth(app, simpleAuthorizationServerProvider);

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

            // Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataBaseContext, Configuration>());
        }

        public void ConfigureOAuth(IAppBuilder app, SimpleAuthorizationServerProvider authServerProvider)
        {
            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {

                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(15),
                Provider = authServerProvider
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions); //askp
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
        }
    }

}