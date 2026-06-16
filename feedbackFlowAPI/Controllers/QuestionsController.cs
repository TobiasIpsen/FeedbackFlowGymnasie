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
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _service;
        private readonly IStorageService _storageService;

        public QuestionsController(IQuestionService service, IStorageService storageService)
        {
      
            _storageService = storageService;
        }


        [HttpPost("upload")]
        [HttpGet]
        public async Task<ActionResult<GetQuestionResponse>> GetQuestions([FromQuery] GetQuestionRequest getQuestionRequest)
        {
            return await _service.GetQuestions(getQuestionRequest);
        }
    }
}
