using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.Entity
{
    public class Category : EntityBase
    {
        [Required(ErrorMessage = "Lütfen kategori içeriği giriniz!")]
        [StringLength(50, ErrorMessage = "Kategori {0} karakterden uzun olmamalıdır!")]
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<Article> Articles { get; set; }
    }
}
