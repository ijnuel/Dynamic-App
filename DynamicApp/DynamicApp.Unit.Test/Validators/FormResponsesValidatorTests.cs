using Application.Models.Dtos;
using Application.Services.Abstractions.Base;
using Core.Enums;
using DynamicApp.Unit.Test.Mock;
using FluentValidation.TestHelper;
using Moq;

namespace DynamicApp.Unit.Test.Validators
{
    public class FormResponsesValidatorTests
    {
        private readonly FormResponsesValidator validator;
        private readonly Mock<IBaseService> mockBaseService;

        public FormResponsesValidatorTests()
        {
            mockBaseService = new MockBaseService().Mock;
            validator = new FormResponsesValidator(mockBaseService.Object);
        }

        [Fact]
        public async Task Should_Have_Error_When_ProgramFormId_Is_Invalid()
        {
            // Arrange
            List<string> errorMessages = new();
            var dto = new FormResponsesDto
            {
                ProgramFormId = Guid.NewGuid(), // Assuming this ID does not exist in the mocked baseService
                QuestionAnswers = new List<QuestionAnswerDto>
                {
                    new QuestionAnswerDto 
                    {
                        Question = "Sample question", 
                        QuestionType = QuestionType.Paragraph, 
                        IsMandatory = true, 
                        Answer = "Some answer"
                    }
                }
            };

            // Act
            var result = await validator.TestValidateAsync(dto);
            result.Errors.ForEach(x => errorMessages.AddRange(x.ErrorMessage.Split(" | ", StringSplitOptions.RemoveEmptyEntries)));

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains("Invalid Program Form Id", errorMessages);
        }

        [Fact]
        public async Task Should_Have_Error_When_No_Question_Answers_Are_Provided()
        {
            // Arrange
            List<string> errorMessages = new();
            var dto = new FormResponsesDto
            {
                ProgramFormId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"), // Assuming this ID exists in the mocked baseService
                QuestionAnswers = new List<QuestionAnswerDto>() // Empty list of answers
            };

            // Act
            var result = await validator.TestValidateAsync(dto);
            result.Errors.ForEach(x => errorMessages.AddRange(x.ErrorMessage.Split(" | ", StringSplitOptions.RemoveEmptyEntries)));

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains("Please add responses", errorMessages);
        }

        [Fact]
        public async Task Should_Have_Error_When_Question_Answers_Are_Invalid()
        {
            // Arrange
            List<string> errorMessages = new();
            var dto = new FormResponsesDto
            {
                ProgramFormId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"), // Assuming this ID exists in the mocked baseService
                QuestionAnswers = new List<QuestionAnswerDto>
                {
                    new QuestionAnswerDto 
                    { 
                        Question = "Invalid date question", 
                        QuestionType = QuestionType.Date, 
                        IsMandatory = true, 
                        Answer = "Not a date" 
                    }
                }
            };

            // Act
            var result = await validator.TestValidateAsync(dto);
            result.Errors.ForEach(x => errorMessages.AddRange(x.ErrorMessage.Split(" | ", StringSplitOptions.RemoveEmptyEntries)));

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains("Date is required for 'Invalid date question'", errorMessages);
        }
    }

}
