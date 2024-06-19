using Core.EntityInterfaces;
using Core.EntityModels.Base;

namespace Core.EntityModels
{
    public class FormResponses : BaseEntity, IFormResponsesSchema
    {
        public Guid ProgramFormId { get; set; }
        public List<QuestionAnswer> QuestionAnswers { get; set; }
    }
    public class QuestionAnswer : IQuestionAnswerSchema
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
