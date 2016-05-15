using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using WebShop.ApiProxy;
using WebShop.DTO.Model;
using WebShop.Mvc.Models;

namespace WebShop.Mvc.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IApiProxy _apiProxy;
        private const string XsrfKey = "XsrfId";

        public AccountController(IApiProxy apiProxy)
        {
            _apiProxy = apiProxy;
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var response = _apiProxy.RequestToken(model.Email, model.Password);

            if (response.StatusCode == HttpStatusCode.OK)
            {
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
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var registerModel = AutoMapper.Mapper.Map<RegisterViewModel, RegisterModel>(model);
                var response = _apiProxy.Register(registerModel);

                if (response.StatusCode == HttpStatusCode.OK)
                    return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            System.Web.HttpContext.Current.Session["token"] = null;

            return RedirectToAction("Index", "Home");
        }
    }
}