using IlkinBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IlkinBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ColorController : ControllerBase
    {

        private readonly DataContext _context;

        public ColorController(DataContext context)
        {
            this._context = context;
        }


        [HttpGet]

        public async Task<ActionResult<List<Color>>> Get()
        {

            return Ok(await _context.colors.ToListAsync());

        }


        [HttpGet("getSingleColor")]
        public async Task<ActionResult<List<Color>>> GetSingle(int id)
        {

            var color = await _context.colors.FindAsync(id);
            if (color == null)
                return BadRequest("Category not found");

            return Ok(color);


        }


        [HttpPost]

        public async Task<ActionResult<List<Color>>> PostColor(Color color)
        {

            _context.colors.Add(color);

            await _context.SaveChangesAsync();

            return Ok(await _context.colors.ToListAsync());


        }

        [HttpPut]

        public async Task<ActionResult<List<Color>>>  UpdateColor(Color color)
        {

            var DBcolor = await _context.colors.FindAsync(color.Id);
            if (DBcolor == null)
                return BadRequest("Category not found");

            DBcolor.elementColor = color.elementColor;
            DBcolor.elementName = color.elementName;

            await _context.SaveChangesAsync();

            return Ok(DBcolor);

        }

        [HttpDelete("{Id}")]

        public async Task<ActionResult<List<Color>>> DeleteColor(int id)
        {

            var color = await _context.colors.FindAsync(id);
            if (color == null)
                return BadRequest("Category not found");

            _context.colors.Remove(color);

            await _context.SaveChangesAsync();

            return Ok(await _context.colors.ToListAsync());


        }



    }
}
