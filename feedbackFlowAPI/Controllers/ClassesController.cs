using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers;
using feedbackFlowAPI.Services.Interfaces;
using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.DTOs.StudentLookupDTO;
using feedbackFlowAPI.DTOs.ClassStudentMutationDTO;


namespace feedbackFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {

        private readonly IClassService _service;

        public ClassesController(IClassService service)
        {
            _service = service;
        }


        [HttpPost]
        public async Task<ActionResult<ClassDTO>> PostClass([FromBody] ClassDTO dto)
        {
            try
            {
                ClassDTO createdDto = await _service.CreateClass(dto);
                return Created(Request.Path.Value ?? string.Empty, createdDto);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("{classId}/students")]
        public async Task<ActionResult<IEnumerable<ClassStudentDTO>>> GetStudentsForClass(int classId)
        {
            var students = await _service.GetStudentsForClass(classId);
            return Ok(students);
        }

        [HttpGet("{classId}/students/search")]
        public async Task<ActionResult<IEnumerable<StudentLookupDTO>>> SearchStudents(int classId, [FromQuery] string? query)
        {
            var students = await _service.SearchStudents(query, classId);
            return Ok(students);
        }

        [HttpPost("{classId}/students")]
        public async Task<IActionResult> AddStudentToClass(int classId, [FromBody] ClassStudentMutationDTO request)
        {
            if (request.StudentId <= 0)
            {
                return BadRequest("StudentId must be greater than 0.");
            }

            var success = await _service.AddStudentToClass(classId, request.StudentId);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
