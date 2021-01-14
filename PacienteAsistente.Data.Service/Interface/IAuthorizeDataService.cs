using PacienteAsistente.Model.Enum;

namespace PacienteAsistente.Data.Service.Interface
{
    public interface IAuthorizeDataService
    {
        bool GetIfHaveAothorization(string name, RoleEnum role);
    }
}
