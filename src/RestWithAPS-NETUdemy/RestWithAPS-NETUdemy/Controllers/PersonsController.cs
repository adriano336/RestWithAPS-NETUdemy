using Microsoft.AspNetCore.Mvc;
using RestWithAPS_NETUdemy.Business;
using RestWithAPS_NETUdemy.Model;

namespace RestWithAPS_NETUdemy.Controllers
{
    [ApiController]    
    [ApiVersion("1")]
    [Route("[controller]/v{version:apiVersion}")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonBusiness _personBusiness;

        public PersonsController(IPersonBusiness personBusiness)
        {
            _personBusiness = personBusiness;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindById(id);

            if (person == null) return NotFound();

            return Ok(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Person person)
        {
            if (person == null) return BadRequest();
            return new ObjectResult(_personBusiness.Create(person));
        }

        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {
            if (person == null) return BadRequest();
            return new ObjectResult(_personBusiness.Update(person));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {            
            _personBusiness.Delete(id);

            return NoContent();
        }

    }
}
