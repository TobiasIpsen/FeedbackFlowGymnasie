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

        public async Task<ClassStatisticsDTO> GetStatistics(int ClassId, int QuestionSetId)
        {
            try
            {
                //Class res = await _context.Classes
                //    .Where(c => c.Id == ClassId)
                //    .Include(thisClass => thisClass.Students)
                //    .ThenInclude(user => user.StudentResults.Where(res => res.QuestionSetId == QuestionSetId)
                //    //.ThenInclude(studentResults => studentResults.QuestionSet)
                //    .FirstOrDefaultAsync();


                var res = await _context.Classes
                    .Where(c => c.Id == ClassId)
                    .SelectMany(c => c.Students)
                    .Include(s => s.StudentResults)
                    //.ThenInclude(sr => sr.QuestionSet.Questions)
                    //.ThenInclude(q => q.Subject)
                    //.SelectMany(s => s.StudentResults.Where(sr => sr.QuestionSetId == QuestionSetId))
                    //.SelectMany(sr => sr.QuestionSet.Questions
                    //    .Select(q => new
                    //    {
                    //        SubjectId = q.Subject.Id,
                    //        SubjectName = q.Subject.Name,
                    //        TeacherPoint = sr.TeacherPoint
                    //    }))
                    //.GroupBy(x => new { x.SubjectId, x.SubjectName })
                    //.Select(g => new SubjectScoreCombined
                    //(
                    //    Subject: new SubjectDTO { Id = g.Key.SubjectId, Name = g.Key.SubjectName },
                    //    Score: g.Average(x => x.TeacherPoint) ?? 0.0
                    //))
                    .ToListAsync();

                var str = "";


                ClassStatisticsDTO? ress = await _context.Classes
                    .Where(c => c.Id == ClassId)
                    .Select(thisClass => new
                    {
                        Class = thisClass,
                        FilteredStudents = thisClass.Students.Select(student => new
                        {
                            Student = student,
                            FilteredResults = student.StudentResults
                                .Where(sr => sr.QuestionSetId == QuestionSetId)
                                .ToList()
                        })
                    })
                    .Select(thisClass => new ClassStatisticsDTO
                    {
                        ClassAvgScoreCombined = thisClass.FilteredStudents
                                .SelectMany(student => student.FilteredResults.Select(studentResults => studentResults.TeacherPoint)
                                ).Average() ?? 0.0,
                        ClassAvgScoreAnalog = thisClass.FilteredStudents
                                .SelectMany(students => students.FilteredResults
                                    .Where(studentResults => studentResults.Question.ExamType == ExamType.Analog)
                                    .Select(studentResults => studentResults.TeacherPoint)
                                ).Average() ?? 0.0,
                        ClassAvgScoreDigital = thisClass.FilteredStudents
                                .SelectMany(students => students.FilteredResults
                                    .Where(studentResults => studentResults.Question.ExamType == ExamType.Digital)
                                    .Select(studentResults => studentResults.TeacherPoint)
                                ).Average() ?? 0.0,


                        ClassAvgSubjectScoreCombined = thisClass.FilteredStudents
                                .SelectMany(student => student.FilteredResults
                                    .SelectMany(studentResult => studentResult.QuestionSet.Questions
                                        .Select(question => new
                                        {
                                            SubjectId = question.Subject.Id,
                                            SubjectName = question.Subject.Name,
                                            studentResult.TeacherPoint,
                                        })))
                                .GroupBy(x => new { x.SubjectId, x.SubjectName })
                                .Select(g => new SubjectScoreCombined
                                (
                                    Subject: new SubjectDTO { Id = g.Key.SubjectId, Name = g.Key.SubjectName },
                                    g.Average(x => x.TeacherPoint) ?? 0.0
                                ))
                                .ToList(),

                        //ClassAvgSubjectScoreAnalog = thisClass.FilteredStudents
                        //    .SelectMany(student => student.FilteredResults
                        //        .Select(studentResult => new
                        //        {
                        //            SubjectId = studentResult.QuestionSet.Questions.FirstOrDefault().Subject.Id,
                        //            SubjectName = studentResult.QuestionSet.Questions.FirstOrDefault().Subject.Name,
                        //            studentResult.Question.ExamType,
                        //            studentResult.TeacherPoint,
                        //        }))
                        //    .Where(sr => sr.ExamType == ExamType.Analog)
                        //    .GroupBy(x => new { x.SubjectId, x.SubjectName, x.ExamType })
                        //    .Select(g => new SubjectScore
                        //    (
                        //        Subject: new SubjectDTO { Id = g.Key.SubjectId, Name = g.Key.SubjectName },
                        //        g.Key.ExamType,
                        //        g.Average(x => x.TeacherPoint) ?? 0.0
                        //    ))
                        //    .ToList(),

                        //ClassAvgSubjectScoreDigital = thisClass.FilteredStudents
                        //    .SelectMany(student => student.FilteredResults
                        //        .Select(studentResult => new
                        //        {
                        //            Subject = studentResult.QuestionSet.Questions.Select(q => q.Subject).FirstOrDefault(),
                        //            studentResult.Question.ExamType,
                        //            studentResult.TeacherPoint,
                        //        }))
                        //    .Where(sr => sr.ExamType == ExamType.Analog)
                        //    .GroupBy(x => new { x.Subject, x.ExamType })
                        //    .Select(g => new SubjectScore
                        //    (
                        //        Subject: new SubjectDTO { Id = g.Key.Subject.Id, Name = g.Key.Subject.Name },
                        //        g.Key.ExamType,
                        //        g.Average(x => x.TeacherPoint) ?? 0.0
                        //    ))
                        //    .ToList()





                    })
                    .FirstOrDefaultAsync();

                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
