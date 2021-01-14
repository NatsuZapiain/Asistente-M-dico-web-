using PacienteAsistente.Model.Enum;

namespace PacienteAsistente.Web.Service.Interface
{
    public interface IAuthorizeService
    {
        bool GetIfHaveAothorization(string name, RoleEnum role);
    }
}
