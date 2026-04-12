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
    public class QuestionAnswersController : ControllerBase
    {
        private readonly FbfDbContext _context;
        private readonly IStorageService _storageService;

        public QuestionAnswersController(FbfDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }


        [HttpPost]
        public async Task<IActionResult> SubmitAnswer([FromForm] QuestionAnswerDTO dto)
        {
            string fileUrl = null;

            if (dto.File != null)
            {
               
                fileUrl = await _storageService.UploadFileAsync(dto.File, "question-answers");

            }

            var newAnswer = new QuestionAnswer
            {
                Name = dto.Name,
                Url = fileUrl,                               
            };

            _context.QuestionAnswers.Add(newAnswer);
            await _context.SaveChangesAsync();

            return Ok(newAnswer);
        }







        // GET: api/QuestionAnswers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionAnswer>>> GetQuestionanswers()
        {
            return await _context.QuestionAnswers.ToListAsync();
        }

        // GET: api/QuestionAnswers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionAnswer>> GetQuestionanswer(int id)
        {
            var questionanswer = await _context.QuestionAnswers.FindAsync(id);

            if (questionanswer == null)
            {
                return NotFound();
            }

            return questionanswer;
        }

        // PUT: api/QuestionAnswers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionanswer(int id, QuestionAnswer questionanswer)
        {
            if (id != questionanswer.Id)
            {
                return BadRequest();
            }

            _context.Entry(questionanswer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionanswerExists(id))
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

        // DELETE: api/QuestionAnswers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionanswer(int id)
        {
            var questionanswer = await _context.QuestionAnswers.FindAsync(id);
            if (questionanswer == null)
            {
                return NotFound();
            }

            _context.QuestionAnswers.Remove(questionanswer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionanswerExists(int id)
        {
            return _context.QuestionAnswers.Any(e => e.Id == id);
        }
    }
}
