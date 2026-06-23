using feedbackFlowAPI.Controllers;
using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    public class CreateClassMoq
    {
        private string abc = "string";


        [Fact]
        public async Task Mock()
        {
            //Arrange
            var mockService = new Mock<IClassService>();

            var inputDto = new ClassDTO { Name = "3A", TeacherId = 1, CourseId = 2 };
            var returnedDto = new ClassDTO { Name = "3A", TeacherId = 1, CourseId = 2 };

            mockService.Setup(s => s.CreateClass(inputDto))
                .ReturnsAsync(returnedDto);

            var controller = new ClassesController(mockService.Object);
            
            //Act
            var result = await controller.PostClass(inputDto);

            //Assert
            var createdResult = result.Result.Should().BeOfType<CreatedResult>().Subject;
            
            createdResult.StatusCode.Should().Be(201);
            createdResult.Value.Should().BeEquivalentTo(returnedDto);
        }
    }
}
