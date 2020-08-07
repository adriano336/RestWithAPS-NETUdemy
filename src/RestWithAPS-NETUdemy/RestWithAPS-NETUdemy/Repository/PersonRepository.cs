using RestWithAPS_NETUdemy.Model;
using RestWithAPS_NETUdemy.Model.Context;
using RestWithAPS_NETUdemy.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAPS_NETUdemy.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MySQLContext context) : base(context)
        {
        }
    }
}
