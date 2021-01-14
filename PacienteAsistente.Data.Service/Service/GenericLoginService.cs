using PacienteAsistente.Data.Service.Interface;

namespace PacienteAsistente.Data.Service.Service
{
    public class GenericLoginService : IGenericLoginService
    {
        private readonly IAspNetUserDataService _aspNetUserDataService;

        public GenericLoginService()
        {
            _aspNetUserDataService = new AspNetUserDataService();
        }

        public string GetRolUserByAspNetUserId(string aspNetUserId)
        {
            return _aspNetUserDataService.GetUserRol(aspNetUserId);
        }
    }
}
