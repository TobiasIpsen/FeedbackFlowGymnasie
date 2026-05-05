

using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers;
using feedbackFlowAPI.Mappers.Implementations;
using feedbackFlowAPI.Mappers.Interface;
using feedbackFlowAPI.Services.Implementations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;

namespace UnitTests
{
    public class ClassStatistics
    {



        [Fact]
        public async Task Get_Class_Statistics_Includes_Correct_Information()
        {
            /*
                Info needed on return:
                - Klassens samlet score, på papir og digitalt
                - Klassens gennemsnit score pr emne
                - Elev gennemsnit score på papir og digitalt
                - Top 3 svageste emner
                - Data kan filtreres med startDate, endDate, og afleveringstype

                Pseudo JSON:
                startdate,
                enddate,
                studentsScoreAssignments: [{student, question, score},]
                classAvgScoreCombined,
                classAvgScoreAnalog,
                classAvgScoreDigital,
                classAvgSubjectScoreCombined: [{subject, examType score},],
                classAvgSubjectScoreAnalog: [{subject, examType, score},],
                classAvgSubjectScoreDigital: [{subject, examType, score},],
                studentsAvgScoreCombined: [{student, examType, score},],
                studentsAvgScoreAnalog: [{student, examType, score},],
                studentsAvgScoreDigital: [{student, examType, score},],
                studentsScoreSubject: [{student, subject, score},],
                weakestSubjects: [{subject, avgScore},],
            */

            //Arrange
            var options = new DbContextOptionsBuilder<FbfDbContext>()
                .UseInMemoryDatabase(databaseName: "db_" + Guid.NewGuid())
                .Options;

            List<QuestionQuestionSet> questionQuestionSets = new List<QuestionQuestionSet>
            {
                new QuestionQuestionSet { Order = 1, QuestionId = 1, QuestionSetId = 1, SubjectId = 2},
                new QuestionQuestionSet { Order = 2, QuestionId = 2, QuestionSetId = 1, SubjectId = 5},
            };

            Question q1 = new Question
            {
                ImgSrc = "abc/123.png",
                Points = "10",
                ExamType = ExamType.Analog,
                ClassLevel = ClassLevel.A,
                QuestionDifficulty = QuestionDifficulty.Hard,
                QuestionMethodRequirement = QuestionMethodRequirement.NoRequirement,
                Education = Education.HF,
                QuestionContext = QuestionContext.YesLight,
                StandardQuestion = StandardQuestion.Yes,
                NewOldSystem = NewOldSystem.New,
                CourseId = 1,
                UserId = 1,
                QuestionSets = questionQuestionSets
            };
            Question q2 = new Question
            {
                ImgSrc = "abc/456.png",
                Points = "5",
                ExamType = ExamType.Digital,
                ClassLevel = ClassLevel.B,
                QuestionDifficulty = QuestionDifficulty.Easy,
                QuestionMethodRequirement = QuestionMethodRequirement.ApplyFormula,
                Education = Education.HHX,
                QuestionContext = QuestionContext.No,
                StandardQuestion = StandardQuestion.WithATwist,
                NewOldSystem = NewOldSystem.Old,
                CourseId = 1,
                UserId = 1,
                QuestionSets = questionQuestionSets
            };

            UserRole admin = new UserRole { Id = 1, Name = "Admin" };
            UserRole teacher = new UserRole { Id = 2, Name = "Teacher" };
            UserRole student = new UserRole { Id = 3, Name = "Student" };

            List<UserRole> teacherAdmin = new List<UserRole> { teacher, admin };
            List<UserRole> studentList = new List<UserRole> { student };

            List<Subject> subjects = new List<Subject>
                {
                    new Subject{ Name = "Combinatorics"},
                    new Subject { Name = "Differential Calculus" },
                    new Subject { Name = "Quadratic polynomial" },
                    new Subject { Name = "Regression" },
                    new Subject { Name = "Exponential function" },
                    new Subject { Name = "Binomial distribution" },
                };

            List<User> users = new List<User>
                {
                    new User { Firstname = "Tobias", Lastname = "I", Email = "a@a.dk", UserRoles = teacherAdmin },
                    new User { Firstname = "Aleksander", Lastname = "A", Email = "a@a.dk", UserRoles = studentList },
                    new User { Firstname = "Emil", Lastname = "I", Email = "a@a.dk", UserRoles = studentList },
                    new User { Firstname = "Bob", Lastname = "I", Email = "a@a.dk", UserRoles = studentList },
                    new User { Firstname = "Niels", Lastname = "I", Email = "a@a.dk", UserRoles = studentList },
                    new User { Firstname = "Niels", Lastname = "I", Email = "a@a.dk", UserRoles = studentList },
                };

            QuestionSet questionSet = new QuestionSet
            {
                Name = "First Question Set",
                IsExam = false,
                IsDraft = false,
                //TeacherId = users.Single(u => u.Firstname == "Tobias").Id
                TeacherId = 1,
            };

            await using (var context = new FbfDbContext(options))
            {
                await context.Courses.AddAsync(new Course { Name = "Math" });

                await context.Questions.AddAsync(q1);
                await context.Questions.AddAsync(q2);

                await context.UserRoles.AddAsync(admin);
                await context.UserRoles.AddAsync(teacher);
                await context.UserRoles.AddAsync(student);

                await context.Users.AddRangeAsync(users);
                await context.Subjects.AddRangeAsync(subjects);

                await context.QuestionSets.AddAsync(questionSet);

                /*
                //await context.Users.AddAsync(new User { Firstname = "Tobias", Lastname = "I", Email = "a@a.dk", UserRoles = teacherAdmin });
                //await context.Users.AddAsync(new User { Firstname = "Aleksander", Lastname = "A", Email = "a@a.dk", UserRoles = studentList });
                //await context.Users.AddAsync(new User { Firstname = "Emil", Lastname = "I", Email = "a@a.dk", UserRoles = studentList });
                //await context.Users.AddAsync(new User { Firstname = "Bob", Lastname = "I", Email = "a@a.dk", UserRoles = studentList });
                //await context.Users.AddAsync(new User { Firstname = "Niels", Lastname = "I", Email = "a@a.dk", UserRoles = studentList });
                //await context.Users.AddAsync(new User { Firstname = "Niels", Lastname = "I", Email = "a@a.dk", UserRoles = studentList });

                //await context.Subjects.AddAsync(new Subject { Name = "Combinatorics" });
                //await context.Subjects.AddAsync(new Subject { Name = "Differential Calculus" });
                //await context.Subjects.AddAsync(new Subject { Name = "Quadratic polynomial" });
                //await context.Subjects.AddAsync(new Subject { Name = "Regression" });
                //await context.Subjects.AddAsync(new Subject { Name = "Exponential function" });
                //await context.Subjects.AddAsync(new Subject { Name = "Binomial distribution" });
                */
                await context.SaveChangesAsync();
            }

            var result = "null";

            //Act
            await using (var context = new FbfDbContext(options))
            {
                IClassMapper classMapper = new ClassMapper();
                ClassService _service = new ClassService(context, classMapper);

                //result = await _service.GetStatistics();
            }

            //Assert
            //result.studentsScoreAssignments.Should().BeOfType<object>;
            //result.classAvgScoreCombined.Should().BeOfType<int>;
            //result.classAvgScoreAnalog.Should().BeOfType<int>;
            //result.classAvgScoreDigital.Should().BeOfType<int>;
            //result.classAvgSubjectScoreCombined.Should().BeOfType<object>;
            //result.classAvgSubjectScoreAnalog.Should().BeOfType<object>;
            //result.classAvgSubjectScoreDigital.Should().BeOfType<object>;
            //result.studentsAvgScoreCombined.Should().BeOfType<object>;
            //result.studentsAvgScoreAnalog.Should().BeOfType<object>;
            //result.studentsAvgScoreDigital.Should().BeOfType<object>;
            //result.studentsScoreSubject.Should().BeOfType<object>;
            //result.weakestSubjects.Should().BeOfType<object>;
        }
    }
}
