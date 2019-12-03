using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SilkVideo.Models
{
    public class Comment
    {
        [Key]
        public long Id { set; get; }
        public string Message { set; get; }
    }
}
