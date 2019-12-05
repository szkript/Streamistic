using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SilkVideo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SilkVideo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly SilkVideoContext _context;
        private readonly UserManager<User> _userManager;

        public VideoController(SilkVideoContext context, UserManager<User> usermanager)
        {
            _context = context;
            _userManager = usermanager;
        }

        [HttpGet]
        public async Task<List<Video>> GetAllVideos()
        {
            List<Video> videos = new List<Video>();
            await foreach (Video video in _context.Videos)
            {
                videos.Add(video);
            }

            return videos;
        }

        [HttpGet("{Id}")]
        public async Task<Video> GetVideoById(long Id)
        {
            var video = await _context.Videos.FindAsync(Id);
            return video;

        }

        [HttpPost]
        public async Task<bool> UploadVideoData([FromForm]Microsoft.AspNetCore.Http.IFormFile body)
        {
            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await body.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
                if (!Directory.Exists("./Videos"))
                {
                    Directory.CreateDirectory("Videos");
                }
            }

            var filename = "./Videos/"+ body.FileName;
            var contentType = body.ContentType;
            bool fileWrite = ByteArrayToFile(filename, fileBytes);
            var user = await _userManager.FindByNameAsync("ezaz");
            if (user.Videos == null)
            {
                user.Videos = new List<Video>();
            }

            Video video = new Video();
            video.Path = filename;
            video.UploadTime = DateTime.Now;
            video.Description = "in Progress";
            user.Videos.Add(video);

            await _userManager.UpdateAsync(user);
            return true;

        }
        public bool ByteArrayToFile(string fileName, byte[] byteArray)
        {
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
                return false;
            }
        }
    }

}