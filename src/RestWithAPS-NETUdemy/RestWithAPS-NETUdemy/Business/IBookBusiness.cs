using RestWithAPS_NETUdemy.Model;
using System.Collections.Generic;

namespace RestWithAPS_NETUdemy.Business
{
    public interface IBookBusiness
    {
        Book Create(Book book);
        Book FindById(long id);
        List<Book> FindAll();
        Book Update(Book book);
        void Delete(long id);
    }
}
