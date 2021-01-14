using System.Linq;
using PacienteAsistente.Data;
using PacienteAsistente.Data.Service.Interface;
using PacienteAsistente.Data.Service.Service;
using PacienteAsistente.Model.Enum;
using PacienteAsistente.Model.Helper;
using PacienteAsistente.Web.Service.Interface;

namespace PacienteAsistente.Web.Service.Service
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly IAspNetUserDataService _aspNetUserDataService;

        public AuthorizeService()
        {
            _aspNetUserDataService = new AspNetUserDataService();
        }

        private bool ValidPermissions(AspNetUser user, RoleEnum roleEnum)
        {
            var roles = user.AspNetRoles.Select(x => x.Name).ToList();
            return roles.Contains(roleEnum.GetEnumDescription());
        }

        public bool GetIfHaveAothorization(string name, RoleEnum role)
        {
            var user = _aspNetUserDataService.GetByUserName(name);
            return user != null && ValidPermissions(user, role);
        }
    }
}
