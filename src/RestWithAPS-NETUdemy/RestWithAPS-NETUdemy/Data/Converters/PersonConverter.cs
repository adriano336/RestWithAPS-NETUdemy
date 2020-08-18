using RestWithAPS_NETUdemy.Data.Converter;
using RestWithAPS_NETUdemy.Data.VO;
using RestWithAPS_NETUdemy.Model;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAPS_NETUdemy.Data.Converters
{
    public class PersonConverter : IParser<PersonVO, Person>, IParser<Person, PersonVO>
    {
        public Person Parse(PersonVO origin)
        {
            if (origin == null) return new Person();

            return new Person
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender
            };
        }

        public PersonVO Parse(Person origin)
        {
            if (origin == null) return new PersonVO();

            return new PersonVO
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender
            };
        }

        public List<PersonVO> ParseList(List<Person> origin)
        {
            if (origin == null) return new List<PersonVO>();

            return origin.Select(p => Parse(p)).ToList();
        }

        public List<Person> ParseList(List<PersonVO> origin)
        {
            if (origin == null) return new List<Person>();

            return origin.Select(p => Parse(p)).ToList();
        }
    }
}
