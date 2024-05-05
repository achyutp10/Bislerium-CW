using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class BlogHistory

    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string? BlogTitlePrevious { get; set; }
        [Required]
        public string? BlogContentPrevious { get; set; }
        public string? BlogImageNamePrevious{ get; set; }


        public DateTime? BlogCreatedDateTime{ get;set; }
        public DateTime? BlogModifiedDateTime { get;set; } = DateTime.Now;

        [ForeignKey(nameof(blogFK))]

        public Guid Blog { get; set; }

  
        public virtual Blog? blogFK { get; set; }
    }
}
