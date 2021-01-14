using System.Collections.Generic;

namespace PacienteAsistente.Data.Service.Interface
{
    public interface IBaseDataService<T> where T : class
    {
        List<T> GetAll();
        bool Update(T element);
        bool Delete(T element);
        bool Create(T element);
        bool CreateAll(List<T> elements);
        bool UpdateAll(List<T> elements);
    }
}
