using Core.EntityInterfaces;
using Core.EntityInterfaces.Base;
using Core.EntityModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
