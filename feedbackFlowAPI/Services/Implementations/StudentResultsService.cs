using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers;
using feedbackFlowAPI.Mappers.Interface;
using feedbackFlowAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace feedbackFlowAPI.Services.Implementations
{
    public class StudentResultsService : IStudentResultsService
    {
        private FbfDbContext _context;
        private readonly IStudentResultMapper _mapper;

        public StudentResultsService(FbfDbContext context, IStudentResultMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TeacherFeedbackDTO> SaveTeacherFeedbackAsync(int studentId, int questionSetId, int questionId, TeacherFeedbackDTO dto)
        {

            bool alreadyExists = await _context.StudentResults.AnyAsync(r =>
                r.StudentId == studentId &&
                r.QuestionSetId == questionSetId &&
                r.QuestionId == questionId &&
                r.TeacherId == dto.TeacherId);

            if (alreadyExists)
            {
                throw new InvalidOperationException("Teacher feedback already exists");
            }

            StudentResult entity = _mapper.ToEntity(
                dto,
                studentId,
                questionSetId,
                questionId
            );

            EntityEntry<StudentResult> result = await _context.StudentResults.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _mapper.ToDTO(result.Entity);
        }
    }
}
