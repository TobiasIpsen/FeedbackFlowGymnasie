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
        private readonly IQuestionAnswerService _qaService;

        public QuestionAnswersController(IQuestionAnswerService qaService)
        {
            _qaService = qaService;
        }


        [HttpPost]
        public async Task<IActionResult> SubmitAnswer([FromForm] QuestionAnswerDTO dto)
        {
            try
            {
                
                var result = await _qaService.SubmitAnswer(dto);

                
                return Ok(result);
            }
            catch (Exception ex)
            {
                
                return BadRequest(new { message = ex.Message });
            }

        }

    }
}
