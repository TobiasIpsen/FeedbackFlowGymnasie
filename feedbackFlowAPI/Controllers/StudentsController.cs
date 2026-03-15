using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                return await _service.SaveTeacherFeedbackAsync(studentId, questionSetId, questionId, dto);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
