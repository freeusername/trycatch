using System;
using Microsoft.Practices.Unity;
using WebShop.ApiProxy;

namespace WebShop.Mvc.App_Start
{
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            var authServerUrl = System.Configuration.ConfigurationManager.AppSettings["authApiUrl"];

            container.RegisterType<IApiProxy, ApiProxy.ApiProxy>(new InjectionConstructor(authServerUrl));
        }
    }
}
