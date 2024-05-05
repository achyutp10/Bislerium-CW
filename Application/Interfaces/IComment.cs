using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IComment
    {
        Task<CommentResponse> AddComment(Comment comments);
        Task<CommentResponse> UpdateComment(Comment comments);
        Task<CommentResponse> DeleteComment(Guid id);
        Task<Comment> GetCommentById(Guid id);
        Task<IEnumerable<Comment>> GetAllComment();
    }
}
