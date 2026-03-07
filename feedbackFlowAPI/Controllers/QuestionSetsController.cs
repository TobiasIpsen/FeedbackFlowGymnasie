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
    public class QuestionSetsController : ControllerBase
    {
        private readonly FbfDbContext _context;

        public QuestionSetsController(FbfDbContext context)
        {
            _context = context;
        }

        // GET: api/Questionsets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionSet>>> GetQuestionsets()
        {
            return await _context.QuestionSets.ToListAsync();
        }

        // GET: api/Questionsets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionSet>> GetQuestionset(int id)
        {
            var questionset = await _context.QuestionSets.FindAsync(id);

            if (questionset == null)
            {
                return NotFound();
            }

            return questionset;
        }

        // PUT: api/Questionsets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionset(int id, QuestionSet questionset)
        {
            if (id != questionset.Id)
            {
                return BadRequest();
            }

            _context.Entry(questionset).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionsetExists(id))
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

        // POST: api/Questionsets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QuestionSet>> PostQuestionset(QuestionSet questionset)
        {
            _context.QuestionSets.Add(questionset);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (QuestionsetExists(questionset.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetQuestionset", new { id = questionset.Id }, questionset);
        }

        // DELETE: api/Questionsets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionset(int id)
        {
            var questionset = await _context.QuestionSets.FindAsync(id);
            if (questionset == null)
            {
                return NotFound();
            }

            _context.QuestionSets.Remove(questionset);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionsetExists(int id)
        {
            return _context.QuestionSets.Any(e => e.Id == id);
        }
    }
}
