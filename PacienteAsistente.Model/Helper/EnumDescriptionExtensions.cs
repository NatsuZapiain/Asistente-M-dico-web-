using System;
using System.Linq;
using System.Reflection;

namespace PacienteAsistente.Model.Helper
{
    public static class EnumDescriptionExtensions
    {
        public static string GetEnumDescription<T>(this T enumValue) where T : struct
        {
            var castEnum = enumValue as System.Enum;
            if (castEnum == null) throw new InvalidCastException("Should be not a Enum");
            var attribute = GetAttributeOfType<EnumDescriptionAttribute>(castEnum);
            return attribute == null ? castEnum.ToString() : attribute.Description;
        }

        private static T GetAttributeOfType<T>(this System.Enum enumVal) where T : Attribute
        {
            var typeInfo = enumVal.GetType().GetTypeInfo();
            var v = typeInfo.DeclaredMembers.FirstOrDefault(x => x.Name == enumVal.ToString());
            return v == null ? null : v.GetCustomAttribute<T>();
        }
    }
}
