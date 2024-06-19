using Core.EntityModels;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EntityInterfaces
{
    public interface IProgramFormSchema
    {
        public string ProgramTitle { get; set; }
        public string ProgramDescription { get; set; }

    }
    public interface IFormSectionSchema
    {
        public string Title { get; set; }
    }
    public interface IDynamicQuestionSchema
    {
        public QuestionType Type { get; set; }
        public string Question { get; set; }
        public List<string> Choices { get; set; }
        public bool IsOtherOptionEnabled { get; set; }
        public int MaxChoiceAllowed { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsInternal { get; set; }
        public bool IsHidden { get; set; }

    }
}
