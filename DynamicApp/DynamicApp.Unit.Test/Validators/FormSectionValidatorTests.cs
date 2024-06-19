using Application.Models.Dtos;
using Core.Enums;
using FluentValidation.TestHelper;

namespace DynamicApp.Unit.Test.Validators
{
    public class FormSectionValidatorTests
    {
        private readonly FormSectionValidator validator;

        public FormSectionValidatorTests()
        {
            validator = new FormSectionValidator();
        }

        [Theory]
        [InlineData("Section Title", true)] // Valid scenario with questions
        [InlineData("Section Title", false)] // Invalid scenario without questions
        public void Should_Have_Error_When_Questions_Are_Missing(string sectionTitle, bool isValid)
        {
            // Arrange
            List<string> errorMessages = new();
            var dto = new FormSectionDto
            {
                Title = sectionTitle,
                Questions = isValid ? 
                new List<DynamicQuestionDto> { 
                    new DynamicQuestionDto { 
                        Type = QuestionType.YesNo, 
                        Question = "Sample question" 
                    } 
                } : null
            };

            // Act
            var result = validator.TestValidate(dto);
            result.Errors.ForEach(x => errorMessages.AddRange(x.ErrorMessage.Split(" | ", StringSplitOptions.RemoveEmptyEntries)));

            // Assert
            Assert.Equal(isValid, result.IsValid);
            if (!isValid)
            {
                Assert.Contains($"Please add questions to '{dto.Title}'", errorMessages);
            }
        }

        [Fact]
        public void Should_Have_Error_When_Question_Validation_Fails()
        {
            // Arrange
            List<string> errorMessages = new();
            var dto = new FormSectionDto
            {
                Title = "Section Title",
                Questions = new List<DynamicQuestionDto>
                {
                    new DynamicQuestionDto 
                    { 
                        Type = QuestionType.MultipleChoice, 
                        Question = "Invalid question", 
                        MaxChoiceAllowed = 0
                    }
                }
            };

            // Act
            var result = validator.TestValidate(dto);
            result.Errors.ForEach(x => errorMessages.AddRange(x.ErrorMessage.Split(" | ", StringSplitOptions.RemoveEmptyEntries)));

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains($"Max choice allowed for 'Invalid question' must be greater than zero", errorMessages);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Validation_Is_Passed()
        {
            // Arrange
            var dto = new FormSectionDto
            {
                Title = "Section Title",
                Questions = new List<DynamicQuestionDto>
                {
                    new DynamicQuestionDto 
                    { 
                        Type = QuestionType.Dropdown,
                        Question = "Valid question",
                        Choices = new List<string> 
                        { 
                            "Choice 1", 
                            "Choice 2" 
                        } 
                    }
                }
            };

            // Act
            var result = validator.TestValidate(dto);

            // Assert
            Assert.True(result.IsValid);
        }
    }

}
