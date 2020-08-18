using Microsoft.AspNetCore.Mvc;
using RestWithAPS_NETUdemy.Business;
using RestWithAPS_NETUdemy.Data.VO;
using RestWithAPS_NETUdemy.Model;

namespace RestWithAPS_NETUdemy.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class BooksController : ControllerBase
    {
        private readonly IBookBusiness _bookBusiness;

        public BooksController(IBookBusiness bookBusiness)
        {
            _bookBusiness = bookBusiness;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_bookBusiness.FindById(id));
        }

        // POST api/<BooksController>
        [HttpPost]
        public IActionResult Post([FromBody] BookVO book)
        {
            return Ok(_bookBusiness.Create(book));
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] BookVO book)
        {
            return Ok(_bookBusiness.Update(book));
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bookBusiness.Delete(id);
            return NoContent();
        }
    }
}
