using IlkinBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace IlkinBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogController : ControllerBase
    {

        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;

        public BlogController(DataContext context, IWebHostEnvironment env)
        {
            this._env = env;
            this._context = context;
        }


        //GET BLOGS

        [HttpGet, AllowAnonymous]
        public async Task<ActionResult<List<Blog>>> Get()
        {

            return Ok(await _context.blogs.ToListAsync());

        }

        //GET SINGLE BLOG
        [HttpGet("{id}"), AllowAnonymous]
        public async Task<ActionResult<Blog>> Get(int id)
        {
            var blog = await _context.blogs.FindAsync(id);
            if (blog == null)
            {
                return BadRequest("Blog not Found");
            }
            return Ok(blog);
        }


        //POST BLOG

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<List<Blog>>> AddBlog([FromForm] Blog blog)
        {


            if (blog.File.Length > 0)
            {

                string imageName = DateTime.Now.Millisecond.ToString() + blog.File.FileName;
                string imageFilePath = Path.Combine(_env.ContentRootPath, "Images", imageName);
                using (var imageFileStream = System.IO.File.Create(imageFilePath))
                {
                    await blog.File.CopyToAsync(imageFileStream); // save file
                    blog.ImagePath = imageFilePath;

                }

            }



            _context.blogs.Add(blog);

            await _context.SaveChangesAsync();

            return Ok(await _context.blogs.ToListAsync());
        }


        [HttpPut]
        public async Task<ActionResult<List<Blog>>> UpdateBlog(Blog updatedBlog)
        {

            var DBblog = await _context.blogs.FindAsync(updatedBlog.Id);
            if (DBblog == null)
                return BadRequest("Blog not Found");

            DBblog.Title = updatedBlog.Title;
            DBblog.TitleEn = updatedBlog.TitleEn;
            DBblog.Content = updatedBlog.Content;
            DBblog.ContentEn = updatedBlog.ContentEn;
            DBblog.Description = updatedBlog.Description;
            DBblog.DescriptionEn = updatedBlog.DescriptionEn;
            DBblog.Author = updatedBlog.Author;
            DBblog.CategoryId = updatedBlog.CategoryId;
            DBblog.ImagePath = updatedBlog.ImagePath;



            await _context.SaveChangesAsync();


            return Ok(await _context.blogs.ToListAsync());
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Blog>>> DeleteBlog(int id)
        {
            var DBblog = await _context.blogs.FindAsync(id);
            if (DBblog == null)
                return BadRequest("Blog not Found");


            _context.blogs.Remove(DBblog);

            await _context.SaveChangesAsync();

            return Ok(await _context.blogs.ToListAsync());
        }



    }
}
