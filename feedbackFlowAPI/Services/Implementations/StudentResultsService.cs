using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.DTOs.StudentResults;
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

        public async Task<List<MistakeTypeDTO>> GetMistakeTypesAsync()
        {
            return await _context.Mistakes
                .OrderBy(m => m.Id)
                .Select(m => new MistakeTypeDTO
                {
                    Id = m.Id,
                    Name = m.Name
                })
                .ToListAsync();
        }

        public async Task<List<MistakeTypeDTO>?> GetAssignedMistakesAsync(int studentId, int questionSetId, int questionId, int teacherId)
        {
            var studentResult = await _context.StudentResults
                .Include(r => r.Mistakes)
                .FirstOrDefaultAsync(r =>
                    r.StudentId == studentId &&
                    r.QuestionSetId == questionSetId &&
                    r.QuestionId == questionId &&
                    r.TeacherId == teacherId);

            if (studentResult == null)
            {
                return null;
            }

            return studentResult.Mistakes
                .OrderBy(m => m.Id)
                .Select(m => new MistakeTypeDTO
                {
                    Id = m.Id,
                    Name = m.Name
                })
                .ToList();
        }

        public async Task<(bool Success, string? Error)> AssignMistakesAsync(int studentId, int questionSetId, int questionId, AssignMistakesDTO dto)
        {
            var mistakeIds = dto.MistakeIds?
                .Where(id => id > 0)
                .Distinct()
                .ToList() ?? new List<int>();

            var studentResult = await _context.StudentResults
                .Include(r => r.Mistakes)
                .FirstOrDefaultAsync(r =>
                    r.StudentId == studentId &&
                    r.QuestionSetId == questionSetId &&
                    r.QuestionId == questionId &&
                    r.TeacherId == dto.TeacherId);

            if (studentResult == null)
            {
                return (false, "Student result not found. Create teacher feedback first.");
            }

            var mistakes = await _context.Mistakes
                .Where(m => mistakeIds.Contains(m.Id))
                .ToListAsync();

            if (mistakes.Count != mistakeIds.Count)
            {
                return (false, "One or more mistake types are invalid.");
            }

            studentResult.Mistakes.Clear();
            foreach (var mistake in mistakes)
            {
                studentResult.Mistakes.Add(mistake);
            }

            await _context.SaveChangesAsync();

            return (true, null);
        }

        public async Task<StudentResultDTO> CreateStudentSelfAssessmentAsync(int studentId, int questionSetId, int questionId, StudentSelfAssessmentDTO selfAssessmentPoint)
        {
            StudentResult? res = await _context.StudentResults
                    .FirstOrDefaultAsync(s =>
                        s.StudentId == studentId &&
                        s.QuestionSetId == questionSetId &&
                        s.QuestionId == questionId);


            if (res == null)
            {
                QuestionSet? questionSet = await _context.QuestionSets.FindAsync(questionSetId);
                if (questionSet == null) throw new Exception("Questionset not found");
                res = new StudentResult
                {
                    StudentId = studentId,
                    QuestionSetId = questionSetId,
                    QuestionId = questionId,
                    TeacherId = questionSet.TeacherId
                };
                await _context.StudentResults.AddAsync(res);
            }


            res.StudentSelfAssessmentPoints = selfAssessmentPoint.points;

            await _context.SaveChangesAsync();

            return _mapper.StudentResultToDTO(res);
        }
    }
}
