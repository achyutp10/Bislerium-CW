using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;

        public AdminServices(ApplicationDbContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
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

        //public async Task<int> GetDailyDownvoteCount(DateTime date)
        //{
        //    return await _dbContext.Likes
        //        .Where(l => !l.ReactionType && l.CreatedAt != null && ((DateTime)l.CreatedAt).Date == date.Date)
        //        .CountAsync();
        //}
        //public async Task<int> GetDailyDownvoteCount(DateTime date)
        //{
        //    return await _dbContext.Likes
        //         .Where(c => c.CreatedAt.Date == date.Date && c.ReactionType == false)
        //         .CountAsync();
        //}

        public async Task<int> GetDailyUpvoteCount(DateTime date)
        {
            // Get the start and end of the specified date
            DateTime startDate = date.Date; // Midnight of the specified date
            DateTime endDate = startDate.AddDays(1); // Midnight of the next day

            // Count the likes within the specified date range
            return await _dbContext.Likes
                .Where(c => c.CreatedAt >= startDate && c.CreatedAt < endDate && c.ReactionType)
                .CountAsync();
        }
        public async Task<int> GetDailyDownvoteCount(DateTime date)
        {
            // Get the start and end of the specified date
            DateTime startDate = date.Date; // Midnight of the specified date
            DateTime endDate = startDate.AddDays(1); // Midnight of the next day

            // Count the downvotes within the specified date range
            return await _dbContext.Likes
                .Where(c => c.CreatedAt >= startDate && c.CreatedAt < endDate && !c.ReactionType) // Filter for ReactionType being false (downvote)
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

        public async Task<List<string>> GetTop10PopularBloggersEmails()
        {
            var topBloggersUsernames = await GetTop10PopularBloggers();
            var emails = new List<string>();

            foreach (var username in topBloggersUsernames)
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user != null)
                {
                    emails.Add(user.Email);
                }
            }

            return emails;
        }
    }
}
