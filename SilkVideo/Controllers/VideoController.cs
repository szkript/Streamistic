﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SilkVideo.Models;
using System.Collections.Generic;
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
        public async Task<bool> UploadVideoData(Video video)
        {
            var user = await _userManager.FindByNameAsync("ezaz");
            if (user.Videos == null)
            {
                user.Videos = new List<Video>();
            }

            user.Videos.Add(video);
            await _userManager.UpdateAsync(user);
            return true;

        }
    }
}