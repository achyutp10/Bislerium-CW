using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class CommentResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Comment Comment { get; set; }

        public CommentResponse(bool success, string message, Comment comment = null)
        {
            Success = success;
            Message = message;
            Comment = comment;
        }
    }
}
