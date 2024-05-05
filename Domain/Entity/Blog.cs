using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Blog
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? BlogTitle { get; set; }

        [Required]
        public string? BlogContent { get; set; }

        public string? ImageName { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;


        [ForeignKey(nameof(userFK))]
    
        public string? User { get; set; }

        public virtual AppUser? userFK { get; set; }

        public int LikeCount { get; set; } = 0;
        public int DislikeCount { get; set; } = 0;
        public int CommentCount { get; set; } = 0;


        public int Popularity { get; set; } = 0;
    }
}
