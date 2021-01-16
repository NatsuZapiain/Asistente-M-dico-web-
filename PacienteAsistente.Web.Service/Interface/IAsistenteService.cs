using PacienteAsistente.Model.Models;

namespace PacienteAsistente.Data.Service.Interface
{
    public interface IAsistenteService
    {
        bool RegistroAsistente(string userName, string code);
        AsistenteViewModel GetModelPerfil(string userName);
        PacienteViewModel GetModelPerfilPaciente(string userName);
        bool UpdateDataPerfil(string userName, AsistenteViewModel model);
        listaAplicacionViewModel GetListaAplicacionViewModel(string userName);
        bool RegistroAplicacion(string userName, listaAplicacionViewModel model);
        ListaAsistentesViewModel getListaAsistentes(string userName);
        bool RegistroMedicamentoPaciente(string userName, TratamientoViewModel model);
    }

}
