using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers;
using feedbackFlowAPI.Services.Interfaces;
using feedbackFlowAPI.DTOs;

namespace feedbackFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {

        private readonly IClassService _service;

        public ClassesController(IClassService service)
        {
            _service = service;
        }


        [HttpPost]
        public async Task<ActionResult<ClassDTO>> PostClass(ClassDTO dto)
        {
            try
            {
                return await _service.CreateClass(dto);
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
    }
}
