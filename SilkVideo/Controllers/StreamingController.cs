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
            string username = this.User.Identity.Name;
            if (username != null)
            {
                await StreamRecording(username);
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

        private async Task StreamRecording(string username)
        {
            Video video = new Video();
            video.Path = null;
            Process rtmpdump = new Process();
            rtmpdump.StartInfo.FileName = "cmd.exe";
            rtmpdump.StartInfo.Arguments = "/C rtmpdump -q --rtmp rtmp://64.225.24.130:1935/show --playpath " + username + " -o Videos/"+username+".flv --live";
            rtmpdump.StartInfo.UseShellExecute = true;
            rtmpdump.StartInfo.RedirectStandardOutput = false;
            await Task.Run(() => {
                int counter = 0;
                while (true)
                {
                    rtmpdump.Start();
                    rtmpdump.WaitForExit();
                    FileInfo file = new FileInfo("Videos/"+ username + ".flv");
                    if (file.Length > 0)
                    {
                        DateTime uploadDate = DateTime.Now;
                        string formattedUploadDate ="" + uploadDate.Year + uploadDate.Month + uploadDate.Day + uploadDate.Hour + uploadDate.Minute + uploadDate.Second;
                        
                        video.Description = "In Progress";
                        video.Path = "Videos/" + username + formattedUploadDate + ".mp4";
                        video.UploadTime = uploadDate;
                        Process ffmpeg = new Process();
                        ffmpeg.StartInfo.FileName = "cmd.exe";
                        ffmpeg.StartInfo.Arguments = "/C ffmpeg -i Videos/"+username+".flv -c:v libx264 -crf 19 -strict experimental Videos/"+username+ formattedUploadDate +".mp4";
                        ffmpeg.StartInfo.UseShellExecute = true;
                        ffmpeg.StartInfo.RedirectStandardOutput = false;
                        ffmpeg.Start();
                        ffmpeg.WaitForExit();
                        file.Delete();
                        
                        break;
                    }

                    if (counter > 10)
                    {
                        file.Delete();
                        break;
                    }

                    counter++;
                }
            });

            if (video.Path != null)
            {
                await SaveVideoAsync(username, video);
            }
            
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