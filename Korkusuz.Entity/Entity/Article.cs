using Blog.Entity.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.Entity
{
    public class Article : EntityBase
    {
        [Required(ErrorMessage = "Lütfen makale başlığı giriniz!")]
        [StringLength(100, ErrorMessage = "Başlık {0} karakterden uzun olmamalıdır!")]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.Html)]
        public string Summary { get; set; }

        [Required]
        [DataType(DataType.Html)]
        [UIHint("tinymce_full_compressed")]
        
        public string Content { get; set; }
        [StringLength(100)]
        public string Picture { get; set; }
        //public string PicturesId { get; set; }  //Pictures tablosu kullanılırsa...
        public int CategoryId { get; set; }
        public string UserId { get; set; }
        public int OkunmaSayisi { get; set; }


        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public virtual List<Tag> Tags { get; set; }
        public virtual List<Comment> Comments { get; set; }

    }
}
