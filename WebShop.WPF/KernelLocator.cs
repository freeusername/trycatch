using Ninject;
using WebShop.ApiProxy;
using WebShop.WPF.ViewModels;

namespace WebShop.WPF
{
    public class KernelLocator
    {
        public static IKernel Container { get; }

        static KernelLocator()
        {
            Container = new StandardKernel();

            var authServerUrl = System.Configuration.ConfigurationManager.AppSettings["authApiUrl"];

            Container.Bind<IApiProxy>().To<ApiProxy.ApiProxy>().WithConstructorArgument(authServerUrl);
            Container.Bind<IRegistrationWindow>().To<RegistrationWindow>();
            Container.Bind<IRegistrationViewModel>().To<RegistrationViewModel>();
            Container.Bind<ILoginWindow>().To<LoginWindow>();
            Container.Bind<ILoginViewModel>().To<LoginViewModel>();
            Container.Bind<IMainViewModel>().To<MainViewModel>(); 
        }
    }
}
