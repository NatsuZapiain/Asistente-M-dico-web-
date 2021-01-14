using System.Linq;
using PacienteAsistente.Data.Service.Interface;

namespace PacienteAsistente.Data.Service.Service
{
    public class AsistenteDataService : BaseService<Asistente>, IAsistenteDataService
    {
        public Asistente GetByAspNet(string id)
        {
            return DataBase.Asistentes.FirstOrDefault(x => x.AspNetUserId == id);
        }
    }
}
