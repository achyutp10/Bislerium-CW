using Application.Interfaces;
using Domain;
using Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Services
{
    public class CommentLikeServices : ILikeComment
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentLikeServices(ApplicationDbContext context, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _userManager = userManager;
        }


        public async Task<LikeComment> GetCommentsUserLike(Guid id, string u_id)
        {
            var res = await _context.LikeComments.FirstOrDefaultAsync(x => x.Comment == id && x.User == u_id);
            return res;

        }

        public async Task<LikeCommentResponse> AddDownVote(LikeComment likecmt)
        {
            if (likecmt == null || likecmt.ReactionType)
                return new LikeCommentResponse(false, "Invalid operation: like is null or it's an upvote");

            var commentService = new CommentServices(_context);
            var existingLike = await GetCommentsUserLike(likecmt.Comment, likecmt.User);
            var comments = await commentService.GetCommentById(likecmt.Comment);

            if (existingLike != null)
            {
                if (!existingLike.ReactionType)
                {
                    _context.LikeComments.Remove(existingLike);
                    comments.DislikeCount--;
                    
                    _context.Comments.Update(comments);
                    await _context.SaveChangesAsync();
                    return new LikeCommentResponse(true, "Downvote removed successfully", likecmt);
                }
                else
                {
                    return new LikeCommentResponse(false, "Cannot downvote an upvoted post");
                }
            }
            else
            {
                _context.LikeComments.Add(likecmt);
                comments.DislikeCount++;
           
                _context.Comments.Update(comments);
                await _context.SaveChangesAsync();
                return new LikeCommentResponse(true, "Downvote added successfully", likecmt) ;
            }


        }
        public async Task<LikeCommentResponse> AddUpvote(LikeComment likecmt)
        {
            if (likecmt == null || !likecmt.ReactionType)
                return new LikeCommentResponse(false, "Invalid operation: like is null or it's an downvote");

            var commentService = new CommentServices(_context);
            var existingLike = await GetCommentsUserLike(likecmt.Comment, likecmt.User);
            var comments = await commentService.GetCommentById(likecmt.Comment);

            if (existingLike != null)
            {
                if (existingLike.ReactionType)
                {
                    _context.LikeComments.Remove(existingLike);
                    comments.LikeCount--;

                    _context.Comments.Update(comments);
                    await _context.SaveChangesAsync();
                    return new LikeCommentResponse(true, "Upvote removed successfully", likecmt);
                }
                else
                {
                    return new LikeCommentResponse(false, "Cannot downvote an upvoted post");
                }
            }
            else
            {
                _context.LikeComments.Add(likecmt);
                comments.LikeCount++;

                _context.Comments.Update(comments);
                await _context.SaveChangesAsync();
                return new LikeCommentResponse(true, "Upvote added successfully", likecmt);
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
