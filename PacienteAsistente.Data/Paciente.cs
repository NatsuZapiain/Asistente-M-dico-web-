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
    
    public partial class Paciente
    {
        public System.Guid Id { get; set; }
        public string AspNetUserId { get; set; }
        public string NombreUsuario { get; set; }
        public string Genero { get; set; }
        public string Edad { get; set; }
        public string Padecimiento { get; set; }
        public string Alergias { get; set; }
        public string TipoSangre { get; set; }
        public string NumEmergencia { get; set; }
        public string CodPaciente { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
