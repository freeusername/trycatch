using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebShop.DAL.Models;
using WebShop.DAL.Repositories;

namespace WebShop.DAL.Services
{
    public class AuthService
    {
        private readonly IClientRepository _clientRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthService(IClientRepository clientRepository, UserManager<IdentityUser> userManager)
        {
            _clientRepository = clientRepository;
            _userManager = userManager;
            //_userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            var user = new IdentityUser
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            var user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public Client FindClient(string clientId)
        {
            var client = _clientRepository.GetAll()
                .SingleOrDefault(o => o.Id == clientId);

            return client;
        }
    }
}