using RestWithAPS_NETUdemy.Data.Converters;
using RestWithAPS_NETUdemy.Data.VO;
using RestWithAPS_NETUdemy.Repository;
using System.Collections.Generic;
using Tapioca.HATEOAS.Utils;

namespace RestWithAPS_NETUdemy.Business.Implementations
{
    public class PersonBusiness : IPersonBusiness
    {
        private IPersonRepository _repository;
        private readonly PersonConverter _converter;

        public PersonBusiness(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<PersonVO> FindAll()
        {
            return _converter.ParseList(_repository.FindAll());
        }

        public PersonVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }

        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _converter.ParseList(_repository.FindByName(firstName, lastName));
        }

        public PagedSearchDTO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            page = page > 0 ? page - 1 : 0;
            string query = " Select * From Persons p Where 1 = 1 ";
            if (!string.IsNullOrEmpty(name)) query += $" and p.firstName like '%{name}%'";
            query += $" Order By p.firstName {sortDirection} limit {pageSize} offset {page}";

            var persons = _converter.ParseList(_repository.FindWithPagedSearch(query));

            string countQuery = " Select count(-1) From Persons p Where 1 = 1";
            if (!string.IsNullOrEmpty(name)) countQuery += $" and p.firstName like '%{name}%'";

            var totalResult = _repository.GetCount(countQuery);

            return new PagedSearchDTO<PersonVO>
            {
                CurrentPage = page + 1,
                List = persons,
                SortDirections = sortDirection,
                TotalResults = totalResult,
                PageSize = pageSize
            };
        }
    }
}
