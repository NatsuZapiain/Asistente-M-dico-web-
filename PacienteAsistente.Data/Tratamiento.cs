//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PacienteAsistente.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tratamiento
    {
        public System.Guid Id { get; set; }
        public System.Guid IdRegAsistente { get; set; }
        public System.Guid IdUsuario { get; set; }
        public string NomMedicamento { get; set; }
        public string TipoMedicamento { get; set; }
        public string DosisDia { get; set; }
        public System.DateTime FechaAplicacion { get; set; }
        public System.TimeSpan HoraAplicacion { get; set; }
        public Nullable<System.Guid> Aplicador { get; set; }
    
        public virtual Asistente Asistente { get; set; }
    }
}
