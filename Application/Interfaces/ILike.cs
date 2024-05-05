using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ILike
    {
        Task<LikeResponse> AddUpvote(Like like);
        Task<LikeResponse> AddDownVote( Like like);
        Task<Like> GetUsersLike(Guid id, string u_id);
        Task DeleteVote(Guid id);

    }
}
