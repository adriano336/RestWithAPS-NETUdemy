using RestWithAPS_NETUdemy.Model.Base;
using System.Collections.Generic;

namespace RestWithAPS_NETUdemy.Repository.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T entity);
        T FindById(long id);
        List<T> FindAll();
        T Update(T entity);
        void Delete(long id);
        bool Exists(long? id);
        List<T> FindWithPagedSearch(string query);
        int GetCount(string query);
    }
}
