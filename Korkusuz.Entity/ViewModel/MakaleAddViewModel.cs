using Blog.Entity.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Blog.Entity.ViewModel
{
    public class MakaleAddViewModel
    {
        public string Title { get; set; }
        [Required]
        [DataType(DataType.Html)]
        public string Summary { get; set; }

        [Required]
        [DataType(DataType.Html)]
        public string Content { get; set; }
        [StringLength(100)]
        public string Picture { get; set; }
        public int CategoryId { get; set; }
        public string UserId { get; set; }
        //public HttpPostedFileBase PictureUpload { get; set; }
    }
}
