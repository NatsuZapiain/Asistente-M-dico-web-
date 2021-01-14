namespace PacienteAsistente.Data.Service.Interface
{
    public interface IAsistenteDataService : IBaseDataService<Asistente>
    {
        Asistente GetByAspNet(string id);
    }
}
