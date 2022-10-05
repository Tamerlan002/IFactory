using IlkinBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IlkinBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {


        private readonly DataContext _context;

        public CategoryController(DataContext context)
        {
            this._context = context;
        }


        //GET CATEGORIES

        [HttpGet, AllowAnonymous]
        public async Task<ActionResult<List<Category>>> Get()
        {

            return Ok(await _context.categories.ToListAsync());

        }


        //GET SINGLE CATEGORY
        [HttpGet("{id}"), AllowAnonymous]
        public async Task<ActionResult<List<Category>>> Get(int id)
        {

            var category= await _context.categories.FindAsync(id);
            if (category == null)
                return BadRequest("Category not found");

            return Ok(category);


        }


        [HttpGet("getBlogByCategoryId"), AllowAnonymous]

        public async Task<ActionResult<List<Blog>>> GetBlogsByCategoryId(int id)
        {

            List<Blog> blogsByCatId = await _context.blogs.Where(b => b.CategoryId == id).ToListAsync();

            return Ok(blogsByCatId);

        }




        [HttpPost]
        public async Task<ActionResult> PostCategory(Category category)
        {

            _context.categories.Add(category);

            await _context.SaveChangesAsync();

            return Ok(await _context.categories.ToListAsync());

        }


        [HttpPut]

        public async Task<ActionResult<List<Category>>> UpdateCategory(Category updatedCategory)
        {

            var DBcategory = await _context.categories.FindAsync(updatedCategory.Id);
            if (DBcategory == null)
                return BadRequest("Category not found");

            DBcategory.Title = updatedCategory.Title;
            DBcategory.TitleEn = updatedCategory.TitleEn;

            await _context.SaveChangesAsync();

            return Ok(DBcategory);

        }


        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await _context.categories.FindAsync(id);
            if (category == null)
                return BadRequest("Category not found");

            _context.categories.Remove(category);

            await _context.SaveChangesAsync();
    
            return Ok(await _context.categories.ToListAsync());


        }


    }
}
