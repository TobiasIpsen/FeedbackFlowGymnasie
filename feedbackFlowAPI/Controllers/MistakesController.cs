using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers;

namespace feedbackFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MistakesController : ControllerBase
    {
        private readonly FbfDbContext _context;

        public MistakesController(FbfDbContext context)
        {
            _context = context;
        }

        // GET: api/Mistakes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mistake>>> GetMistakes()
        {
            return await _context.Mistakes.ToListAsync();
        }
    }
}