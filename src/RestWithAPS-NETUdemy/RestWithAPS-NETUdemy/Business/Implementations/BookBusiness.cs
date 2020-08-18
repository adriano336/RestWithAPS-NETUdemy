using RestWithAPS_NETUdemy.Data.Converters;
using RestWithAPS_NETUdemy.Data.VO;
using RestWithAPS_NETUdemy.Model;
using RestWithAPS_NETUdemy.Repository.Generic;
using System.Collections.Generic;

namespace RestWithAPS_NETUdemy.Business.Implementations
{
    public class BookBusiness : IBookBusiness
    {
        private readonly IRepository<Book> _repository;
        private readonly BookConverter _bookConverter;

        public BookBusiness(IRepository<Book> repository)
        {
            _repository = repository;
            _bookConverter = new BookConverter();
        }

        public BookVO Create(BookVO bookVo)
        {
            var book = _bookConverter.Parse(bookVo);
            return _bookConverter.Parse(_repository.Create(book));
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<BookVO> FindAll()
        {
            return _bookConverter.ParseList(_repository.FindAll());
        }

        public BookVO FindById(long id)
        {
            return _bookConverter.Parse(_repository.FindById(id));
        }

        public BookVO Update(BookVO personVO)
        {
            var person = _bookConverter.Parse(personVO);
            return _bookConverter.Parse(_repository.Update(person));
        }
    }
}
