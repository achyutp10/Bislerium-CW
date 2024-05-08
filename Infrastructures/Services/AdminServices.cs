using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Services
{
    public class AdminServices : IAdmin
    {
        private readonly ApplicationDbContext _dbContext;
        public AdminServices(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> GetAllTimeBlogPostCount()
        {
            return await _dbContext.Blogs.CountAsync();
        }

        public async Task<int> GetAllTimeCommentCount()
        {
            return await _dbContext.Comments.CountAsync();
        }

        public async Task<int> GetAllTimeDownvoteCount()
        {
            return await _dbContext.Likes.Where(l => !l.ReactionType).CountAsync();
        }

        public async Task<int> GetAllTimeUpvoteCount()
        {
            return await _dbContext.Likes.Where(l => l.ReactionType).CountAsync();
        }

        public async Task<int> GetDailyBlogPostCount(DateTime date)
        {
            return await _dbContext.Blogs.CountAsync(b => b.CreatedDate != null && b.CreatedDate.Value.Date == date.Date);
        }

        public async Task<int> GetDailyCommentCount(DateTime date)
        {
            return await _dbContext.Comments.CountAsync(c => c.PostedAt.Date == date.Date);
        }

        public async Task<int> GetDailyDownvoteCount(DateTime date)
        {
            return await _dbContext.Likes
                .Where(l => !l.ReactionType && l.CreatedAt != null && ((DateTime)l.CreatedAt).Date == date.Date)
                .CountAsync();
        }

        public async Task<int> GetDailyUpvoteCount(DateTime date)
        {
            return await _dbContext.Likes
                .Where(l => l.ReactionType && l.CreatedAt != null && ((DateTime)l.CreatedAt).Date == date.Date)
                .CountAsync();
        }

#nullable disable
        public async Task<List<string>> GetTop10PopularPosts()
        {
            return await _dbContext.Blogs.OrderByDescending(b => b.Popularity)
                                         .Select(b => b.BlogTitle)
                                         .Take(10)
                                         .ToListAsync();
        }

        public async Task<List<string>> GetTop10PopularBloggers()
        {
            return await _dbContext.Blogs.GroupBy(b => b.User)
                                         .OrderByDescending(g => g.Count())
                                         .Select(g => g.Key)
                                         .Take(10)
                                         .ToListAsync();
        }
    }
}
