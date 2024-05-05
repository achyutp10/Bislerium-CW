using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class LikeCommentResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public LikeComment Likecmt { get; set; }

        public LikeCommentResponse(bool success, string message, LikeComment like = null)
        {
            Success = success;
            Message = message;
            Likecmt = like;
        }

    }
}
