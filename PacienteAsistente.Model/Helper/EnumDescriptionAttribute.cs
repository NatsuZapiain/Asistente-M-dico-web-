using System;

namespace PacienteAsistente.Model.Helper
{
    public class EnumDescriptionAttribute : Attribute
    {
        public string Description { get; set; }

        public EnumDescriptionAttribute(string description)
        {
            Description = description;
        }
    }
}
