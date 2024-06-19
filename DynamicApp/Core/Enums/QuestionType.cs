using System.ComponentModel;

namespace Core.Enums
{
    public enum QuestionType
    {
        [Description("Paragraph")]
        Paragraph,
        [Description("Yes/No")]
        YesNo,
        [Description("Dropdown")]
        Dropdown,
        [Description("Multiple Choice")]
        MultipleChoice,
        [Description("Date")]
        Date,
        [Description("Number")]
        Number
    }
}
