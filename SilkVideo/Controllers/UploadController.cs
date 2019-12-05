using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SilkVideo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]IFormFile body)
        {
            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await body.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }

            var filename = body.FileName;
            var contentType = body.ContentType;

            //SaveFileToDatabase(id, fileBytes, filename, contentType);

            return Ok();
        }
    }
}