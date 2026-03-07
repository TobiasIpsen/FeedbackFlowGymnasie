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
    public class QuestionCollectionsController : ControllerBase
    {
        private readonly FbfDbContext _context;

        public QuestionCollectionsController(FbfDbContext context)
        {
            _context = context;
        }

        // GET: api/Questioncollections
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionCollection>>> GetQuestioncollections()
        {
            return await _context.QuestionCollections.ToListAsync();
        }

        // GET: api/Questioncollections/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionCollection>> GetQuestioncollection(int id)
        {
            var questioncollection = await _context.QuestionCollections.FindAsync(id);

            if (questioncollection == null)
            {
                return NotFound();
            }

            return questioncollection;
        }

        // PUT: api/Questioncollections/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestioncollection(int id, QuestionCollection questioncollection)
        {
            if (id != questioncollection.Id)
            {
                return BadRequest();
            }

            _context.Entry(questioncollection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestioncollectionExists(id))
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

        // POST: api/Questioncollections
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QuestionCollection>> PostQuestioncollection(QuestionCollection questioncollection)
        {
            _context.QuestionCollections.Add(questioncollection);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (QuestioncollectionExists(questioncollection.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetQuestioncollection", new { id = questioncollection.Id }, questioncollection);
        }

        // DELETE: api/Questioncollections/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestioncollection(int id)
        {
            var questioncollection = await _context.QuestionCollections.FindAsync(id);
            if (questioncollection == null)
            {
                return NotFound();
            }

            _context.QuestionCollections.Remove(questioncollection);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestioncollectionExists(int id)
        {
            return _context.QuestionCollections.Any(e => e.Id == id);
        }
    }
}
