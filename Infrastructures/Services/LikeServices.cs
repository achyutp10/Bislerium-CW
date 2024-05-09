using Application.Interfaces;
using Azure;
using Domain;
using Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Services
{
    public class LikeServices : ILike
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LikeServices(ApplicationDbContext context, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _userManager = userManager;
        }

        public async Task<Like> GetUsersLike(Guid id, string u_id)
        {
            var res = await _context.Likes.FirstOrDefaultAsync(x => x.Blog == id && x.User == u_id);
            return res;

        }
        public async Task<LikeResponse> AddUpvote(Like like)
        {
            if (like == null || !like.ReactionType)
                return new LikeResponse(false, "Invalid operation");
            var blogService = new BlogServices(_context, _userManager, _httpContextAccessor);
            var existingLike = await GetUsersLike(like.Blog, like.User);
            var blog = await blogService.GetBlogById(like.Blog);

            if (existingLike != null)
            {
                if (existingLike.ReactionType)
                {
                    _context.Likes.Remove(existingLike);
                    blog.LikeCount--;
                    blog.Popularity = (2 * blog.LikeCount) + (-1 * blog.DislikeCount) + (1 * blog.CommentCount);
                    _context.Blogs.Update(blog);
                    await _context.SaveChangesAsync();
                    return new LikeResponse(true, "Upvote removed");
                }
                else
                {
                    return new LikeResponse(false, "Cannot upvote");
                }
            }
            else
            {
                _context.Likes.Add(like);
                blog.LikeCount++;
                blog.Popularity = (2 * blog.LikeCount) + (-1 * blog.DislikeCount) + (1 * blog.CommentCount);
                _context.Blogs.Update(blog);
                await _context.SaveChangesAsync();
                return new LikeResponse(true, "Upvote added successfully", like);
            }
        }

        public async Task<LikeResponse> AddDownVote(Like like)
        {
            if (like == null || like.ReactionType)
                return new LikeResponse(false, "Invalid operation");
            var blogService = new BlogServices(_context,_userManager,_httpContextAccessor);
            var existingLike = await GetUsersLike(like.Blog, like.User);
            var blog = await blogService.GetBlogById(like.Blog);
            if (existingLike != null)
            {
                if (!existingLike.ReactionType)
                {
                    _context.Likes.Remove(existingLike);
                    blog.DislikeCount--;
                    blog.Popularity = (2 * blog.LikeCount) + (-1 * blog.DislikeCount) + (1 * blog.CommentCount);
                    _context.Blogs.Update(blog);
                    await _context.SaveChangesAsync();
                    return new LikeResponse(true, "Downvote removed", like);
                }
                else
                {
                    return new LikeResponse(false, "Cannot downvote");
                }
            }
            else
            {
                _context.Likes.Add(like);
                blog.DislikeCount++;
                blog.Popularity = (2 * blog.LikeCount) + (-1 * blog.DislikeCount) + (1 * blog.CommentCount);
                _context.Blogs.Update(blog);
                await _context.SaveChangesAsync();
                return new LikeResponse(true, "Downvote added successfully", like);
            }
        }
        
        public async Task DeleteVote(Guid id)
        {
            var like = await _context.Likes.FirstOrDefaultAsync(x => x.Id == id);
            if (like != null)
            {
                _context.Likes.Remove(like);
                await _context.SaveChangesAsync();
            }
        }


    }

   
}
