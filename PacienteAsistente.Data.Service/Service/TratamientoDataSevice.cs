using System;
using System.Linq;
using PacienteAsistente.Data.Service.Interface;

namespace PacienteAsistente.Data.Service.Service
{
    public class TratamientoDataSevice : BaseService<Tratamiento>, ITratamientoDataService

    {
        public Tratamiento GetbyId(Guid Id)
        {
            return DataBase.Tratamientoes.FirstOrDefault(x => x.Id == Id);
        }
        
    }
}
