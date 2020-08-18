using RestWithAPS_NETUdemy.Model;
using RestWithAPS_NETUdemy.Model.Context;
using RestWithAPS_NETUdemy.Repository.Generic;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAPS_NETUdemy.Repository.Implementattions
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MySQLContext context) : base(context)
        {
        }

        public List<Person> FindByName(string firstName, string lastName)
        {
            return _context.Persons.Where(p => (string.IsNullOrEmpty(firstName) || p.FirstName == firstName)
            && (string.IsNullOrEmpty(lastName) || p.LastName == lastName)).ToList();

        }
    }
}
