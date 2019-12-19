﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SilkVideo.Models;
using System.Diagnostics;
using System.IO;
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
        public async Task<string> StartLiveStream()
        {
            string username = this.User.Identity.Name;
            if (username != null)
            {
                StreamRecording(username);
            }
            return username;
            //var user = await _userManager.FindByNameAsync("ezaz");
            //Video liveStream = stream;
            //user.Videos.Add(liveStream);
            //_context.Update(user);
            //var updatedStream = await _context.Videos.FindAsync(stream);
            //return Redirect("localhost:/stream/"+ updatedStream.Id);
        }

        private async void StreamRecording(string username)
        {
            Process rtmpdump = new Process();
            rtmpdump.StartInfo.FileName = "cmd.exe";
            rtmpdump.StartInfo.Arguments = "/C rtmpdump -q --rtmp rtmp://64.225.24.130:1935/show --playpath " + username + " -o "+username+".flv --live";
            rtmpdump.StartInfo.UseShellExecute = true;
            rtmpdump.StartInfo.RedirectStandardOutput = false;
            await Task.Run(() => {
                while (true)
                {
                    rtmpdump.Start();
                    rtmpdump.WaitForExit();
                    FileInfo file = new FileInfo(username + ".flv");
                    if (file.Length > 0)
                    { break; }
                }
            });
        }

    }
}