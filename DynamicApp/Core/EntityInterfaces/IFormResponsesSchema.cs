using Core.Enums;

namespace Core.EntityInterfaces
{
    public interface IFormResponsesSchema
    {
        public Guid ProgramFormId { get; set; }
    }
    public interface IQuestionAnswerSchema
    {
        public string Question { get; set; }
        public QuestionType QuestionType { get; set; }
        public bool IsMandatory { get; set; }
        public string Answer { get; set; }
    }
}
