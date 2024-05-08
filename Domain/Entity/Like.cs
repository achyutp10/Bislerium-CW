using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Like  {

        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(userFK))]

        public string User { get; set; }

        public bool ReactionType {  get; set; }

        public virtual AppUser? userFK { get; set; }

        [ForeignKey(nameof(blogFK))]

        public Guid Blog { get; set; }

        public virtual Blog? blogFK { get; set; }

        public DateTime CreatedAt { get; set; }


    }
}
