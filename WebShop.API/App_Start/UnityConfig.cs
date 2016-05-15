using System.Data.Entity;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Unity.WebApi;
using WebShop.DAL;
using WebShop.DAL.Repositories;
using WebShop.DAL.Services;
using WebShop.Providers;

namespace WebShop
{
    public static class UnityConfig
    {
        public static UnityContainer RegisterComponents()
        {
            var container = new UnityContainer();

            // Contexts
            container.RegisterType<IDatabaseContext, DataBaseContext>(new PerThreadLifetimeManager());

            // Repositories
            container.RegisterType<IArticleRepository, ArticleRepository>();
            container.RegisterType<IClientRepository, ClientRepository>();

            // Asp.Net Identity types
            container.RegisterType<IUserStore<IdentityUser>, UserStore<IdentityUser>>(
                new InjectionFactory(c => new UserStore<IdentityUser>(c.Resolve<IDatabaseContext>() as DbContext)));
            container.RegisterType<UserManager<IdentityUser>>(
                new InjectionConstructor(typeof(IUserStore<IdentityUser>)));
            container.RegisterType<SimpleAuthorizationServerProvider>();

            // Application Services
            container.RegisterType<IArticleService, ArticleService>();
            container.RegisterType<AuthService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

            return container;
        }
    }
}