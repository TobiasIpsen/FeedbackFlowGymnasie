using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers;
using feedbackFlowAPI.Helpers.ControllerHelpers;
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
    public class QuestionsController : ControllerBase
    {
        private readonly feedbackFlowAPI.Interface.IQuestionService _questionService;
        private readonly feedbackFlowAPI.Services.Interfaces.IQuestionService _questionQueryService;

        public QuestionsController(feedbackFlowAPI.Interface.IQuestionService questionService, feedbackFlowAPI.Services.Interfaces.IQuestionService questionQueryService)
        {
            _questionService = questionService;
            _questionQueryService = questionQueryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
        {
            var questions = await _questionService.GetQuestionsAsync();
            return Ok(questions);
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(int id)
        {
            var question = await _questionService.GetQuestionByIdAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            return question;
        }

    

        // POST: api/Questions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Question>> PostQuestion(QuestionDTO questionDto)
        {
            if (questionDto == null)
            {
                return BadRequest(new { message = "Request body is required." });
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var (question, errors) = await _questionService.CreateQuestionAsync(questionDto);
            if (errors.Count > 0)
            {
                return BadRequest(new { errors });
            }

            if (question == null)
            {
                return BadRequest(new { message = "Unable to create question from provided values." });
            }

            return CreatedAtAction("GetQuestion", new { id = question.Id }, question);
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var wasDeleted = await _questionService.DeleteQuestionAsync(id);
            if (!wasDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
        
        [HttpGet]
        public async Task<ActionResult<GetQuestionResponse>> GetQuestions([FromQuery] GetQuestionRequest getQuestionRequest)
        {
            return await _questionQueryService.GetQuestions(getQuestionRequest);
        }
    }
}
