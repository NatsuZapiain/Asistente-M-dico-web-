using System;
using System.Linq;
using PacienteAsistente.Data.Service.Interface;

namespace PacienteAsistente.Data.Service.Service
{
    public class AspNetUserDataService : BaseService<AspNetUser>, IAspNetUserDataService
    {
        public AspNetUser GetByEmail(string email)
        {
            return DataBase.AspNetUsers.FirstOrDefault(x => x.Email == email);
        }

        public AspNetUser GetByUserName(string userName)
        {
            return DataBase.AspNetUsers.FirstOrDefault(x => x.UserName == userName);
        }

        public AspNetUser GetById(string id)
        {
            try
            {
                return DataBase.AspNetUsers.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception )
            {
                return null;
            }
        }

        public string GetUserRol(string aspId)
        {
            var user = GetById(aspId);
            return user == null ? "" : user.AspNetRoles.Select(x => x.Name).FirstOrDefault();
        }

        public AspNetUser GetByRole(string role)
        {
            return DataBase.AspNetUsers.FirstOrDefault(x => x.AspNetRoles.Select(y => y.Name).Contains(role));
        }
    }
}
