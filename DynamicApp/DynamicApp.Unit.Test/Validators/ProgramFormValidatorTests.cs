using Application.Models.Dtos;
using Core.Enums;
using FluentValidation.TestHelper;
using Moq;

namespace DynamicApp.Unit.Test.Validators
{
    public class ProgramFormValidatorTests
    {
        private readonly ProgramFormValidator validator;

        public ProgramFormValidatorTests()
        {
            validator = new ProgramFormValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Sections_Are_Missing()
        {
            // Arrange
            List<string> errorMessages = new();
            var dto = new ProgramFormDto
            {
                ProgramTitle = "Sample Program",
                ProgramDescription = "Sample Program Description",
                Sections = null
            };

            // Act
            var result = validator.TestValidate(dto);
            result.Errors.ForEach(x => errorMessages.AddRange(x.ErrorMessage.Split(" | ", StringSplitOptions.RemoveEmptyEntries)));

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains("'Sections' must not be empty.", errorMessages);
        }

        [Fact]
        public void Should_Have_Error_When_Title_Is_Missing()
        {
            // Arrange
            List<string> errorMessages = new();
            var dto = new ProgramFormDto
            {
                ProgramTitle = null,
                ProgramDescription = "Sample Program Description",
                Sections = new List<FormSectionDto> 
                { 
                    new FormSectionDto 
                    { 
                        Title = "Section 1",
                        Questions = new List<DynamicQuestionDto>() 
                    } 
                }
            };

            // Act
            var result = validator.TestValidate(dto);
            result.Errors.ForEach(x => errorMessages.AddRange(x.ErrorMessage.Split(" | ", StringSplitOptions.RemoveEmptyEntries)));

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains("'Program Title' must not be empty.", errorMessages);
        }

        [Fact]
        public void Should_Have_Error_When_Description_Is_Missing()
        {
            // Arrange
            List<string> errorMessages = new();
            var dto = new ProgramFormDto
            {
                ProgramTitle = "Sample Program",
                ProgramDescription = null,
                Sections = new List<FormSectionDto> 
                { 
                    new FormSectionDto 
                    { 
                        Title = "Section 1",
                        Questions = new List<DynamicQuestionDto>()
                    } 
                }
            };

            // Act
            var result = validator.TestValidate(dto);
            result.Errors.ForEach(x => errorMessages.AddRange(x.ErrorMessage.Split(" | ", StringSplitOptions.RemoveEmptyEntries)));

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains("'Program Description' must not be empty.", errorMessages);
        }

        [Fact]
        public void Should_Have_Error_When_Section_Validation_Fails()
        {
            // Arrange
            List<string> errorMessages = new();
            var dto = new ProgramFormDto
            {
                ProgramTitle = "Sample Program",
                ProgramDescription = "Sample Program Description",
                Sections = new List<FormSectionDto> 
                { 
                    new FormSectionDto 
                    { 
                        Title = null, 
                        Questions = new List<DynamicQuestionDto>()
                    } 
                }
            };

            // Act
            var result = validator.TestValidate(dto);
            result.Errors.ForEach(x => errorMessages.AddRange(x.ErrorMessage.Split(" | ", StringSplitOptions.RemoveEmptyEntries)));

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Validation_Is_Passed()
        {
            // Arrange
            var dto = new ProgramFormDto
            {
                ProgramTitle = "Sample Program",
                ProgramDescription = "Sample Program Description",
                Sections = new List<FormSectionDto> 
                { 
                    new FormSectionDto 
                    { 
                        Title = "Section 1",
                        Questions = new List<DynamicQuestionDto>
                        {
                            new DynamicQuestionDto
                            {
                                Type = QuestionType.MultipleChoice,
                                Question = "Sample question",
                                Choices = new List<string> { "Choice 1", "Choice 2" },
                                MaxChoiceAllowed = 2
                            }
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
