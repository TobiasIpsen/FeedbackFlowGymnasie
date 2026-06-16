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
        private readonly IQuestionSetMapper _QSMapper;

        public QuestionService(FbfDbContext context, IQuestionMapper mapper, IQuestionSetMapper qsMapper)
        {
            _context = context;
            _mapper = mapper;
            _QSMapper = qsMapper;
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

        public async Task<byte[]> CreateQuestionSetAndGeneratePdf(List<int> QuestionIds, int SubjectId, QuestionSetDTO Set)
        {
            // 1. Gem forholdet i databasen (SQL)
            QuestionSet entity = _QSMapper.ToEntity(QuestionIds, SubjectId, Set);
            await _context.QuestionSets.AddAsync(entity);
            await _context.SaveChangesAsync();

            // 2. Hent billed-URL'erne fra de valgte spørgsmål
            var imageUrls = await _context.Questions
                .Where(q => QuestionIds.Contains(q.Id))
                .Select(q => q.ImgSrc)
                .ToListAsync();

            // 3. Brug din PdfService (som implementerer IDocument)
            var pdfDoc = new PdfService
            {
                Title = Set.Name,
                ImageUrls = imageUrls
            };

            // 4. Generer og returner bytes
            return pdfDoc.GeneratePdf();
        }

    }
}
