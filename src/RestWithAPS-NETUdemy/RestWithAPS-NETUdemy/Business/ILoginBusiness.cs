using RestWithAPS_NETUdemy.Model;

namespace RestWithAPS_NETUdemy.Business
{
    public interface ILoginBusiness
    {
        object FindByLogin(User login);
    }
}
