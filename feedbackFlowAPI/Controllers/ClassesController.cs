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
using feedbackFlowAPI.Services.Interfaces;

namespace feedbackFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly FbfDbContext _context;
        private readonly IClassStudentsService _classStudentsService;

        public ClassesController(FbfDbContext context, IClassStudentsService classStudentsService)
        {
            _context = context;
            _classStudentsService = classStudentsService;
        }

        // GET: api/Classes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> GetClasses()
        {
            return await _context.Classes.ToListAsync();
        }

        // GET: api/Classes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> GetClass(int id)
        {
            var schoolclass = await _context.Classes.FindAsync(id);

            if (schoolclass == null)
            {
                return NotFound();
            }

            return schoolclass;
        }

        // PUT: api/Classes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClass(int id, Class schoolclass)
        {
            if (id != schoolclass.Id)
            {
                return BadRequest();
            }

            _context.Entry(schoolclass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(id))
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

        // POST: api/Classes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Class>> PostClass(Class schoolclass)
        {
            _context.Classes.Add(schoolclass);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClassExists(schoolclass.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetClass", new { id = schoolclass.Id }, schoolclass);
        }

        // DELETE: api/Classes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var schoolclass = await _context.Classes.FindAsync(id);
            if (schoolclass == null)
            {
                return NotFound();
            }

            _context.Classes.Remove(schoolclass);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Classes/{classId}/students
        [HttpGet("{classId}/students")]
        public async Task<ActionResult<IEnumerable<ClassStudentDTO>>> GetClassStudents(int classId)
        {
            var students = await _classStudentsService.GetClassStudentsAsync(classId);
            if (students == null)
            {
                return NotFound(new { message = "Class not found." });
            }

            return Ok(students);
        }

        // GET: api/Classes/{classId}/students/search?query=...
        [HttpGet("{classId}/students/search")]
        public async Task<ActionResult<IEnumerable<StudentSearchDTO>>> SearchStudents(int classId, [FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest(new { message = "Query is required." });
            }

            var students = await _classStudentsService.SearchStudentsAsync(classId, query);
            if (students == null)
            {
                return NotFound(new { message = "Class not found." });
            }

            return Ok(students);
        }

        // POST: api/Classes/{classId}/students
        [HttpPost("{classId}/students")]
        public async Task<IActionResult> AddStudentToClass(int classId, [FromBody] AddStudentToClassDTO dto)
        {
            if (dto == null)
            {
                return BadRequest(new { message = "Request body is required." });
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var result = await _classStudentsService.AddStudentToClassAsync(classId, dto.StudentId!.Value);
            if (!result.Success)
            {
                if (result.Error == "Class not found." || result.Error == "Student not found.")
                {
                    return NotFound(new { message = result.Error });
                }

                return BadRequest(new { message = result.Error });
            }

            return NoContent();
        }

        // DELETE: api/Classes/{classId}/students/{studentId}
        [HttpDelete("{classId}/students/{studentId}")]
        public async Task<IActionResult> RemoveStudentFromClass(int classId, int studentId)
        {
            if (studentId <= 0)
            {
                return BadRequest(new { message = "studentId must be greater than 0." });
            }

            var result = await _classStudentsService.RemoveStudentFromClassAsync(classId, studentId);
            if (!result.Success)
            {
                if (result.Error == "Class not found.")
                {
                    return NotFound(new { message = result.Error });
                }

                return BadRequest(new { message = result.Error });
            }

            return NoContent();
        }

        private bool ClassExists(int id)
        {
            return _context.Classes.Any(e => e.Id == id);
        }
    }
}
