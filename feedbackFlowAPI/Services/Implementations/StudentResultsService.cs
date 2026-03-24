using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers;
using feedbackFlowAPI.Mappers;
using feedbackFlowAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace feedbackFlowAPI.Services.Implementations
{
    public class StudentResultsService : IStudentResultsService
    {
        private FbfDbContext _context;

        public StudentResultsService(FbfDbContext context)
        {
            _context = context;
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

            StudentResult entity = StudentResultMapper.ToEntity(
                dto,
                studentId,
                questionSetId,
                questionId
            );

            EntityEntry<StudentResult> result = await _context.StudentResults.AddAsync(entity);
            await _context.SaveChangesAsync();

            return StudentResultMapper.ToDTO(result.Entity);
        }
    }
}
