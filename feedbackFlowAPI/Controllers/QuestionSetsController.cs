using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers;
using feedbackFlowAPI.Helpers.ControllerHelpers;
using feedbackFlowAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace feedbackFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionSetsController : ControllerBase
    {

        private readonly IQuestionSetService _service;

        public QuestionSetsController(IQuestionSetService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<QuestionSetDTO>> CreateQuestionSet([FromBody] CreateQuestionSetRequest request)
        {
            try
            {
                QuestionSetDTO createdSet = await _service.CreateQuestionSet(request.QuestionIds, request.SubjectId, request.Set);
                return Created(Request.Path.Value ?? string.Empty, createdSet);
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

        [HttpGet("assignable-classes")]
        public async Task<ActionResult<IEnumerable<ClassListItemDTO>>> GetAssignableClasses()
        {
            var classes = await _service.GetAssignableClassesAsync();
            return Ok(classes);
        }

        [HttpPost("{questionSetId:int}/assign/classes/{classId:int}")]
        public async Task<ActionResult<QuestionSetAssignmentResultDTO>> AssignQuestionSetToClass(int questionSetId, int classId)
        {
            try
            {
                var result = await _service.AssignQuestionSetToClassAsync(questionSetId, classId);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
