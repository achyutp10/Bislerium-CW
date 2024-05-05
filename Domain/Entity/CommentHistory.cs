using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class CommentHistory
    {
        [Key]
        public Guid Id { get; set; }

      
        [Required]
        public string? CommentContentPrevious { get; set; }
        public DateTime? CommentCreatedDateTime { get; set; }
        public DateTime? CommwntModifiedDateTime { get; set; } = DateTime.Now;

        [ForeignKey(nameof(commentFk))]

        public Guid Comments { get; set; }


        public virtual Comment commentFk { get; set; }
    }
}
