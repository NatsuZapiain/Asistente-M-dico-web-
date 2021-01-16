using System;
using System.Collections.Generic;

namespace PacienteAsistente.Model.Models
{
    public class TratamientoViewModel
    {
        public string NomMedicamento { get; set; }
        public string TipoMedicamento { get; set; }
        public string DosisDia { get; set; }
        public DateTime FechaAplicacion { get; set; }
        public TimeSpan HoraAplicacion { get; set; }
        public string Aplicador { get; set; }
        public string Id { get; set; }
        public string IdUsuario { get; set; }
        public string Contador { get; set; }
        public DateTime FechaInicio { get; set; }
        public string DosisVariable { get; set; }
      
    }
    public class listaAplicacionViewModel{
        public List<TratamientoViewModel> ListaAplicacion { get; set; }
        public TratamientoViewModel  TratamientoAplicacion { get; set; }

    }
}
