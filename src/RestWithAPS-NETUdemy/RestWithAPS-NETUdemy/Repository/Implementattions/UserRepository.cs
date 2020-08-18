using RestWithAPS_NETUdemy.Model;
using RestWithAPS_NETUdemy.Model.Context;
using System.Linq;

namespace RestWithAPS_NETUdemy.Repository.Implementattions
{
    public class UserRepository : IUserRepository
    {
        private readonly MySQLContext _context;

        public UserRepository(MySQLContext context)
        {
            _context = context;
        }

        public User FindByLogin(User login)
        {
            return _context.Users.SingleOrDefault(u => u.Login.Equals(login.Login));
        }
    }
}
