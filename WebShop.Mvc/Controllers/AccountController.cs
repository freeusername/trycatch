using System.Net;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using RestSharp;
using RestSharp.Extensions;
using WebShop.DTO;
using WebShop.Mvc.Models;

namespace WebShop.Mvc.Controllers
{
    [System.Web.Mvc.Authorize]
    public class AccountController : Controller
    {
        private const string XsrfKey = "XsrfId";
        private readonly RestClient _authClient;

        public AccountController()
        {
            var authServerUrl = System.Configuration.ConfigurationManager.AppSettings["authApiUrl"];

            // TODO move this logic out of here
            _authClient = new RestClient(authServerUrl);
        }

        //
        // GET: /Account/Login
        [System.Web.Mvc.AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var request = new RestRequest("Token", Method.POST);

            request.AddParameter("grant_type", "password");
            request.AddParameter("username", model.Email);
            request.AddParameter("password", model.Password);

            var response = _authClient.Execute<TokenDTO>(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                //TODO
                var token = response.Data.Access_Token;
                System.Web.HttpContext.Current.Session["token"] = token;
                System.Web.HttpContext.Current.Session.Timeout = 15;
                FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                //TODO handle it
            }
            return View(model);
        }

        //
        // GET: /Account/Register
        [System.Web.Mvc.AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.AllowAnonymous]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var request = new RestRequest("api/Account/Register", Method.POST);
                request.AddObject(new
                {
                    UserName = model.Email,
                    Password = model.Password,
                    ConfirmPassword = model.ConfirmPassword
                });

                var response = _authClient.Execute<object>(request);

                if (response.StatusCode == HttpStatusCode.OK)
                    return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        //
        // POST: /Account/LogOff
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            System.Web.HttpContext.Current.Session["token"] = null;

            return RedirectToAction("Index", "Home");
        }

        #region Helpers


        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}