namespace PacienteAsistente.Data.Service.Interface
{
    public interface IPacienteDataService : IBaseDataService<Paciente>
    {
        Paciente GetByCode(string code);
        Paciente GetByAspNet(string id);
    }
}
