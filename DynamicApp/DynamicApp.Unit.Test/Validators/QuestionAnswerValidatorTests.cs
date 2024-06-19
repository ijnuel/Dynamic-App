using Application.Models.Dtos;
using Core.Enums;
using FluentValidation.TestHelper;


namespace DynamicApp.Unit.Test.Validators
{
    public class QuestionAnswerValidatorTests
    {
        private readonly QuestionAnswerValidator validator;

        public QuestionAnswerValidatorTests()
        {
            validator = new QuestionAnswerValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Question_Is_Null()
        {
            // Arrange
            List<string> errorMessages = new();
            var dto = new QuestionAnswerDto
            {
                Question = null,
                QuestionType = QuestionType.Paragraph,
                IsMandatory = true,
                Answer = "Some answer"
            };

            // Act
            var result = validator.TestValidate(dto);
            result.Errors.ForEach(x => errorMessages.AddRange(x.ErrorMessage.Split(" | ", StringSplitOptions.RemoveEmptyEntries)));

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains("'Question' must not be empty.", errorMessages);
        }

        [Fact]
        public void Should_Have_Error_When_Answer_Is_Empty_And_Mandatory()
        {
            // Arrange
            List<string> errorMessages = new();
            var dto = new QuestionAnswerDto
            {
                Question = "Sample question",
                QuestionType = QuestionType.Paragraph,
                IsMandatory = true,
                Answer = string.Empty
            };

            // Act
            var result = validator.TestValidate(dto);
            result.Errors.ForEach(x => errorMessages.AddRange(x.ErrorMessage.Split(" | ", StringSplitOptions.RemoveEmptyEntries)));

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains($"Invalid response for '{dto.Question}'", errorMessages);
        }

        [Fact]
        public void Should_Have_Error_When_Answer_Is_Not_A_Valid_Date()
        {
            // Arrange
            List<string> errorMessages = new();
            var dto = new QuestionAnswerDto
            {
                Question = "Date question",
                QuestionType = QuestionType.Date,
                IsMandatory = false,
                Answer = "Not a date"
            };

            // Act
            var result = validator.TestValidate(dto);
            result.Errors.ForEach(x => errorMessages.AddRange(x.ErrorMessage.Split(" | ", StringSplitOptions.RemoveEmptyEntries)));

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains($"Date is required for '{dto.Question}'", errorMessages);
        }

        [Fact]
        public void Should_Have_Error_When_Answer_Is_Not_A_Valid_Number()
        {
            // Arrange
            List<string> errorMessages = new();
            var dto = new QuestionAnswerDto
            {
                Question = "Number question",
                QuestionType = QuestionType.Number,
                IsMandatory = false,
                Answer = "Not a number"
            };

            // Act
            var result = validator.TestValidate(dto);
            result.Errors.ForEach(x => errorMessages.AddRange(x.ErrorMessage.Split(" | ", StringSplitOptions.RemoveEmptyEntries)));

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains($"Number is required for '{dto.Question}'", errorMessages);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Answer_Is_Valid()
        {
            // Arrange
            var dto = new QuestionAnswerDto
            {
                Question = "Valid question",
                QuestionType = QuestionType.Paragraph,
                IsMandatory = false,
                Answer = "Valid answer"
            };

            // Act
            var result = validator.TestValidate(dto);

            // Assert
            Assert.True(result.IsValid);
        }
    }

}
