using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers;
using feedbackFlowAPI.Mappers.Interface;
using feedbackFlowAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace feedbackFlowAPI.Services.Implementations
{
    public class QuestionSetService : IQuestionSetService
    {
        private FbfDbContext _context;
        private readonly IQuestionSetMapper _QSMapper;

        public QuestionSetService(FbfDbContext context, IQuestionSetMapper mapper)
        {
            _context = context;
            _QSMapper = mapper;
        }

        public async Task<QuestionSetDTO> CreateQuestionSet(List<int> QuestionIds, int SubjectId, QuestionSetDTO Set)
        {
            QuestionSet entity = _QSMapper.ToEntity(QuestionIds, SubjectId, Set);
            EntityEntry<QuestionSet> result = await _context.QuestionSets.AddAsync(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

            return _QSMapper.ToDTO(result.Entity);
        }
    }
}
