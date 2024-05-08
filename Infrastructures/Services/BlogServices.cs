using Application.Interfaces;
using Domain;
using Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Infrastructures.Services
{
    public class BlogServices : IBlog
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BlogServices(ApplicationDbContext context, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _userManager = userManager;
        }


        public async Task<Blog> AddBlog(Blog blog)
        {
            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
            return blog;
        }

        public async Task DeleteBlog(Guid id)
        {
            var result = await _context.Blogs.FindAsync(id);
            if (result != null)
            {
                _context.Blogs.Remove(result);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Blog>> GetAllBlogs()
        {
            var result = await _context.Blogs.Include(x => x.userFK).ToListAsync(); 
            return result;
        }

        public async Task<Blog> GetBlogById(Guid id)
        {
            return await _context.Blogs.Include(x => x.userFK).FirstOrDefaultAsync(x => x.Id == id);

        }

   

        public async Task<Blog> UpdateBlog(Blog blog)
        {
            Blog prevBlog = await GetBlogById(blog.Id);
            BlogHistory history = new BlogHistory();


            if (prevBlog != null)
            {
                history.Blog = prevBlog.Id;
                history.BlogContentPrevious = prevBlog.BlogContent;
                history.BlogTitlePrevious = prevBlog.BlogTitle;
                history.BlogCreatedDateTime = prevBlog.CreatedDate;
                history.BlogImageNamePrevious = prevBlog.ImageName;
                await _context.BlogsHistories.AddAsync(history);

                if (!string.IsNullOrEmpty(blog.ImageName))
                {
                    prevBlog.ImageName = blog.ImageName;

                }
                prevBlog.BlogTitle = blog.BlogTitle;
                prevBlog.BlogContent = blog.BlogContent;
              

                _context.Blogs.Update(prevBlog);
                await _context.SaveChangesAsync();

            }
            return prevBlog;

        }
    }
}
