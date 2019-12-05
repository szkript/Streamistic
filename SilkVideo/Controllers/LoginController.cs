using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SilkVideo.Models;
using System.Threading.Tasks;

namespace SilkVideo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<User> usermanager;
        private readonly SignInManager<User> signinmanager;

        public LoginController(UserManager<User> manager, SignInManager<User> signInManager)
        {
            usermanager = manager;
            signinmanager = signInManager;
        }

        [HttpPost]
        public async Task<Microsoft.AspNetCore.Identity.SignInResult> Login(User user)
        {
            var result = await signinmanager.PasswordSignInAsync(user.UserName, user.Password, false, false);
            return result;
        }

        [HttpGet("logout")]
        public async void Logout()
        {
            await signinmanager.SignOutAsync();
        }
    }
}