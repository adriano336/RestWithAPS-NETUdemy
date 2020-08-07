using RestWithAPS_NETUdemy.Model;
using RestWithAPS_NETUdemy.Repository.Generic;
using System.Collections.Generic;

namespace RestWithAPS_NETUdemy.Business
{
    public class BookBusiness : IBookBusiness
    {
        private readonly IRepository<Book> _repository;

        public BookBusiness(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public Book Create(Book book)
        {
            return _repository.Create(book);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<Book> FindAll()
        {
            return _repository.FindAll();
        }

        public Book FindById(long id)
        {
            return _repository.FindById(id);
        }

        public Book Update(Book person)
        {
            return _repository.Update(person);
        }
    }
}
