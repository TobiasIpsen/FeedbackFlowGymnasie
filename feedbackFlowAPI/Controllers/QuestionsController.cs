using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers;
using feedbackFlowAPI.Helpers.ControllerHelpers;
using feedbackFlowAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace feedbackFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public async Task<ActionResult<GetQuestionResponse>> GetQuestions([FromQuery] GetQuestionRequest getQuestionRequest)
        {
            return await _questionService.GetQuestions(getQuestionRequest);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(int id)
        {
            var question = await _questionService.GetQuestionByIdAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        [HttpPost]
        public async Task<ActionResult<Question>> PostQuestion([FromBody] QuestionDTO questionDto)
        {
            if (questionDto == null)
            {
                return BadRequest(new { message = "Request body is required." });
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var question = await _questionService.CreateQuestionAsync(questionDto);

            return CreatedAtAction(nameof(GetQuestion), new { id = question.Id }, question);
        }

    }
}
