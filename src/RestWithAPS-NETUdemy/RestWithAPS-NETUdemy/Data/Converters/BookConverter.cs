using RestWithAPS_NETUdemy.Data.Converter;
using RestWithAPS_NETUdemy.Data.VO;
using RestWithAPS_NETUdemy.Model;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAPS_NETUdemy.Data.Converters
{
    public class BookConverter : IParser<BookVO, Book>, IParser<Book, BookVO>
    {
        public Book Parse(BookVO origin)
        {
            if (origin == null) return new Book();

            return new Book
            {
                Id = origin.Id,
                Author = origin.Author,
                LaunchDate = origin.LaunchDate,
                Price = origin.Price,
                Title = origin.Title
            };
        }

        public BookVO Parse(Book origin)
        {
            if (origin == null) return new BookVO();

            return new BookVO
            {
                Id = origin.Id,
                Author = origin.Author,
                LaunchDate = origin.LaunchDate,
                Price = origin.Price,
                Title = origin.Title
            };
        }

        public List<BookVO> ParseList(List<Book> origin)
        {
            if (origin == null) return new List<BookVO>();

            return origin.Select(p => Parse(p)).ToList();
        }

        public List<Book> ParseList(List<BookVO> origin)
        {
            if (origin == null) return new List<Book>();

            return origin.Select(p => Parse(p)).ToList();
        }
    }
}
