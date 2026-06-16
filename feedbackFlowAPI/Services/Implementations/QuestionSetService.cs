using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers;
using feedbackFlowAPI.Helpers.ControllerHelpers;
using feedbackFlowAPI.Mappers.Interface;
using feedbackFlowAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using QuestPDF.Fluent;

namespace feedbackFlowAPI.Services.Implementations
{
    public class QuestionSetService : IQuestionSetService
    {
        private FbfDbContext _context;
        private readonly IQuestionSetMapper _QSMapper;
        //test
        public QuestionSetService(FbfDbContext context, IQuestionSetMapper qsMapper)
        {
            _context = context;
            _QSMapper = qsMapper;
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
        // lavet denne så det er muligt at generere et pdf dokument med spørgsmålene i et question set
        public async Task<byte[]> CreateQuestionSetAndGeneratePdf(List<int> QuestionIds, int SubjectId, QuestionSetDTO Set)
        {
            
            QuestionSet entity = _QSMapper.ToEntity(QuestionIds, SubjectId, Set);
            await _context.QuestionSets.AddAsync(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }


            var imageUrls = await _context.Questions
                .Where(q => QuestionIds.Contains(q.Id))
                .Select(q => q.ImgSrc)
                .ToListAsync();

         
            var pdfDoc = new PdfService
            {
                Title = Set.Name,
                ImageUrls = imageUrls
            };

            
            return pdfDoc.GeneratePdf();
        }

    }
}
