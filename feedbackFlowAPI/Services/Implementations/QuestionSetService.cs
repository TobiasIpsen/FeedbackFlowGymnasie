using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers;
using feedbackFlowAPI.Helpers.ControllerHelpers;
using feedbackFlowAPI.Mappers.Interface;
using feedbackFlowAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace feedbackFlowAPI.Services.Implementations
{
    public class QuestionSetService : IQuestionSetService
    {
        private FbfDbContext _context;
        private readonly IQuestionSetMapper _mapper;

        public QuestionSetService(FbfDbContext context, IQuestionSetMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<QuestionSetDTO> CreateQuestionSet(List<int> QuestionIds, int SubjectId, QuestionSetDTO Set)
        {
            QuestionSet entity = _mapper.ToEntity(QuestionIds, SubjectId, Set);
            EntityEntry<QuestionSet> result = await _context.QuestionSets.AddAsync(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

            return _mapper.ToDTO(result.Entity);
        }

        public async Task<List<ClassListItemDTO>> GetAssignableClassesAsync()
        {
            var classes = await _context.Classes
                .AsNoTracking()
                .Where(c => c.DeletedAt == null)
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    Year = c.Year.Year,
                    StudentCount = c.Students.Count(s => s.DeletedAt == null)
                })
                .OrderBy(c => c.Name)
                .ThenByDescending(c => c.Year)
                .ToListAsync();

            return classes
                .Select(c => new ClassListItemDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Year = c.Year,
                    StudentCount = c.StudentCount
                })
                .ToList();
        }

        public async Task<QuestionSetAssignmentResultDTO> AssignQuestionSetToClassAsync(int questionSetId, int classId)
        {
            var classEntity = await _context.Classes
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == classId && c.DeletedAt == null);

            if (classEntity == null)
            {
                throw new InvalidOperationException("Class not found.");
            }

            var questionSet = await _context.QuestionSets
                .FirstOrDefaultAsync(qs => qs.Id == questionSetId && qs.DeletedAt == null);

            if (questionSet == null)
            {
                throw new InvalidOperationException("Question set not found.");
            }

            var questionIds = await _context.QuestionQuestionSets
                .Where(qqs => qqs.QuestionSetId == questionSetId)
                .Select(qqs => qqs.QuestionId)
                .Distinct()
                .ToListAsync();

            if (questionIds.Count == 0)
            {
                throw new InvalidOperationException("Question set has no questions.");
            }

            var students = classEntity.Students
                .Where(s => s.DeletedAt == null)
                .ToList();

            if (students.Count == 0)
            {
                return new QuestionSetAssignmentResultDTO
                {
                    QuestionSetId = questionSetId,
                    ClassId = classId,
                    AssignedStudentCount = 0,
                    AssignedQuestionCount = questionIds.Count,
                    Notifications = new List<StudentNotificationDTO>()
                };
            }

            var studentIds = students.Select(s => s.Id).ToList();
            var existingAssignments = await _context.StudentResults
                .Where(sr =>
                    sr.QuestionSetId == questionSetId &&
                    studentIds.Contains(sr.StudentId) &&
                    questionIds.Contains(sr.QuestionId))
                .Select(sr => new { sr.StudentId, sr.QuestionId })
                .ToListAsync();

            var existingKeys = existingAssignments
                .Select(a => $"{a.StudentId}:{a.QuestionId}")
                .ToHashSet();

            var now = DateTimeOffset.UtcNow;

            foreach (var student in students)
            {
                foreach (var questionId in questionIds)
                {
                    var key = $"{student.Id}:{questionId}";
                    if (existingKeys.Contains(key))
                    {
                        continue;
                    }

                    _context.StudentResults.Add(new StudentResult
                    {
                        TeacherId = classEntity.TeacherId,
                        StudentId = student.Id,
                        QuestionSetId = questionSetId,
                        QuestionId = questionId,
                        CreatedAt = now
                    });
                }
            }

            questionSet.IsDraft = false;
            await _context.SaveChangesAsync();

            var notifications = students
                .Select(s => new StudentNotificationDTO
                {
                    StudentId = s.Id,
                    StudentEmail = s.Email,
                    Message = $"You have been assigned question set '{questionSet.Name ?? questionSet.Id.ToString()}'."
                })
                .ToList();

            return new QuestionSetAssignmentResultDTO
            {
                QuestionSetId = questionSetId,
                ClassId = classId,
                AssignedStudentCount = students.Count,
                AssignedQuestionCount = questionIds.Count,
                Notifications = notifications
            };
        }
    }
}
