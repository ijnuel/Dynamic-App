namespace Core.EntityInterfaces
{
    public interface IFormResponsesSchema
    {
        public Guid ProgramFormId { get; set; }
    }
    public interface IQuestionAnswerSchema
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
