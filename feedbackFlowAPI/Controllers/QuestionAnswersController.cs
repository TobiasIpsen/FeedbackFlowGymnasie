using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers;

namespace feedbackFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionAnswersController : ControllerBase
    {
        private readonly FbfDbContext _context;

        public QuestionAnswersController(FbfDbContext context)
        {
            _context = context;
        }

        // GET: api/QuestionAnswers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionAnswer>>> GetQuestionanswers()
        {
            return await _context.QuestionAnswers.ToListAsync();
        }

        // GET: api/QuestionAnswers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionAnswer>> GetQuestionanswer(int id)
        {
            var questionanswer = await _context.QuestionAnswers.FindAsync(id);

            if (questionanswer == null)
            {
                return NotFound();
            }

            return questionanswer;
        }

        // PUT: api/QuestionAnswers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionanswer(int id, QuestionAnswer questionanswer)
        {
            if (id != questionanswer.Id)
            {
                return BadRequest();
            }

            _context.Entry(questionanswer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionanswerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/QuestionAnswers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QuestionAnswer>> PostQuestionanswer(QuestionAnswer questionanswer)
        {
            _context.QuestionAnswers.Add(questionanswer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (QuestionanswerExists(questionanswer.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetQuestionanswer", new { id = questionanswer.Id }, questionanswer);
        }

        // DELETE: api/QuestionAnswers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionanswer(int id)
        {
            var questionanswer = await _context.QuestionAnswers.FindAsync(id);
            if (questionanswer == null)
            {
                return NotFound();
            }

            _context.QuestionAnswers.Remove(questionanswer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionanswerExists(int id)
        {
            return _context.QuestionAnswers.Any(e => e.Id == id);
        }
    }
}
