using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers;
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
       
        private readonly IStorageService _storageService;

        public QuestionsController(FbfDbContext context, IStorageService storageService)
        {
          _storageService = storageService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromForm] QuestionDTO dto)
        {
            string fileUrl = null;

           
            if (dto.File != null)
            {
                
                fileUrl = await _storageService.UploadFileAsync(dto.File, "questions");
            }

            
            var newQuestion = new Question
            {
                ImgSrc = fileUrl, 
                Points = dto.Points,

            };

            return CreatedAtAction(nameof(newQuestion), new { id = newQuestion.Id }, newQuestion);
        }
    }
}
