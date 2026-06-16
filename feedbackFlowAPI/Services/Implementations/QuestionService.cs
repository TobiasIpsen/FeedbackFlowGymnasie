using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers;
using feedbackFlowAPI.Helpers.ControllerHelpers;
using feedbackFlowAPI.Mappers.Interface;
using feedbackFlowAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;

namespace feedbackFlowAPI.Services.Implementations
{
    public class QuestionService : IQuestionService
    {
        private FbfDbContext _context;
        private IQuestionMapper _mapper;

        public QuestionService(FbfDbContext context, IQuestionMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            
        }

        public async Task<GetQuestionResponse> GetQuestions(GetQuestionRequest getQuestionRequest)
        {
            List<QuestionDTO> questions = await _context.Questions
                .AsNoTracking()
                .OrderByDescending(q => q.Id)
                .Where(q => q.DeletedAt == null)
                .Where(q => q.Id < getQuestionRequest.LastId)
                .Take(getQuestionRequest.PageSize)
                .Select(q => _mapper.ToDTO(q))
                .ToListAsync();

            GetQuestionResponse GetQuestionResponse = new GetQuestionResponse
            {
                Questions = questions,
                LastId = questions.Any() ? questions.Last().Id : null
            };

            return GetQuestionResponse;
        }

    }
}
