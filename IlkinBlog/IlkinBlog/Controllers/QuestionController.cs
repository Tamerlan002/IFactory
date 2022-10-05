using IlkinBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IlkinBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionController : ControllerBase
    {
        private readonly DataContext _context;

        public QuestionController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]

        public async Task<ActionResult<List<Question>>> Get() 
        { 
            
            return Ok(await _context.questions.ToListAsync());
            
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Question>>> Get(int id)
        {

            var question = await _context.questions.FindAsync(id);

            if(question == null)
            {
                return BadRequest("No question found");
            }

            return Ok(question);

        }



        [HttpPost("postQuestion"), AllowAnonymous]

        public async Task<ActionResult<Question>> PostQuestion(Question question) 
        {

            _context.questions.Add(question);

            await _context.SaveChangesAsync();

            return Ok(question);


        }


        [HttpDelete("{Id}")]

        public async Task<ActionResult<List<Question>>> DeleteQuestion(int id)
        {

            var question = await _context.questions.FindAsync(id);
            if (question == null)
                return BadRequest("Question not found to delete");

            _context.questions.Remove(question);

            await _context.SaveChangesAsync();

            return Ok(await _context.questions.ToListAsync());

        }


    }
}
