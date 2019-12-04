using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SilkVideo.Models;

namespace SilkVideo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {

        private readonly UserManager<User> usermanager;

        public RegistrationController(UserManager<User> manager)
        {
            usermanager = manager;
        }


        [HttpGet("{username}")]
        public async Task<bool> IsUsernameInUse(string username)
        {
            var user = await usermanager.FindByNameAsync(username);
            
            if (user != null)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }

        [HttpPost]
        public async Task<bool> Registration(User user)
        {
            var iduser = new IdentityUser { UserName = user.UserName };
            var result = await usermanager.CreateAsync(user, user.Password);
            return result.Succeeded;
        }
    }
}