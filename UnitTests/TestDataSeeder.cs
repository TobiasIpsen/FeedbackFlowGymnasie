using Bogus;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace UnitTests
{
    public static class TestDataSeeder
    {
        public static async Task SeedAsync(FbfDbContext context)
        {
            // ----- Lookup data already inserted via HasData() in your configurations -----
            // Re-creating these would throw a primary key violation, so we just fetch them.
            var roles = await context.UserRoles.ToListAsync();
            var adminRole = roles.Single(r => r.Name == "Admin");
            var teacherRole = roles.Single(r => r.Name == "Teacher");
            var studentRole = roles.Single(r => r.Name == "Student");

            var mathCourse = await context.Courses.SingleAsync(c => c.Name == "Math");
            var subjects = await context.Subjects.ToListAsync();
            var diffCalc = subjects.Single(s => s.Name == "Differential Calculus");
            var contentTypes = await context.ContentTypes.ToListAsync();
            var textContentType = contentTypes.Single(c => c.Name == "Text");
            var mistakes = await context.Mistakes.ToListAsync();
            var calculationMistake = mistakes.Single(m => m.Name == "Calculation");

            //// ----- Fixed, known core (predictable - safe to assert against in tests) -----
            var teacher = new User
            {
                Firstname = "Anna",
                Lastname = "Madsen",
                Email = "anna.teacher@fbf.test"
            };
            teacher.UserRoles.Add(teacherRole);

            var student = new User
            {
                Firstname = "Mikkel",
                Lastname = "Sorensen",
                Email = "mikkel.student@fbf.test"
            };
            student.UserRoles.Add(studentRole);

            var admin = new User
            {
                Firstname = "Admin",
                Lastname = "User",
                Email = "admin@fbf.test"
            };
            admin.UserRoles.Add(adminRole);

            var mathClass = new Class
            {
                Name = "1.g Mat A",
                Year = new DateTimeOffset(2025, 8, 1, 0, 0, 0, TimeSpan.Zero),
                Education = Education.STX,
                ClassLevel = ClassLevel.A,
                Course = mathCourse,
                Teacher = teacher
            };
            mathClass.Students.Add(student);

            var questionSet = new QuestionSet
            {
                Name = "Eksamenssaet 1",
                IsExam = true,
                IsDraft = false,
                Teacher = teacher
            };

            var question = new Question
            {
                ImgSrc = "https://example.com/question-1.png",
                Points = "2",
                ExamType = ExamType.Digital,
                ClassLevel = ClassLevel.A,
                QuestionDifficulty = QuestionDifficulty.Medium,
                QuestionMethodRequirement = QuestionMethodRequirement.ApplyFormula,
                Education = Education.STX,
                QuestionContext = QuestionContext.YesLight,
                StandardQuestion = StandardQuestion.Yes,
                NewOldSystem = NewOldSystem.New,
                Course = mathCourse,
                User = teacher
            };
            question.Subjects!.Add(diffCalc);
            question.ContentTypes!.Add(textContentType);

            // QuestionAnswer.QuestionId and QuestionSetId are both non-nullable ints on
            // the entity, so both must be set even though the comment on the property
            // suggests it's meant to be either/or. Flag this with the team if it's not
            // intentional - for now we satisfy the model as it stands.
            var questionAnswer = new QuestionAnswer
            {
                Name = "Facit til opgave 1",
                Url = "https://example.com/answer-1.pdf",
                Visibility = Visibility.Public,
                Question = question,
                QuestionSet = questionSet
            };
            questionAnswer.ContentTypes.Add(textContentType);

            var appendix = new Appendix
            {
                Name = "Bilag 1",
                Url = "https://example.com/appendix-1.pdf",
                Question = question
            };

            var questionCollection = new QuestionCollection
            {
                Points = "2",
                Sequence = 1,
                Question = question
            };

            var questionQuestionSet = new QuestionQuestionSet
            {
                Subject = diffCalc,
                Question = question,
                QuestionSet = questionSet,
                Order = 1
            };

            var studentResult = new StudentResult
            {
                TeacherPoint = 2,
                TeacherFeedback = "Godt klaret, husk metodebeskrivelse.",
                StudentSelfAssessmentPoints = 8,
                CreatedAt = DateTimeOffset.UtcNow,
                Question = question,
                QuestionSet = questionSet,
                Teacher = teacher,
                Student = student
            };
            studentResult.Mistakes.Add(calculationMistake);

            context.AddRange(
                teacher, student, admin, mathClass,
                questionSet, question, questionAnswer, appendix,
                questionCollection, questionQuestionSet, studentResult);




            // ----- Randomized supplementary data (Bogus) -----
            var random = new Random(451065);

            var userFaker = new Faker<User>()
                .RuleFor(u => u.Firstname, f => f.Name.FirstName())
                .RuleFor(u => u.Lastname, f => f.Name.LastName())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Firstname, u.Lastname));

            var extraTeachers = userFaker.Generate(2);
            foreach (var t in extraTeachers) t.UserRoles.Add(teacherRole);

            var extraStudents = userFaker.Generate(3);
            foreach (var s in extraStudents) s.UserRoles.Add(studentRole);

            var allTeachers = new List<User> { teacher };
            allTeachers.AddRange(extraTeachers);

            var mathClassFaker = new Faker<Class>()
                .RuleFor(c => c.Name, f => "Math2")
                .RuleFor(c => c.Year, f => DateTimeOffset.UtcNow)
                .RuleFor(c => c.Education, f => f.PickRandom<Education>())
                .RuleFor(c => c.ClassLevel, f => f.PickRandom<ClassLevel>())
                .RuleFor(c => c.Course, f => mathCourse)
                .RuleFor(c => c.Teacher, f => f.PickRandom(allTeachers));
            var extraMathClasses = mathClassFaker.Generate();
            extraMathClasses.Students.AddRange(extraStudents);

            var questionFaker = new Faker<Question>()
                .RuleFor(q => q.ImgSrc, f => f.Image.PicsumUrl())
                .RuleFor(q => q.Points, f => f.Random.Int(1, 10).ToString())
                .RuleFor(q => q.ExamType, f => f.PickRandom<ExamType>())
                .RuleFor(q => q.ClassLevel, f => f.PickRandom<ClassLevel>())
                .RuleFor(q => q.QuestionDifficulty, f => f.PickRandom<QuestionDifficulty>())
                .RuleFor(q => q.QuestionMethodRequirement, f => f.PickRandom<QuestionMethodRequirement>())
                .RuleFor(q => q.Education, f => f.PickRandom<Education>())
                .RuleFor(q => q.QuestionContext, f => f.PickRandom<QuestionContext>())
                .RuleFor(q => q.StandardQuestion, f => f.PickRandom<StandardQuestion>())
                .RuleFor(q => q.NewOldSystem, f => f.PickRandom<NewOldSystem>())
                .RuleFor(q => q.Course, f => mathCourse)
                .RuleFor(q => q.User, f => f.PickRandom(allTeachers));

            var extraQuestions = questionFaker.Generate(4);
            foreach (var q in extraQuestions)
            {
                q.Subjects!.Add(subjects[random.Next(subjects.Count)]);
                q.ContentTypes!.Add(contentTypes[random.Next(contentTypes.Count)]);
            }

            var questionQuestionSetFaker = new Faker<QuestionQuestionSet>()
                .RuleFor(q => q.Question, f => extraQuestions[f.IndexFaker])
                .RuleFor(q => q.QuestionSet, f => questionSet)
                .RuleFor(q => q.Subject, f => f.PickRandom(subjects))
                .RuleFor(q => q.Order, f => f.IndexFaker + 2);

            var extraQuestionQuestionSet = questionQuestionSetFaker.Generate(extraQuestions.Count);

            var extraAnswerFaker = new Faker<QuestionAnswer>()
                .RuleFor(a => a.Name, f => $"Facit - {f.Lorem.Sentence(3)}")
                .RuleFor(a => a.Url, f => f.Internet.Url())
                .RuleFor(a => a.Visibility, f => f.PickRandom<Visibility>())
                .RuleFor(a => a.Question, f => f.PickRandom(extraQuestions))
                .RuleFor(a => a.QuestionSet, f => questionSet);

            var extraAnswers = extraAnswerFaker.Generate(5);

            var extraResultFaker = new Faker<StudentResult>()
                .RuleFor(r => r.TeacherPoint, f => f.Random.Int(0, 10))
                .RuleFor(r => r.TeacherFeedback, f => f.Lorem.Sentence())
                .RuleFor(r => r.StudentSelfAssessmentPoints, f => f.Random.Int(0, 10))
                .RuleFor(r => r.CreatedAt, f => DateTimeOffset.UtcNow)
                .RuleFor(r => r.Question, f => f.PickRandom(extraQuestions))
                .RuleFor(r => r.QuestionSet, f => questionSet)
                .RuleFor(r => r.Teacher, f => f.PickRandom(allTeachers))
                .RuleFor(r => r.Student, f => f.PickRandom(extraStudents));

            var extraResults = extraResultFaker.Generate(15);
            foreach (var r in extraResults)
            {
                r.Mistakes.Add(mistakes[random.Next(mistakes.Count)]);
            }

            context.AddRange(extraTeachers);
            context.AddRange(extraStudents);
            context.AddRange(extraQuestions);
            context.AddRange(extraAnswers);
            context.AddRange(extraResults);
            context.AddRange(extraMathClasses);
            context.AddRange(extraQuestionQuestionSet);

            await context.SaveChangesAsync();
        }
    }
}
