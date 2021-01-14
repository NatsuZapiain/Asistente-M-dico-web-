namespace PacienteAsistente.Data.Service.Interface
{
    public interface IGenericLoginService
    {
        string GetRolUserByAspNetUserId(string aspNetUserId);
    }
}
