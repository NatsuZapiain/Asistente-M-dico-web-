using PacienteAsistente.Model.Models;

namespace PacienteAsistente.Data.Service.Interface
{
    public interface IAsistenteService
    {
        bool RegistroAsistente(string userName, string code);
        AsistenteViewModel GetModelPerfil(string userName);
        bool UpdateDataPerfil(string userName, AsistenteViewModel model);

    }
}
