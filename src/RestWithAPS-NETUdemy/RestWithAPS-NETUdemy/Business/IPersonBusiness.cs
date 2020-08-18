using RestWithAPS_NETUdemy.Data.VO;
using System.Collections.Generic;
using Tapioca.HATEOAS.Utils;

namespace RestWithAPS_NETUdemy.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        PersonVO FindById(long id);
        List<PersonVO> FindAll();
        PersonVO Update(PersonVO person);
        void Delete(long id);
        List<PersonVO> FindByName(string firstName, string lastName);
        PagedSearchDTO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);
    }
}
