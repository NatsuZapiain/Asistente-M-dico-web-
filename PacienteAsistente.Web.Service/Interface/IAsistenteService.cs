using PacienteAsistente.Model.Models;

namespace PacienteAsistente.Data.Service.Interface
{
    public interface IAsistenteService
    {
        bool RegistroAsistente(string userName, string code);
        AsistenteViewModel GetModelPerfil(string userName);
        PacienteViewModel GetModelPerfilPaciente(string userName);
        bool UpdateDataPerfil(string userName, AsistenteViewModel model);

        ListaAsistentesViewModel getListaAsistentes(string userName);
        bool RegistroMedicamentoPaciente(string userName, TratamientoViewModel model);
    }

}
