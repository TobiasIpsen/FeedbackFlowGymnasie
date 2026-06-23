using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.DTOs.ClassStatistics;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers;
using feedbackFlowAPI.Mappers.Interface;
using feedbackFlowAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Npgsql;
using feedbackFlowAPI.DTOs.StudentLookupDTO;

namespace feedbackFlowAPI.Services.Implementations
{
    public class ClassService : IClassService
    {
        private FbfDbContext _context;
        private readonly IClassMapper _mapper;

        public ClassService(FbfDbContext context, IClassMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClassDTO> CreateClass(ClassDTO dto)
        {
            Class entity = _mapper.ToEntity(dto);
            EntityEntry<Class> result = await _context.Classes.AddAsync(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == PostgresErrorCodes.UniqueViolation)
            {
                throw new InvalidOperationException($"Constraint {pgEx.ConstraintName} violated");
            }

            return _mapper.ToDTO(result.Entity);
        }

        public async Task<List<ClassStudentDTO>> GetStudentsForClass(int classId)
        {
            var classEntity = await _context.Classes
                .AsNoTracking()
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == classId && c.DeletedAt == null);

            if (classEntity == null)
            {
                return new List<ClassStudentDTO>();
            }

            return classEntity.Students
                .Where(s => s.DeletedAt == null)
                .OrderBy(s => s.Firstname)
                .ThenBy(s => s.Lastname)
                .Select(s => new ClassStudentDTO
                {
                    Id = s.Id,
                    Firstname = s.Firstname,
                    Lastname = s.Lastname,
                    Email = s.Email
                })
                .ToList();
        }

        public async Task<List<StudentLookupDTO>> SearchStudents(string? query, int? classId)
        {
            var studentQuery = _context.Users
                .AsNoTracking()
                .Where(u => u.DeletedAt == null)
                .Where(u => u.UserRoles.Any(r => r.Name == "Student"));

            if (!string.IsNullOrWhiteSpace(query))
            {
                var term = query.Trim();
                studentQuery = studentQuery.Where(u =>
                    EF.Functions.ILike(u.Firstname, $"%{term}%") ||
                    EF.Functions.ILike(u.Lastname, $"%{term}%") ||
                    EF.Functions.ILike(u.Email, $"%{term}%"));
            }

            var students = await studentQuery
                .OrderBy(u => u.Firstname)
                .ThenBy(u => u.Lastname)
                .Take(100)
                .Select(u => new StudentLookupDTO
                {
                    Id = u.Id,
                    FullName = u.Firstname + " " + u.Lastname,
                    Email = u.Email,
                    IsInClass = false
                })
                .ToListAsync();

            if (!classId.HasValue)
            {
                return students;
            }

            var classEntity = await _context.Classes
                .AsNoTracking()
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == classId.Value && c.DeletedAt == null);

            if (classEntity == null)
            {
                return students;
            }

            var studentIdsInClass = classEntity.Students
                .Where(s => s.DeletedAt == null)
                .Select(s => s.Id)
                .ToHashSet();

            foreach (var student in students)
            {
                student.IsInClass = studentIdsInClass.Contains(student.Id);
            }

            return students;
        }

        public async Task<bool> AddStudentToClass(int classId, int studentId)
        {
            var classEntity = await _context.Classes
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == classId && c.DeletedAt == null);

            if (classEntity == null)
            {
                return false;
            }

