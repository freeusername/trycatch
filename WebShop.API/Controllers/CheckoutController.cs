using System.Web.Http;

namespace WebShop.Controllers
{
    [RoutePrefix("api/Checkout")]
    public class CheckoutController : ApiController
    {
        [Route("")]
        [Authorize]
        public IHttpActionResult Post()
        {
            return Ok($"api response for {User.Identity.Name}: success result");
        }
    }
}
