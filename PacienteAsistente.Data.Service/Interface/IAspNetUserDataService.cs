namespace PacienteAsistente.Data.Service.Interface
{
    public interface IAspNetUserDataService : IBaseDataService<AspNetUser>
    {
        AspNetUser GetByEmail(string email);
        AspNetUser GetByUserName(string userName);
        AspNetUser GetById(string id);
        string GetUserRol(string aspId);

        AspNetUser GetByRole(string role);
    }
}
