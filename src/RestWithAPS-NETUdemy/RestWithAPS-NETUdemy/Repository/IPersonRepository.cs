using RestWithAPS_NETUdemy.Model;
using RestWithAPS_NETUdemy.Repository.Generic;
using System.Collections.Generic;

namespace RestWithAPS_NETUdemy.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        List<Person> FindByName(string firstName, string lastName);
    }
}
