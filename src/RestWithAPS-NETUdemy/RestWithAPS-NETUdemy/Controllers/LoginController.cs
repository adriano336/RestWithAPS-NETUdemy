using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithAPS_NETUdemy.Business;
using RestWithAPS_NETUdemy.Model;

namespace RestWithAPS_NETUdemy.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginBusiness _business;

        public LoginController(ILoginBusiness business)
        {
            _business = business;
        }

        [HttpPost]
        [AllowAnonymous]        
        public object Post([FromBody] User user)
        {
            if (user == null) return BadRequest();
            return _business.FindByLogin(user);
        }


    }
}
