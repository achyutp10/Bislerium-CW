using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAdmin
    {
        Task<int> GetAllTimeBlogPostCount();
        Task<int> GetAllTimeUpvoteCount();
        Task<int> GetAllTimeDownvoteCount();
        Task<int> GetAllTimeCommentCount();

        Task<int> GetDailyBlogPostCount(DateTime date);
        Task<int> GetDailyUpvoteCount(DateTime date);
        Task<int> GetDailyDownvoteCount(DateTime date);
        Task<int> GetDailyCommentCount(DateTime date);

        Task<List<string>> GetTop10PopularPosts();
        Task<List<string>> GetTop10PopularBloggers();
    }
}
