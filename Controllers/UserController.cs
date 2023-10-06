using Microsoft.AspNetCore.Mvc;
using adaPrueba_b.Midddlewares;
using adaPrueba_b.Services.UserServices;
using adaPrueba_b.Dtos;

namespace adaPrueba_b.Controllers.v1
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserServices _servicesUser;
        private readonly IAutorizacion _autorizacion;
        public UserController(IUserServices UserServices, IAutorizacion autorizacion)
        {
            _servicesUser = UserServices;
            _autorizacion = autorizacion;
        }
        [HttpGet("singIn")]
        public async Task<ActionResult<ServiceResponse<UserDtos>>> Autorize([FromQuery] string nameUser, [FromQuery] string password)
        {
            return await _autorizacion.SingIn(nameUser, password);

        }
        [HttpPost("register-user")]
        public async Task<ActionResult<ServiceResponse<UserRegisterDtos>>> SaveUser(UserRegisterDtos user)
        {
            return await _servicesUser.SaveUser(user);
        }
        [HttpGet("get-users")]
        public async Task<ActionResult<ServiceResponse<List<User>>>> GetUser()
        {
            return await _servicesUser.GetUsers();
        }
    }
}