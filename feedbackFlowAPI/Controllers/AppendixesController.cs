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
    public class AppendixesController : ControllerBase
    {
        private readonly FbfDbContext _context;

        public AppendixesController(FbfDbContext context)
        {
            _context = context;
        }

        // GET: api/Appendixes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appendix>>> GetAppendices()
        {
            return await _context.Appendices.ToListAsync();
        }

        // GET: api/Appendixes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appendix>> GetAppendix(int id)
        {
            var appendix = await _context.Appendices.FindAsync(id);

            if (appendix == null)
            {
                return NotFound();
            }

            return appendix;
        }

        // PUT: api/Appendixes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppendix(int id, Appendix appendix)
        {
            if (id != appendix.Id)
            {
                return BadRequest();
            }

            _context.Entry(appendix).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppendixExists(id))
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

        // POST: api/Appendixes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Appendix>> PostAppendix(Appendix appendix)
        {
            _context.Appendices.Add(appendix);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AppendixExists(appendix.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAppendix", new { id = appendix.Id }, appendix);
        }

        // DELETE: api/Appendixes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppendix(int id)
        {
            var appendix = await _context.Appendices.FindAsync(id);
            if (appendix == null)
            {
                return NotFound();
            }

            _context.Appendices.Remove(appendix);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppendixExists(int id)
        {
            return _context.Appendices.Any(e => e.Id == id);
        }
    }
}
