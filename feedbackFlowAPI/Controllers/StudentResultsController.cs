using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers;
using feedbackFlowAPI.DTOs;

namespace feedbackFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentResultsController : ControllerBase
    {
        private readonly FbfDbContext _context;

        public StudentResultsController(FbfDbContext context)
        {
            _context = context;
        }

        // GET: api/Studentresults
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentResult>>> GetStudentresults()
        {
            return await _context.StudentResults.ToListAsync();
        }

        // GET: api/Studentresults/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentResult>> GetStudentresult(int id)
        {
            var studentresult = await _context.StudentResults.FindAsync(id);

            if (studentresult == null)
            {
                return NotFound();
            }

            return studentresult;
        }

        // PUT: api/Studentresults/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentresult(int id, StudentResult studentresult)
        {
            if (id != studentresult.Id)
            {
                return BadRequest();
            }

            _context.Entry(studentresult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentresultExists(id))
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

        // PUT: api/Studentresults/5/teacher-feedback
        [HttpPut("{id}/teacher-feedback")]
        public async Task<IActionResult> PutStudentresultTeacherFeedback(int id, StudentResultFeedbackDTO feedback)
        {
            var studentresult = await _context.StudentResults.FindAsync(id);

            if (studentresult == null)
            {
                return NotFound();
            }

            studentresult.TeacherFeedback = feedback.TeacherFeedback;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Studentresults
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentResult>> PostStudentresult(StudentResult studentresult)
        {
            _context.StudentResults.Add(studentresult);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StudentresultExists(studentresult.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStudentresult", new { id = studentresult.Id }, studentresult);
        }

        // DELETE: api/Studentresults/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentresult(int id)
        {
            var studentresult = await _context.StudentResults.FindAsync(id);
            if (studentresult == null)
            {
                return NotFound();
            }

            _context.StudentResults.Remove(studentresult);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentresultExists(int id)
        {
            return _context.StudentResults.Any(e => e.Id == id);
        }
    }
}
