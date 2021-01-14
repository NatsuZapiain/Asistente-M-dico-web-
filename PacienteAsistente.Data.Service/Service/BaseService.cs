using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace PacienteAsistente.Data.Service.Service
{
    public class BaseService<T> where T : class
    {
        protected readonly ControlMedicamentoEntities DataBase;

        protected BaseService()
        {
            DataBase = new ControlMedicamentoEntities();
        }

        public List<T> GetAll()
        {
            return DataBase.Set<T>().ToList();
        }

        public bool Update(T element)
        {
            try
            {
                DataBase.Entry(element).State = System.Data.Entity.EntityState.Modified;
                DataBase.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException )
            {
                return false;
            }
            catch (Exception )
            {
                return false;
            }
        }

        public bool Delete(T element)
        {
            try
            {
                DataBase.Entry(element).State = System.Data.Entity.EntityState.Deleted;
                DataBase.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException )
            {
                return false;
            }
            catch (Exception )
            {
                return false;
            }
        }

        public bool Create(T element)
        {
            try
            {
                DataBase.Set<T>().Add(element);
                DataBase.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException )
            {
                return false;
            }
            catch (Exception )
            {
                return false;
            }
        }

        public bool CreateAll(List<T> elements)
        {
            try
            {
                DataBase.Set<T>().AddRange(elements);
                DataBase.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException )
            {
                return false;
            }
            catch (Exception )
            {
                return false;
            }
        }

        public bool UpdateAll(List<T> elements)
        {
            try
            {
                foreach (var element in elements)
                {
                    DataBase.Entry(element).State = EntityState.Modified;
                }
                DataBase.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException )
            {
                return false;
            }
            catch (Exception )
            {
                return false;
            }
        }
    }
}
