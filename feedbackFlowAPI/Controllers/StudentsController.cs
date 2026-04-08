using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using feedbackFlowAPI.DTOs.StudentResults;

namespace feedbackFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentResultsService _service;

        public StudentsController(IStudentResultsService service)
        {
            _service = service;
        }

        [HttpPost("{studentId:int}/questionSets/{questionSetId:int}/questions/{questionId:int}/feedback")]
        public async Task<ActionResult<TeacherFeedbackDTO>> PostStudentResults(
            int studentId,
            int questionSetId,
            int questionId,
            [FromBody] TeacherFeedbackDTO dto)
        {
            if (dto == null)
            {
                return BadRequest(new { message = "Request body is required." });
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            try
            {
                return await _service.SaveTeacherFeedbackAsync(studentId, questionSetId, questionId, dto);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("mistakes/types")]
        public async Task<ActionResult<IEnumerable<MistakeTypeDTO>>> GetMistakeTypes()
        {
            var mistakes = await _service.GetMistakeTypesAsync();
            return Ok(mistakes);
        }

        [HttpGet("{studentId:int}/questionSets/{questionSetId:int}/questions/{questionId:int}/feedback/{teacherId:int}/mistakes")]
        public async Task<ActionResult<IEnumerable<MistakeTypeDTO>>> GetAssignedMistakes(
            int studentId,
            int questionSetId,
            int questionId,
            int teacherId)
        {
            if (teacherId <= 0)
            {
                return BadRequest(new { message = "teacherId must be greater than 0." });
            }

            var mistakes = await _service.GetAssignedMistakesAsync(studentId, questionSetId, questionId, teacherId);
            if (mistakes == null)
            {
                return NotFound(new { message = "Student result not found." });
            }

            return Ok(mistakes);
        }

        [HttpPut("{studentId:int}/questionSets/{questionSetId:int}/questions/{questionId:int}/mistakes")]
        public async Task<IActionResult> AssignMistakes(
            int studentId,
            int questionSetId,
            int questionId,
            [FromBody] AssignMistakesDTO dto)
        {
            if (dto == null)
            {
                return BadRequest(new { message = "Request body is required." });
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            if (dto.TeacherId == null || dto.TeacherId <= 0)
            {
                return BadRequest(new { message = "TeacherId is required." });
            }

            if (dto.MistakeIds == null)
            {
                return BadRequest(new { message = "MistakeIds is required." });
            }

            var result = await _service.AssignMistakesAsync(studentId, questionSetId, questionId, dto);
            if (!result.Success)
            {
                if (result.Error == "Student result not found. Create teacher feedback first.")
                {
                    return NotFound(new { message = result.Error });
                }

                return BadRequest(new { message = result.Error });
            }

            return NoContent();
        }
    }
}
