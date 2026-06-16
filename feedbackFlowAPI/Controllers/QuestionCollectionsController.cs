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

        [HttpPost("save-and-download")]
        public async Task<IActionResult> SaveAndDownloadSet([FromBody] CreateQuestionSetRequest request)
        {
            try
            {

                byte[] pdfFile = await _service.CreateQuestionSetAndGeneratePdf(
                    request.QuestionIds,
                    request.SubjectId,
                    request.Set
                );


                return File(pdfFile, "application/pdf", $"{request.Set.Name}.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}