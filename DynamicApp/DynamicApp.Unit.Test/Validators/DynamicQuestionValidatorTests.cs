using Application.Models.Dtos;
using Core.Enums;
using FluentValidation.TestHelper;


namespace DynamicApp.Unit.Test.Validators
{
    public class DynamicQuestionValidatorTests
    {
        private readonly DynamicQuestionValidator validator;

        public DynamicQuestionValidatorTests()
        {
            validator = new DynamicQuestionValidator();
        }

        [Theory]
        [InlineData(QuestionType.Dropdown, "", false)]
        [InlineData(QuestionType.MultipleChoice, "", false)]
        [InlineData(QuestionType.MultipleChoice, "Choice 1,Choice 2", true)]
        public void Should_Have_Error_When_Choices_Are_Invalid(QuestionType type, string choices, bool isValid)
        {
            // Arrange
            List<string> errorMessages = new();
            var dto = new DynamicQuestionDto
            {
                Type = type,
                Question = "Sample question",
                Choices = choices.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList(),
                MaxChoiceAllowed = 2
            };

            // Act
            var result = validator.TestValidate(dto);
            result.Errors.ForEach(x => errorMessages.AddRange(x.ErrorMessage.Split(" | ", StringSplitOptions.RemoveEmptyEntries)));

            // Assert
            Assert.Equal(isValid, result.IsValid);
            if (!isValid)
            {
                Assert.Contains($"Please include choices for '{dto.Question}'", errorMessages);
            }
        }

        [Theory]
        [InlineData(QuestionType.MultipleChoice, 0)]
        [InlineData(QuestionType.MultipleChoice, -1)]
        public void Should_Have_Error_When_MaxChoiceAllowed_Is_Invalid(QuestionType type, int maxChoiceAllowed)
        {
            // Arrange
            List<string> errorMessages = new();
            var dto = new DynamicQuestionDto
            {
                Type = type,
                Question = "Sample question",
                MaxChoiceAllowed = maxChoiceAllowed
            };

            // Act
            var result = validator.TestValidate(dto);
            result.Errors.ForEach(x => errorMessages.AddRange(x.ErrorMessage.Split(" | ", StringSplitOptions.RemoveEmptyEntries)));

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains($"Max choice allowed for '{dto.Question}' must be greater than zero", errorMessages);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Validation_Is_Passed()
        {
            // Arrange
            var dto = new DynamicQuestionDto
            {
                Type = QuestionType.MultipleChoice,
                Question = "Sample question",
                Choices = new List<string> { "Choice 1", "Choice 2" },
                MaxChoiceAllowed = 2
            };

            // Act
            var result = validator.TestValidate(dto);

            // Assert
            Assert.True(result.IsValid);
        }
    }

}
