using PacienteAsistente.Model.Models;

namespace PacienteAsistente.Data.Service.Interface
{
    public interface IPacienteService
    {
        bool RegistroPaciente(string userName);
        PacienteViewModel GetModelPerfil(string userName);
        bool UpdateDataPerfil(string userName, PacienteViewModel model);
        IndexViewModel IndexPaciente(string userName);

        listaAplicacionViewModel GetListaAplicacionViewModel(string userName);
        ListaAsistentesViewModel getListaAsistentes(string userName);

        listaAplicacionViewModel GetHistorialAplicacion(string userName);
    }
}
