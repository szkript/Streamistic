using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace SilkVideo.Models
{
    public class Video
    {
        [Key]
        public long Id { set; get; }
        public string Description { set; get; }
        public string Path { set; get; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime UploadTime { set; get; }

        public virtual ICollection<Comment> Comments { set; get; }
    }
}
