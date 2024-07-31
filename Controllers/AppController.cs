using AppRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Task.Controllers
{
    // Controllers/UserController.cs
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly BloggingContext _context;

        public UserController(BloggingContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPost("follow")]
        public async Task<IActionResult> Follow(Follow follow)
        {
            _context.Follows.Add(follow);
            await _context.SaveChangesAsync();
            return Ok(follow);
        }
    }

    // Controllers/BlogPostController.cs
    [ApiController]
    [Route("api/[controller]")]
    public class BlogPostController : ControllerBase
    {
        private readonly BloggingContext _context;

        public BlogPostController(BloggingContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogPost blogPost)
        {
            _context.BlogPosts.Add(blogPost);
            await _context.SaveChangesAsync();
            return Ok(blogPost);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string title, [FromQuery] string author)
        {
            var query = _context.BlogPosts.AsQueryable();
            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(bp => bp.Title.Contains(title));
            }
            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(bp => bp.Author.Username.Contains(author));
            }
            var blogPosts = await query.OrderByDescending(bp => bp.CreationDate).ToListAsync();
            return Ok(blogPosts);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BlogPost blogPost)
        {
            var existingPost = await _context.BlogPosts.FindAsync(id);
            if (existingPost == null)
            {
                return NotFound();
            }
            existingPost.Title = blogPost.Title;
            existingPost.Content = blogPost.Content;
            await _context.SaveChangesAsync();
            return Ok(existingPost);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            _context.BlogPosts.Remove(blogPost);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }

    // Controllers/CommentController.cs
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly BloggingContext _context;

        public CommentController(BloggingContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return Ok(comment);
        }
    }

}
