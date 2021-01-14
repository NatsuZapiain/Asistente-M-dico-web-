using System.Linq;
using PacienteAsistente.Data.Service.Interface;

namespace PacienteAsistente.Data.Service.Service
{
    public class PacienteDataService : BaseService<Paciente>, IPacienteDataService
    {
        public Paciente GetByCode(string code)
        {
            return DataBase.Pacientes.FirstOrDefault(x => x.CodPaciente == code);
        }

        public Paciente GetByAspNet(string id)
        {
            return DataBase.Pacientes.FirstOrDefault(x => x.AspNetUserId == id);
        }
    }
}
