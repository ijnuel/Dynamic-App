using Application.Helpers;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class EnumResponseModel
    {
        public EnumResponseModel(Enum enumItem)
        {
            Value = (int)(object)enumItem;
            Name = enumItem.ToString();
            Description = ((Enum)(object)enumItem).GetDescription();
        }
        public int Value { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
