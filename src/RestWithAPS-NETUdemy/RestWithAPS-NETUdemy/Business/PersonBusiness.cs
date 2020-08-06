using RestWithAPS_NETUdemy.Model;
using RestWithAPS_NETUdemy.Repository;
using System.Collections.Generic;

namespace RestWithAPS_NETUdemy.Business
{
    public class PersonBusiness : IPersonBusiness
    {
        private IPersonRepository _repository;

        public PersonBusiness(IPersonRepository repository)
        {
            _repository = repository;
        }

        public Person Create(Person person)
        {
            return _repository.Create(person);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<Person> FindAll()
        {
            return _repository.FindAll();
        }

        public Person FindById(long id)
        {
            return _repository.FindById(id);
        }

        public Person Update(Person person)
        {
            var result = _repository.Update(person);
            return result;
        }
    }
}
