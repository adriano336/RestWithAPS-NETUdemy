using RestWithAPS_NETUdemy.Model;

namespace RestWithAPS_NETUdemy.Repository
{
    public interface IUserRepository
    {
        User FindByLogin(User login);
    }
}
