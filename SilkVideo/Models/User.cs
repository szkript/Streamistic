using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SilkVideo.Models
{
    public class User : IdentityUser
    {
        [NotMapped]
        public string Password { set; get; }

        public virtual ICollection<Video> Videos { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
