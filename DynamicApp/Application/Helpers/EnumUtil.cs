using Application.Models;
using System.ComponentModel;
using System.Reflection;

namespace Application.Helpers
{
    public static class EnumUtil
    {
        public static List<EnumResponseModel> GetEnum<T>() where T : Enum
        {
            var enums = (T[])Enum.GetValues(typeof(T));
            return enums.Select(x => new EnumResponseModel(x)).ToList();
        }
        public static string GetDescription(this Enum genericEnum)
        {
            Type type = genericEnum.GetType();
            MemberInfo[] memberInfos = type.GetMember(genericEnum.ToString());
            if (memberInfos.Length > 0)
            {
                var attributes = memberInfos[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes != null && attributes.Any())
                {
                    return ((DescriptionAttribute)attributes.ElementAt(0)).Description;
                }
            }
            return genericEnum.ToString();
        }
    }
}
