using feedbackFlowAPI.DTOs.ClassStatistics;
using feedbackFlowAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace feedbackFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IClassService _service;

        public StatisticsController(IClassService service)
        {
            _service = service;
        }


        [HttpGet("classes/{classId}/questionsets/{questionSetId}")]
        public async Task<ActionResult<ClassStatisticsDTO>> ClassStatistics(int classId, int questionSetId)
        {
            var res = await _service.GetStatistics(classId, questionSetId);
            return res;
        }

    }
}
