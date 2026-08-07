using feedbackFlowAPI;
using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers;
using feedbackFlowAPI.Mappers.Interface;
using feedbackFlowAPI.Services.Implementations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests
{
    public class StudentResultsServiceTests
    {
        private readonly Mock<IStudentResultMapper> _mapperMock;
        private readonly Mock<ILogger<Program>> _loggerMock;
        private readonly FbfDbContext _context;
        private readonly StudentResultsService _service;

        public StudentResultsServiceTests()
        {
            var options = new DbContextOptionsBuilder<FbfDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new FbfDbContext(options);

            _mapperMock = new Mock<IStudentResultMapper>();
            _loggerMock = new Mock<ILogger<Program>>();
            _service = new StudentResultsService(_loggerMock.Object, _context, _mapperMock.Object);
        }

        [Fact]
        public async Task SaveTeacherFeedbackAsync()
        {
            TeacherFeedbackDTO dto = new TeacherFeedbackDTO { TeacherId = 1 };

            StudentResult entity = new StudentResult
            {
                StudentId = 5,
                QuestionSetId = 10,
                QuestionId = 20,
                TeacherId = 1,
            };

            _mapperMock
                .Setup(m => m.ToEntity(dto, 5, 10, 20))
                .Returns(entity);

            _mapperMock
                .Setup(m => m.ToTeacherFeedbackDTO(It.IsAny<StudentResult>()))
                .Returns(new TeacherFeedbackDTO { TeacherId = 1 });

            var result = await _service.SaveTeacherFeedbackAsync(5, 10, 20, dto);

            result.Should().NotBeNull();
            result.TeacherId.Should().Be(1);

            _mapperMock.Verify(m => m.ToEntity(dto, 5, 10, 20), Times.Once);

            var dbItemsCount = await _context.StudentResults.CountAsync();
            dbItemsCount.Should().Be(1);
        }
    }
}
