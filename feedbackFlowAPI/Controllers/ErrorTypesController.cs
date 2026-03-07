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
    public class ErrorTypesController : ControllerBase
    {
        private readonly FbfDbContext _context;

        public ErrorTypesController(FbfDbContext context)
        {
            _context = context;
        }

        // GET: api/Errortypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ErrorType>>> GetErrortypes()
        {
            return await _context.Errortypes.ToListAsync();
        }

        // GET: api/Errortypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ErrorType>> GetErrortype(int id)
        {
            var errortype = await _context.Errortypes.FindAsync(id);

            if (errortype == null)
            {
                return NotFound();
            }

            return errortype;
        }

        // PUT: api/Errortypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutErrortype(int id, ErrorType errortype)
        {
            if (id != errortype.Id)
            {
                return BadRequest();
            }

            _context.Entry(errortype).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ErrortypeExists(id))
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

        // POST: api/Errortypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ErrorType>> PostErrortype(ErrorType errortype)
        {
            _context.Errortypes.Add(errortype);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ErrortypeExists(errortype.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetErrortype", new { id = errortype.Id }, errortype);
        }

        // DELETE: api/Errortypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteErrortype(int id)
        {
            var errortype = await _context.Errortypes.FindAsync(id);
            if (errortype == null)
            {
                return NotFound();
            }

            _context.Errortypes.Remove(errortype);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ErrortypeExists(int id)
        {
            return _context.Errortypes.Any(e => e.Id == id);
        }
    }
}
