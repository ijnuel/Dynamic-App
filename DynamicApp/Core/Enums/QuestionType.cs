using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
