using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ILikeComment
    {
        Task<LikeCommentResponse> AddUpvote(LikeComment likecmt);
        Task<LikeCommentResponse> AddDownVote(LikeComment likecmt);
        Task<LikeComment> GetCommentsUserLike(Guid id,string u_id);
        Task DeleteVote(Guid id);
    }
}
