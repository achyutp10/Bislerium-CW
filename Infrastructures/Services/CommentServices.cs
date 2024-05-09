using Application.Interfaces;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Infrastructures.Services
{
    public class CommentServices : IComment
    {
        private readonly ApplicationDbContext _context;
        public CommentServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CommentResponse> AddComment(Comment comments)
        {
            await _context.Comments.AddAsync(comments);
            await _context.SaveChangesAsync();
            var blog = await _context.Blogs.FindAsync(comments.BlogId);
            if (blog != null)
            {
                blog.CommentCount++;
                blog.Popularity = (2 * blog.LikeCount) + (-1 * blog.DislikeCount) + (1 * blog.CommentCount);
                _context.Blogs.Update(blog);
                await _context.SaveChangesAsync();
            }
            else
            {
                return new CommentResponse(false, "Not Found");
            }
            return new CommentResponse(true, "Added Successfully", comments);
        }
        public async Task<CommentResponse> DeleteComment(Guid id)
        {
            var result = await _context.Comments.FindAsync(id);
            if (result != null)
            {
                var blog = await _context.Blogs.FindAsync(result.BlogId);
                if (blog != null)
                {
                    blog.CommentCount--;
                    blog.Popularity = (2 * blog.LikeCount) + (-1 * blog.DislikeCount) + (1 * blog.CommentCount);
                    _context.Blogs.Update(blog);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return new CommentResponse(false, "Not Found");
                }
                _context.Comments.Remove(result);
                await _context.SaveChangesAsync();
                return new CommentResponse(true, " Comment Deleated");
            }
            else
            {
                return new CommentResponse(false, " Comment not Deleated");
            }
        }
        public async Task<IEnumerable<Comment>> GetAllComment()
        {
            var result = await _context.Comments.Include(x => x.User).ToListAsync();
            return result;
        }
        public async Task<Comment> GetCommentById(Guid id)
        {
            var result= await _context.Comments.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
        public async Task<CommentResponse> UpdateComment(Comment comments)
        {
            Comment prevComment = await GetCommentById(comments.Id);
            CommentHistory history = new CommentHistory();
            if (prevComment != null)
            {
                history.Comments = prevComment.Id;
                history.CommentContentPrevious = prevComment.Content;
                history.CommentCreatedDateTime = prevComment.PostedAt;
                await _context.CommentHistories.AddAsync(history);
                prevComment.Content = comments.Content;
                _context.Comments.Update(prevComment);
                await _context.SaveChangesAsync();
            }
            return new CommentResponse(true, "Updated Sucessfully", prevComment);
        }
  
    }
}

