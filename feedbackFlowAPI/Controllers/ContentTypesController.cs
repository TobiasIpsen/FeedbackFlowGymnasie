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
    public class ContentTypesController : ControllerBase
    {
        private readonly FbfDbContext _context;

        public ContentTypesController(FbfDbContext context)
        {
            _context = context;
        }

        // GET: api/Contenttypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContentType>>> GetContenttypes()
        {
            return await _context.ContentTypes.ToListAsync();
        }

        // GET: api/Contenttypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContentType>> GetContenttype(int id)
        {
            var contenttype = await _context.ContentTypes.FindAsync(id);

            if (contenttype == null)
            {
                return NotFound();
            }

            return contenttype;
        }

        // PUT: api/Contenttypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContenttype(int id, ContentType contenttype)
        {
            if (id != contenttype.Id)
            {
                return BadRequest();
            }

            _context.Entry(contenttype).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContenttypeExists(id))
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

        // POST: api/Contenttypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContentType>> PostContenttype(ContentType contenttype)
        {
            _context.ContentTypes.Add(contenttype);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ContenttypeExists(contenttype.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetContenttype", new { id = contenttype.Id }, contenttype);
        }

        // DELETE: api/Contenttypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContenttype(int id)
        {
            var contenttype = await _context.ContentTypes.FindAsync(id);
            if (contenttype == null)
            {
                return NotFound();
            }

            _context.ContentTypes.Remove(contenttype);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContenttypeExists(int id)
        {
            return _context.ContentTypes.Any(e => e.Id == id);
        }
    }
}