            var student = await _context.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Id == studentId && u.DeletedAt == null);

            if (student == null)
            {
                return false;
            }

            var isStudentRole = student.UserRoles.Any(r => r.Name == "Student");
            if (!isStudentRole)
            {
                return false;
            }

            if (classEntity.Students.Any(s => s.Id == studentId))
            {
                return true;
            }

            classEntity.Students.Add(student);
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<ClassStatisticsDTO> GetStatistics(int ClassId, int QuestionSetId)
        {

            var rawData = await _context.StudentResults
                .AsNoTracking()
                .Where(sr => sr.Student.StudentClasses.Any(sc => sc.Id == ClassId))
                .Where(sr => sr.QuestionSetId == QuestionSetId)
                .Select(sr => new
                {
                    Student = sr.Student,
                    TeacherPoint = sr.TeacherPoint,
                    ExamType = sr.Question.ExamType,
                    Subject = sr.QuestionSet.Questions
                                    .First(q => q.QuestionId == sr.QuestionId).Subject,
                    Question = sr.QuestionSet.Questions
                                    .First(q => q.QuestionId == sr.QuestionId)
                })
                .ToListAsync();

            var digital = rawData.Where(x => x.ExamType == ExamType.Digital).ToList();
            var analog = rawData.Where(x => x.ExamType == ExamType.Analog).ToList();

            List<StudentsScore> StudentsScoreAssignments = rawData
                .Select(sr => new StudentsScore(
                    User: new UserDTO(sr.Student),
                    Question: new QuestionQuestionSetDTO(sr.Question),
                    Score: sr.TeacherPoint  ?? 0
                    ))
                .ToList();

            #region ClassAvgScore
            double ClassAvgScoreCombined = Math.Round(rawData
                .Average(s => s.TeacherPoint)
                .GetValueOrDefault(0.0), 2);

            double ClassAvgScoreDigital = Math.Round(digital
                .Average(sr => sr.TeacherPoint)
                .GetValueOrDefault(0.0), 2);

            double ClassAvgScoreAnalog = Math.Round(analog
                .Average(sr => sr.TeacherPoint)
                .GetValueOrDefault(0.0), 2);
            #endregion


            #region ClassAvgSubjectScore
            var ClassAvgSubjectScoreCombined = rawData
                .GroupBy(x => x.Subject.Id)
                .Select(group => new SubjectScoreCombined(
                    Subject: new SubjectDTO(group.First().Subject.Id, group.First().Subject.Name),
                    Score: group.Average(x => x.TeacherPoint) ?? 0.0
                ))
                .ToList();

            var ClassAvgSubjectScoreDigital = digital
                .GroupBy(x => x.Subject.Id)
                .Select(group => new SubjectScore(
                    Subject: new SubjectDTO(group.First().Subject.Id, group.First().Subject.Name),
                    ExamType: group.First().ExamType,
                    Score: group.Average(x => x.TeacherPoint) ?? 0.0
                ))
                .ToList();

            var ClassAvgSubjectScoreAnalog = analog
                .GroupBy(x => x.Subject.Id)
                .Select(group => new SubjectScore(
                    Subject: new SubjectDTO(group.First().Subject.Id, group.First().Subject.Name),
                    ExamType: group.First().ExamType,
                    Score: group.Average(x => x.TeacherPoint) ?? 0.0
                ))
                .ToList();

            #endregion


            #region StudentsAvgScore
            var StudentsAvgScoreCombined = rawData
                .GroupBy(x => x.Student)
                .Select(group => new StudentScore(
                    User: new UserDTO(group.Key),
                    Score: group.Average(x => x.TeacherPoint) ?? 0.0
                ))
                .ToList();

            var StudentsAvgScoreDigital = digital
                .GroupBy(x => x.Student)
                .Select(group => new StudentScore(
                    User: new UserDTO(group.Key),
                    Score: group.Average(x => x.TeacherPoint) ?? 0.0
                ))
                .ToList();

            var StudentsAvgScoreAnalog = analog
                .GroupBy(x => x.Student)
                .Select(group => new StudentScore(
                    User: new UserDTO(group.Key),
                    Score: group.Average(x => x.TeacherPoint) ?? 0.0
                ))
                .ToList();
            #endregion


            #region StudentAvgSccoreSubject
            var StudentsAvgScoreSubject = rawData
                    .GroupBy(x => new { x.Subject.Id, x.Student })
                    .Select(group => new StudentsAvgScoreSubject(
                        User: new UserDTO(group.Key.Student),
                        Subject: new SubjectDTO(group.First().Subject.Id, group.First().Subject.Name),
                        Score: group.Average(x => x.TeacherPoint) ?? 0.0
                    ))
                    .ToList();
            #endregion


            #region WeakestSubjects
            var WeakestSubjects = rawData
                .GroupBy(x => x.Subject)
                .Select(group => new WeakSubject(
                    Subject: new SubjectDTO(group.First().Subject.Id, group.First().Subject.Name),
                    Score: group.Average(x => x.TeacherPoint) ?? 0.0
                ))
                .OrderByDescending(x => x.Score)
                .ToList();
                
            #endregion

            return new ClassStatisticsDTO
            {
                StudentsScoreAssignments = StudentsScoreAssignments,
                ClassAvgScoreCombined = ClassAvgScoreCombined,
                ClassAvgScoreDigital = ClassAvgScoreDigital,
                ClassAvgScoreAnalog = ClassAvgScoreAnalog,
                ClassAvgSubjectScoreCombined = ClassAvgSubjectScoreCombined,
                ClassAvgSubjectScoreDigital = ClassAvgSubjectScoreDigital,
                ClassAvgSubjectScoreAnalog = ClassAvgSubjectScoreAnalog,
                StudentsAvgScoreCombined = StudentsAvgScoreCombined,
                StudentsAvgScoreDigital = StudentsAvgScoreDigital,
                StudentsAvgScoreAnalog = StudentsAvgScoreAnalog,
                StudentsAvgScoreSubject = StudentsAvgScoreSubject,
                WeakestSubjects = WeakestSubjects,
            };
        }
    }
}
