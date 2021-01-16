using System;

namespace PacienteAsistente.Data.Service.Interface
{
    public interface ITratamientoDataService : IBaseDataService<Tratamiento>
    {
        Tratamiento GetbyId(Guid Id);
    }
}
