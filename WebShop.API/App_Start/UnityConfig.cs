using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using WebShop.DAL;
using WebShop.DAL.Repositories;
using WebShop.DAL.Services;

namespace WebShop
{
    public static class UnityConfig
    {
        public static UnityContainer RegisterComponents()
        {
            var container = new UnityContainer();
            
            container.RegisterType<IDatabaseContext, AuthContext>(new PerThreadLifetimeManager());
            container.RegisterType<IArticleRepository, ArticleRepository>();
            container.RegisterType<IArticleService, ArticleService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

            return container;
        }
    }
}