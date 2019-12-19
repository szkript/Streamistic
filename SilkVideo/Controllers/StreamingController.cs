using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SilkVideo.Models;
using System.Threading.Tasks;

namespace SilkVideo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreamingController : ControllerBase
    {
        private readonly SilkVideoContext _context;
        private readonly UserManager<User> _userManager;

        public StreamingController(SilkVideoContext context, UserManager<User> usermanager)
        {
            _context = context;
            _userManager = usermanager;
        }

        [HttpGet]
        public async Task<Video> GetStreamById(long Id)
        {
            var video = _context.Videos.Find(Id);
            return video;
        }

        [HttpPost]
        public async Task<IActionResult> StartLiveStream(Video stream)
        {
            var user = await _userManager.FindByNameAsync("ezaz");
            Video liveStream = stream;
            user.Videos.Add(liveStream);
            _context.Update(user);
            var updatedStream = await _context.Videos.FindAsync(stream);
            return Redirect("localhost:/stream/"+ updatedStream.Id);
        }


    }
}