using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SilkVideo.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace SilkVideo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreamingController : ControllerBase
    {
        RecordService.ServiceClient recordService = new RecordService.ServiceClient();
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
        [Route("record")]
        public async Task<IActionResult> StartLiveStreamRecord()
        {
            Video video = new Video();
            video.Path = null;
            string username = this.User.Identity.Name;
            DateTime uploadDate = DateTime.Now;
            string formattedUploadDate = "" + uploadDate.Year + uploadDate.Month + uploadDate.Day + uploadDate.Hour + uploadDate.Minute + uploadDate.Second;
            if (username != null)
            {
                video.Description = "In Progress";
                video.Path = "Videos/" + username + formattedUploadDate + ".mp4";
                video.UploadTime = uploadDate;
                bool videoIsRecorded = await recordService.StreamRecordingAsync(username, formattedUploadDate);
                if (videoIsRecorded)
                {
                    await SaveVideoAsync(username, video);
                }
            }

            return Ok();
        }

        [HttpPost]
        public async Task<string> StartLiveStream()
        {
            string username = this.User.Identity.Name;
            /*if (username != null)
            {
                await StreamRecording(username);
            }*/
            return username;
            //var user = await _userManager.FindByNameAsync("ezaz");
            //Video liveStream = stream;
            //user.Videos.Add(liveStream);
            //_context.Update(user);
            //var updatedStream = await _context.Videos.FindAsync(stream);
            //return Redirect("localhost:/stream/"+ updatedStream.Id);
        }


        private async Task SaveVideoAsync(string username, Video video)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user.Videos == null)
            {
                user.Videos = new List<Video>();
            }
            user.Videos.Add(video);
            await _userManager.UpdateAsync(user);

        }

    }
}