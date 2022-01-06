using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ActivityPlanner.Helpers
{
    public static class HelperEnums
    {
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
        {
            var typeInfo = enumVal.GetType().GetTypeInfo();
            var v = typeInfo.DeclaredMembers.First(x => x.Name == enumVal.ToString());
            return v.GetCustomAttribute<T>();
        }

        public static string GetDescription(Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return en.ToString();
        }
    }
}
