using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.Entity
{
    public class Tag : EntityBase
    {
        [Required(ErrorMessage = "Lütfen etiket içeriği giriniz!")]
        [StringLength(30, ErrorMessage = "İçerik {0} karakterden uzun olmamalıdır!")]
        public string Content { get; set; }

        public virtual List<Article> Articles { get; set; }
    }
}
